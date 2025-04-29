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
	#endregion

	#region FIELDS
	private bool _isInitialized = false;

	private Func<CharacterDevelopmentModel> _cdmInitializer = null;
	#endregion

	#region OVERRIDES
	protected override void OnSubModuleLoad()
	{
		base.OnSubModuleLoad();

		try
		{
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
		base.OnBeforeInitialModuleScreenSetAsRoot();

		try
		{
			if (_isInitialized)
				return;
			_isInitialized = true;

			MCMSettings.Settings = new MCMSettings();

			var moduleNames = Utilities.GetModulesNames();
			CheckCompatibilityRequired(moduleNames, "TOR_Core", TORUtility.InitializeCompatibility);

			MCMSettings.Settings.Build();
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnBeforeInitialModuleScreenSetAsRoot)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
	{
		base.OnGameStart(game, gameStarterObject);

		try
		{
			if (game.GameType is Campaign)
			{
				//GeneralUtility.Message("OnGameStart", false, Colors.Magenta);
				MCMSettings.Settings.OnGameStart();

				_characterDevelopmentModel = _cdmInitializer?.Invoke() ?? new AdjustableCharacterDevelopmentModel();
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
		base.OnNewGameCreated(game, initializerObject);

		try
		{
			if (game.GameType is Campaign)
			{
				//GeneralUtility.Message("OnNewGameCreated", false, Colors.Magenta);
				MCMSettings.Settings.OnNewGameCreated();
				GameCreatedOrLoaded(game);
			}
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnNewGameCreated)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnGameLoaded(Game game, object initializerObject)
	{
		base.OnGameLoaded(game, initializerObject);

		try
		{
			if (game.GameType is Campaign)
			{
				//GeneralUtility.Message("OnGameLoaded", false, Colors.Magenta);
				MCMSettings.Settings.OnGameLoaded();
				GameCreatedOrLoaded(game);
			}
		}
		catch (Exception exc)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(OnGameLoaded)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
		}
	}

	public override void OnGameEnd(Game game)
	{
		base.OnGameEnd(game);

		try
		{
			if (game.GameType is Campaign)
			{
				//GeneralUtility.Message("OnGameEnd", false, Colors.Magenta);
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

	private void CheckCompatibilityRequired(string[] moduleNames, string moduleName, Func<Func<CharacterDevelopmentModel>> initializeCompatibility)
	{
		if (!moduleNames.Contains(moduleName))
			return;

		if (_cdmInitializer != null)
		{
			GeneralUtility.Message($"ERROR: Adjustable Leveling found '{moduleName}', compatibility conflict detected!", false, Colors.Red, false);
			return;
		}

		_cdmInitializer = initializeCompatibility();
		GeneralUtility.Message($"INFO: Adjustable Leveling found '{moduleName}', applying compatibility", false, Colors.White, false);
	}
	#endregion
}
