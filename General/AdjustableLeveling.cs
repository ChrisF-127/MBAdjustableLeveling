using AdjustableLeveling.Leveling;
using AdjustableLeveling.Settings;
using AdjustableLeveling.Utility;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace AdjustableLeveling.General;

public class AdjustableLeveling : MBSubModuleBase
{
	#region PROPERTIES
	private static CharacterDevelopmentModel _characterDevelopmentModel;
	public static CharacterDevelopmentModel CharacterDevelopmentModel => 
		_characterDevelopmentModel; 

	public static bool Compatibility_TheOldRealm { get; private set; }
	#endregion

	#region FIELDS
	private bool _isInitialized = false;
	#endregion

	#region OVERRIDES
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
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnSubModuleLoad)}):\n{exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

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
			Compatibility_TheOldRealm = HandleCompatibility(ref _characterDevelopmentModel, moduleNames, "TOR_Core", TORUtility.InitializeCompatibility);

			MCMSettings.Settings.Build();
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnBeforeInitialModuleScreenSetAsRoot)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
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
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnGameStart)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnNewGameCreated(Game game, object initializerObject)
	{
		try
		{
			base.OnNewGameCreated(game, initializerObject);

			if (game.GameType is Campaign)
				GameCreatedOrLoaded(game);
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnNewGameCreated)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnGameLoaded(Game game, object initializerObject)
	{
		try
		{
			base.OnGameLoaded(game, initializerObject);

			if (game.GameType is Campaign)
				GameCreatedOrLoaded(game);
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnGameLoaded)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnGameEnd(Game game)
	{
		try
		{
			base.OnGameEnd(game);

			if (game.GameType is Campaign)
			{
				MCMSettings.Settings.OnGameEnd();
			}
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnGameEnd)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}
	#endregion

	#region PRIVATE METHODS
	private void GameCreatedOrLoaded(Game game)
	{
		MCMSettings.Settings.OnGameLoaded();

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

	private static bool HandleCompatibility(ref CharacterDevelopmentModel characterDevelopmentModel, string[] moduleNames, string moduleName, Func<CharacterDevelopmentModel> initialize)
	{
		if (!moduleNames.Contains(moduleName))
			return false;

		if (characterDevelopmentModel != null)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling found {moduleName}, compatibility conflict detected!", false, Colors.Red, false);
			return false;
		}

		characterDevelopmentModel = initialize();
		GeneralUtility.Message($"INFO: Adjustable Leveling found {moduleName}, applying compatibility", false, Colors.White, false);
		return true;
	}
	#endregion
}
