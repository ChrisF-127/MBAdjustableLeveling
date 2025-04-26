using AdjustableLeveling.Leveling;
using AdjustableLevelingTOR;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade;

namespace AdjustableLeveling;

public class AdjustableLeveling : MBSubModuleBase
{
	private static CharacterDevelopmentModel _characterDevelopmentModel;
	public static CharacterDevelopmentModel CharacterDevelopmentModel => 
		_characterDevelopmentModel; 

	public static bool Compatibility_TheOldRealm { get; private set; }

	private bool _isInitialized = false;

	protected override void OnBeforeInitialModuleScreenSetAsRoot()
	{
		try
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();

			if (_isInitialized)
				return;
			_isInitialized = true;

			MCMSettings.Settings = new MCMSettings();

			var moduleNames = Utilities.GetModulesNames();
			Compatibility_TheOldRealm = HandleCompatibility(ref _characterDevelopmentModel, moduleNames, "TOR_Core", AdjLvlTORUtility.GetCDM);
		}
		catch (Exception exc)
		{
			AdjLvlUtility.Message($"ERROR: Adjustable Leveling failed to initialize ({nameof(OnBeforeInitialModuleScreenSetAsRoot)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
	{
		try
		{
			base.OnGameStart(game, gameStarterObject);

			if (game.GameType is Campaign)
			{
				_characterDevelopmentModel ??= new AdjustableCharacterDevelopmentModel();
				((CampaignGameStarter)gameStarterObject).AddModel(CharacterDevelopmentModel);
			}
		}
		catch (Exception exc)
		{
			AdjLvlUtility.Message($"ERROR: Adjustable Leveling failed to initialize ({nameof(OnGameStart)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnGameLoaded(Game game, object initializerObject)
	{
		base.OnGameLoaded(game, initializerObject);

		var _totalXp = typeof(HeroDeveloper).GetField("_totalXp", BindingFlags.NonPublic | BindingFlags.Instance);
		var characters = game.ObjectManager.GetObjectTypeList<CharacterObject>();
		foreach (var character in characters)
		{
			if (character.IsHero 
				&& character.HeroObject is Hero hero
				&& hero.HeroDeveloper is IHeroDeveloper heroDeveloper)
			{
				var level = hero.Level;
				var totalXp = heroDeveloper.TotalXp;
				var currXp = heroDeveloper.GetXpRequiredForLevel(level);
				var nextXp = heroDeveloper.GetXpRequiredForLevel(level + 1);

				// reset totalXp to half-way to next level if it is out of bounds for the current level
				if (level > 0 && (totalXp < currXp || totalXp > nextXp))
				{
					var newTotalXp = (currXp + nextXp) / 2;
					_totalXp.SetValue(heroDeveloper, newTotalXp);
				}
			}
		}
	}

	protected override void OnSubModuleLoad()
	{
		try
		{
			base.OnSubModuleLoad();
			var harmony = new Harmony("sy.adjustableleveling");
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
		catch (Exception exc)
		{
			AdjLvlUtility.Message($"ERROR: Adjustable Leveling failed to initialize ({nameof(OnSubModuleLoad)}):\n{exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	private static bool HandleCompatibility(ref CharacterDevelopmentModel characterDevelopmentModel, string[] moduleNames, string moduleName, Func<CharacterDevelopmentModel> getCDM)
	{
		if (!moduleNames.Contains(moduleName))
			return false;

		if (characterDevelopmentModel != null)
		{
			AdjLvlUtility.Message($"ERROR: Adjustable Leveling found {moduleName}, compatibility conflict detected!", false, Colors.Red, false);
			return false;
		}

		characterDevelopmentModel = getCDM();
		AdjLvlUtility.Message($"INFO: Adjustable Leveling found {moduleName}, applying compatibility", false, Colors.White, false);

#warning TODO add settings
		return true;
	}
}
