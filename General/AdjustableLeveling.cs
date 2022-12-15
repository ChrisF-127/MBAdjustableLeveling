using AdjustableLeveling.Leveling;
using HarmonyLib;
using MCM.Abstractions.Base.Global;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace AdjustableLeveling;

public class AdjustableLeveling : MBSubModuleBase
{
	public static MCMSettings Settings { get; private set; }
	public static AdjustableCharacterDevelopmentModel CharacterDevelopmentModel { get; private set; }

	private bool _isInitialized = false;

	protected override void OnBeforeInitialModuleScreenSetAsRoot()
	{
		try
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (_isInitialized)
				return;
			_isInitialized = true;

			Settings = GlobalSettings<MCMSettings>.Instance;
			if (Settings == null)
				throw new Exception("Settings is null");
		}
		catch (Exception exc)
		{
			var text = $"ERROR: Adjustable Leveling failed to initialize ({nameof(OnBeforeInitialModuleScreenSetAsRoot)}):";
			InformationManager.DisplayMessage(new InformationMessage(text + exc.GetType().ToString(), new Color(1f, 0f, 0f)));
			FileLog.Log(text + "\n" + exc.ToString());
		}
	}

	protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
	{
		try
		{
			base.OnGameStart(game, gameStarterObject);
			if (game.GameType is Campaign)
			{
				CharacterDevelopmentModel = new AdjustableCharacterDevelopmentModel();
				((CampaignGameStarter)gameStarterObject).AddModel(CharacterDevelopmentModel);
			}
		}
		catch (Exception exc)
		{
			var text = $"ERROR: Adjustable Leveling failed to initialize ({nameof(OnGameStart)}):";
			InformationManager.DisplayMessage(new InformationMessage(text + exc.GetType().ToString(), new Color(1f, 0f, 0f)));
			FileLog.Log(text + "\n" + exc.ToString());
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
			var text = $"ERROR: Adjustable Leveling failed to initialize ({nameof(OnSubModuleLoad)}):";
			InformationManager.DisplayMessage(new InformationMessage(text + exc.GetType().ToString(), new Color(1f, 0f, 0f)));
			FileLog.Log(text + "\n" + exc.ToString());
		}
	}
}
