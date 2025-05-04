//#define DEBUG_PRINT

using AdjustableLeveling.Utility;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling.Settings
{
	public enum SkillUserEnum
	{
		Player,
		NPC,
		Clan,
		Companion,
	}

	public class MCMSettings
	{
		#region CONSTANTS
		public const string Id = "AdjustableLeveling";
		public const string DisplayName = "Adjustable Leveling";
		public const string FolderName = "AdjustableLeveling";
		public const string FormatType = "json";

		public const string PlayerTag = "Player_";
		public const string NPCTag = "NPC_";
		public const string ClanTag = "Clan_";
		public const string CompanionTag = "Companion_";

		private const string SkillLevelingPlayerSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_PlayerSkills}Player Skills";
		private const string PlayerSpecificSkillHintText = "{=adjlvl_hint_PlayerSkillSpecific}Further modifies Player skills with a factor. [Default: 1.00]";

		private const string SkillLevelingNPCSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_NPCSkills}NPC Skills";
		private const string NPCSpecificSkillHintText = "{=adjlvl_hint_NPCSkillSpecific}Further modifies NPC skills with a factor. Does not modify Clan Member or Companion skills. [Default: 1.00]";

		private const string SkillLevelingClanSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_ClanSkills}Clan Member Skills";
		private const string ClanSpecificSkillHintText = "{=adjlvl_hint_ClanSkillSpecific}Further modifies Clan Member skills with a factor. [Default: 1.00]";

		private const string SkillLevelingCompanionSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_CompanionSkills}Companion Skills";
		private const string CompanionSpecificSkillHintText = "{=adjlvl_hint_CompanionSkillSpecific}Further modifies Companion skills with a factor. [Default: 1.00]";
		#endregion

		#region PROPERTIES
		public static MCMSettings Settings { get; set; }

		public FluentGlobalSettings GlobalSettings { get; private set; }
		public FluentPerCampaignSettings PerCampaignSettings { get; private set; }

		private Dictionary<string, Func<SkillObject>> SkillObjectGetters { get; } = [];
		private Dictionary<int, Func<SkillUserEnum, float>> SkillModifierGetters { get; } = [];
		private List<int> WarnMissingSkillGetterOnceList { get; } = [];
		private List<int> WarnMissingSkillModifierOnceList { get; } = [];

		#region SETTINGS PROPERTIES
		#region CHARACTER LEVELING MODIFIERS
		public bool UseFasterLevelingCurve { get; set; } = false;

		public int MaxCharacterLevel { get; set; } = 62;
		public float LevelXPModifier { get; set; } = 1f;

		public int LevelsPerAttributePoint { get; set; } = 4;
		public int FocusPointsPerLevel { get; set; } = 1;

		public int MaxAttribute { get; set; } = 10;
		public int MaxFocusPerSkill { get; set; } = 5;

		public int AttributePointsAtStart { get; set; } = 15;
		public int FocusPointsAtStart { get; set; } = 5;
		#endregion

		#region SKILL LEVELING MODIFIERS
		#region GENERAL
		public int LearningLimitIncreasePerAttributePoint { get; set; } = 3;
		public int LearningLimitIncreasePerFocusPoint { get; set; } = 50;
		public int BaseLearningLimit { get; set; } = 50;

		public float MinLearningRate { get; set; } = 0f;
		public float MaxLearningRate { get; set; } = 0f;

		public float PlayerSkillXPModifier { get; set; } = 1f;
		public float NPCSkillXPModifier { get; set; } = 1f;
		public float ClanSkillXPModifier { get; set; } = 1f;
		public float CompanionSkillXPModifier { get; set; } = 1f;
		#endregion

		#region SKILL MODIFIERS
		public Dictionary<string, float> SkillXPModifiers { get; } = [];
		#endregion
		#endregion

		#region OTHER STUFF
		public float TroopXPModifier { get; set; } = 1f;

		public bool WarningShown_1_2_12_2 { get; set; } = false;
		#endregion

		#region SMITHING PART RESEARCH MODIFIERS
		public float SmithingResearchModifier { get; set; } = 1f;
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
		#endregion
		#endregion

		#region FIELDS
		private readonly ISettingsBuilder _settingsBuilder;
		private ISettingsPropertyGroupBuilder _playerSkillsGroupBuilder;
		private ISettingsPropertyGroupBuilder _npcSkillsGroupBuilder;
		private ISettingsPropertyGroupBuilder _clanSkillsGroupBuilder;
		private ISettingsPropertyGroupBuilder _companionSkillsGroupBuilder;

		private int _groupOrder = 0;
		private int _characterLevelingPropertyOrder = 0;
		private int _skillLevelingPropertyOrder = 0;
		private int _playerSkillLevelingPropertyOrder = 0;
		private int _npcSkillLevelingPropertyOrder = 0;
		private int _clanSkillLevelingPropertyOrder = 0;
		private int _companionSkillLevelingPropertyOrder = 0;
		private int _otherPropertyOrder = 0;
		private int _smithingPropertyOrder = 0;
		#endregion

		#region CONSTRUCTORS
		public MCMSettings()
		{
			#region SETTINGS
			_settingsBuilder = BaseSettingsBuilder.Create(Id, DisplayName)
				.SetFormat(FormatType)
				.SetFolderName(FolderName)
				.SetSubFolder(string.Empty)

			#region CHARACTER LEVELING MODIFIERS
				.CreateGroup("{=adjlvl_group_CharacterLeveling}Character Leveling", g => g
					.SetGroupOrder(_groupOrder++)
					.AddBool(
						nameof(UseFasterLevelingCurve),
						"{=adjlvl_name_FasterLevelingCurve}Faster Leveling Curve",
						new ProxyRef<bool>(() => UseFasterLevelingCurve, v => UseFasterLevelingCurve = v), b => b
						.SetHintText("{=adjlvl_hint_FasterLevelingCurve}Slower earlier but faster later levels, level 62 total: 40.7m [ON] vs 95.4m [OFF]. [Default: OFF]\n-WARNING: Backup save recommended, changing this in an ongoing save will reset the level xp to half-way to the next level (if total xp is out of bounds for the current level after conversion)!")
						.SetRequireRestart(true)
						.SetOrder(_characterLevelingPropertyOrder++))

					.AddInteger(
						nameof(MaxCharacterLevel),
						"{=adjlvl_name_MaxCharacterLevel}Maximum Character Level",
						5,
						1024,
						new ProxyRef<int>(() => MaxCharacterLevel, v => MaxCharacterLevel = v), b => b
						.SetHintText("{=adjlvl_hint_MaxCharacterLevel}Adjust the maximum achievable character level. Higher levels require much more xp! [Default: 62]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddFloatingInteger(
						nameof(LevelXPModifier),
						"{=adjlvl_name_CharacterLevelXPModifier}Character Level XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => LevelXPModifier, v => LevelXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_CharacterLevelXPModifier}Adjust how skill xp is converted into level xp, default is 1-to-1 at 1.00. [Default: 1.00]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddInteger(
						nameof(LevelsPerAttributePoint),
						"{=adjlvl_name_LevelsPerAttributePoint}Levels per Attribute Point",
						1,
						10,
						new ProxyRef<int>(() => LevelsPerAttributePoint, v => LevelsPerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LevelsPerAttributePoint}Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Default: 4]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsPerLevel),
						"{=adjlvl_name_FocusPointsPerLevel}Focus Points per Level",
						1,
						10,
						new ProxyRef<int>(() => FocusPointsPerLevel, v => FocusPointsPerLevel = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsPerLevel}Focus points gained per level. [Default: 1]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(MaxAttribute),
						"{=adjlvl_name_MaxAttributePointsForAttribute}Max Attribute Points for Attribute",
						1,
						1000,
						new ProxyRef<int>(() => MaxAttribute, v => MaxAttribute = v), b => b
						.SetHintText("{=adjlvl_hint_MaxAttributePointsForAttribute}Attribute point limit per attribute. [Default: 10]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(MaxFocusPerSkill),
						"{=adjlvl_name_MaxFocusPointsForSkill}Max Focus Points for Skill",
						1,
						1000,
						new ProxyRef<int>(() => MaxFocusPerSkill, v => MaxFocusPerSkill = v), b => b
						.SetHintText("{=adjlvl_hint_MaxFocusPointsForSkill}Focus point limit per skill. (UI will at most show 5 points) [Default: 5]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(AttributePointsAtStart),
						"{=adjlvl_name_AttributePointsAtStart}Attribute Points at Start",
						1,
						100,
						new ProxyRef<int>(() => AttributePointsAtStart, v => AttributePointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_AttributePointsAtStart}Apparently affects the attribute points with which NPCs start, but not the player. [Default: 15]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsAtStart),
						"{=adjlvl_name_FocusPointsAtStart}Focus Points at Start",
						1,
						100,
						new ProxyRef<int>(() => FocusPointsAtStart, v => FocusPointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsAtStart}Apparently affects the focus points with which NPCs start, but not the player. [Default: 5]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					)
			#endregion

			#region SKILL LEVELING SETTINGS
				.CreateGroup("{=adjlvl_group_SkillLeveling}Skill Leveling", g => g
					.SetGroupOrder(_groupOrder++)
					.AddInteger(
						nameof(LearningLimitIncreasePerAttributePoint),
						"{=adjlvl_name_LearningLimitAttributePoint}Learning Limit Increase per Attribute Point",
						0,
						50,
						new ProxyRef<int>(() => LearningLimitIncreasePerAttributePoint, v => LearningLimitIncreasePerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitAttributePoint}E.g. at 3 and with 10 AP an additional 30 skill points can be gained at reducing learning rate; at 5 an additional 50 can be gained. [Default: 3]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(LearningLimitIncreasePerFocusPoint),
						"{=adjlvl_name_LearningLimitFocusPoint}Learning Limit Increase per Focus Point",
						0,
						100,
						new ProxyRef<int>(() => LearningLimitIncreasePerFocusPoint, v => LearningLimitIncreasePerFocusPoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitFocusPoint}Adjust the learning limit increase per focus point. [Default: 50]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(BaseLearningLimit),
						"{=adjlvl_name_BaseLearningLimit}Base Learning Limit",
						0,
						100,
						new ProxyRef<int>(() => BaseLearningLimit, v => BaseLearningLimit = v), b => b
						.SetHintText("{=adjlvl_hint_BaseLearningLimit}The base learning limit achievable without focus points. [Default: 50]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddFloatingInteger(
						nameof(MinLearningRate),
						"{=adjlvl_name_MinLearningRate}Minimum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MinLearningRate, v => MinLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MinLearningRate}Set a minimum learning rate, so that no learning limit exists. [Default: 0.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(MaxLearningRate),
						"{=adjlvl_name_MaxLearningRate}Maximum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MaxLearningRate, v => MaxLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MaxLearningRate}Set a maximum learning rate, zero disables it. [Default: 0.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddFloatingInteger(
						nameof(PlayerSkillXPModifier),
						"{=adjlvl_name_PlayerSkillXPModifier}Player Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => PlayerSkillXPModifier, v => PlayerSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_PlayerSkillXPModifier}Sets the skill learning rate for the player character. [Default: 1.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier),
						"{=adjlvl_name_NPCSkillXPModifier}NPC Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier, v => NPCSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_NPCSkillXPModifier}Sets the skill learning rate for all NPCs, except clan members and companions. [Default: 1.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier),
						"{=adjlvl_name_ClanSkillXPModifier}Clan Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier, v => ClanSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_ClanSkillXPModifier}Sets the skill learning rate for clan members. [Default: 1.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(CompanionSkillXPModifier),
						"{=adjlvl_name_CompanionSkillXPModifier}Companion Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => CompanionSkillXPModifier, v => CompanionSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_CompanionSkillXPModifier}Sets the skill learning rate for companions. [Default: 1.00]")
						.SetOrder(_skillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					)

				 // Skill Leveling Modifier Sub-Groups
				.CreateGroup(SkillLevelingPlayerSkillsGroupName, g =>
					_playerSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g =>
					_npcSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
				.CreateGroup(SkillLevelingClanSkillsGroupName, g =>
					_clanSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
				.CreateGroup(SkillLevelingCompanionSkillsGroupName, g =>
					_companionSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
			#endregion

			#region OTHER STUFF
				.CreateGroup("{=adjlvl_group_Other}Other", g => g
					.SetGroupOrder(_groupOrder++)
					.AddFloatingInteger(
						nameof(TroopXPModifier),
						"{=adjlvl_name_TroopXPModifier}Troop XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => TroopXPModifier, v => TroopXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_TroopXPModifier}Modifies XP gained for upgrading troops. (Required XP numbers in roster will show unmodified values, but dropping equipment is limited to modified value.) [Default 1.00]")
						.SetOrder(_otherPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddBool(
						nameof(WarningShown_1_2_12_2),
						"{=adjlvl_name_DisableStartUpWarning}Uncheck to show version warning on startup again",
						new ProxyRef<bool>(() => WarningShown_1_2_12_2, v => WarningShown_1_2_12_2 = v), b => b
						.SetHintText("{=adjlvl_hint_DisableStartUpWarning}Show the version warning again on starting the game")
						.SetOrder(_otherPropertyOrder++))
					)
			#endregion

			#region SMITHING PART RESEARCH MODIFIERS
				.CreateGroup("{=adjlvl_group_SmithingResearch}Smithing Research", g => g
					.SetGroupOrder(_groupOrder++)
					.AddFloatingInteger(
						nameof(SmithingResearchModifier),
						"{=adjlvl_name_PartResearch}Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingResearchModifier, v => SmithingResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_PartResearch}Adjust smithing part research gain rate for smithing and smelting weapons. [Default: 100%]")
						.SetOrder(_smithingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SmithingFreeBuildResearchModifier),
						"{=adjlvl_name_FreeBuildPartResearch}Free Build Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingFreeBuildResearchModifier, v => SmithingFreeBuildResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_FreeBuildPartResearch}Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Default: 10%]")
						.SetOrder(_smithingPropertyOrder++)
						.AddValueFormat("0.00"))
				);
			#endregion

			#region SKILL LEVELING MODIFIERS
			// VIGOR
			AddSkill("OneHanded", "{=PiHpR4QL}One Handed", () => DefaultSkills.OneHanded);
			AddSkill("TwoHanded", "{=t78atYqH}Two Handed", () => DefaultSkills.TwoHanded);
			AddSkill("Polearm", "{=haax8kMa}Polearm", () => DefaultSkills.Polearm);
			// CONTROL
			AddSkill("Bow", "{=5rj7xQE4}Bow", () => DefaultSkills.Bow);
			AddSkill("Crossbow", "{=TTWL7RLe}Crossbow", () => DefaultSkills.Crossbow);
			AddSkill("Throwing", "{=2wclahIJ}Throwing", () => DefaultSkills.Throwing);
			// ENDURANCE
			AddSkill("Riding", "{=p9i3zRm9}Riding", () => DefaultSkills.Riding);
			AddSkill("Athletics", "{=skZS2UlW}Athletics", () => DefaultSkills.Athletics);
			AddSkill("Crafting", "{=smithingskill}Smithing", () => DefaultSkills.Crafting);
			// CUNNING
			AddSkill("Scouting", "{=LJ6Krlbr}Scouting", () => DefaultSkills.Scouting);
			AddSkill("Tactics", "{=m8o51fc7}Tactics", () => DefaultSkills.Tactics);
			AddSkill("Roguery", "{=V0ZMJ0PX}Roguery", () => DefaultSkills.Roguery);
			// SOCIAL
			AddSkill("Charm", "{=EGeY1gfs}Charm", () => DefaultSkills.Charm);
			AddSkill("Leadership", "{=HsLfmEmb}Leadership", () => DefaultSkills.Leadership);
			AddSkill("Trade", "{=GmcgoiGy}Trade", () => DefaultSkills.Trade);
			// INTELLIGENCE
			AddSkill("Steward", "{=stewardskill}Steward", () => DefaultSkills.Steward);
			AddSkill("Medicine", "{=JKH59XNp}Medicine", () => DefaultSkills.Medicine);
			AddSkill("Engineering", "{=engineeringskill}Engineering", () => DefaultSkills.Engineering);
			#endregion
			#endregion
		}
		#endregion

		#region PUBLIC METHODS
		public void Build()
		{
			// create global settings
			GlobalSettings = _settingsBuilder.BuildAsGlobal();

			// create per campaign settings
			PerCampaignSettings = _settingsBuilder.BuildAsPerCampaign();

			// register global settings
			GlobalSettings.Register();

			// Version warning popup
			if (!WarningShown_1_2_12_2)
			{
				InformationManager.ShowInquiry(new InquiryData(
					"{=adjlvl_title_ChangesPopup}ADJUSTABLE LEVELING CHANGES",
					"{=adjlvl_title_ChangesPopupText_1_2_12_2}>> ATTENTION !!" +
					"\nAdjustable Leveling has been updated and its skill modifier settings have been reworked:" +
					"\n" +
					"\n- General skill user modifiers no longer override each other and instead apply depending on skill user." +
					"\n- Specific skill modifiers are treated as factors further modifying the result." +
					"\n Total skill gain = (skill gain * skill user modifier * specific skill modifier)" +
					"\nWARNING: modifiers which are set to 0 will cause the skill not to progress!" +
					"\n" +
					"\n>> Check the settings before continuing a campaign!" +
					"\n" +
					"\nClick 'ATTENTION' to continue",
					true,
					false,
					"{=adjlvl_title_ChangesAttention}ATTENTION",
					"",
					() => WarningShown_1_2_12_2 = true,
					() => { }));
			}
		}

		public void OnGameStart()
		{
			GlobalSettings.Unregister();
			PerCampaignSettings.Register();
		}
		public void OnNewGameCreated()
		{
			InitializeSkillModifierGetters();

			// For some reason MCM removes the settings between OnGameStart and here, so register it *AGAIN*
			PerCampaignSettings.Register();
		}
		public void OnGameLoaded()
		{
			InitializeSkillModifierGetters();
		}
		public void OnGameEnd()
		{
			SkillModifierGetters.Clear();

			PerCampaignSettings.Unregister();
			GlobalSettings.Register();
		}

		public void AddSkill(string id, string name, Func<SkillObject> getSkillObject)
		{
			try
			{
				// Player settings
				SkillXPModifiers.Add(PlayerTag + id, 1f);
				_playerSkillsGroupBuilder.AddFloatingInteger(
					"PlayerSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(PlayerTag + id), b => b
					.SetHintText(PlayerSpecificSkillHintText)
					.SetOrder(_playerSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// NPC settings
				SkillXPModifiers.Add(NPCTag + id, 1f);
				_npcSkillsGroupBuilder.AddFloatingInteger(
					"NPCSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(NPCTag + id), b => b
					.SetHintText(NPCSpecificSkillHintText)
					.SetOrder(_npcSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// Clan settings
				SkillXPModifiers.Add(ClanTag + id, 1f);
				_clanSkillsGroupBuilder.AddFloatingInteger(
					"ClanSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(ClanTag + id), b => b
					.SetHintText(ClanSpecificSkillHintText)
					.SetOrder(_clanSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// Companion settings
				SkillXPModifiers.Add(CompanionTag + id, 1f);
				_companionSkillsGroupBuilder.AddFloatingInteger(
					"CompanionSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(CompanionTag + id), b => b
					.SetHintText(CompanionSpecificSkillHintText)
					.SetOrder(_companionSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// Intialize SkillObject-Getter for SkillHelper
				SkillObjectGetters.Add(id, getSkillObject);
			}
			catch (Exception exc)
			{
				GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(MCMSettings)}.{nameof(AddSkill)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
			}

			ProxyRef<float> createSkillProxy(string name) =>
				new(() => SkillXPModifiers[name], v => SkillXPModifiers[name] = v);
		}

		public float GetSkillModifier(SkillObject skill, Hero hero)
		{
			// get skill user
			var skillUser = GetSkillUser(hero);

			// get skill modifier
			var skillModifier = 1f;
			if (skill != null)
			{
				var hashCode = skill.GetHashCode();
				if (SkillModifierGetters.TryGetValue(hashCode, out var func))
					skillModifier = func(skillUser);
				else
				{
					// getter not found
					if (!WarnMissingSkillGetterOnceList.Contains(hashCode))
					{
						GeneralUtility.Message($"WARNING: Adjustable Leveling could not find skill getter for '{skill?.Name}' in {nameof(SkillModifierGetters)}, defaulting to '{skillUser}'-modifier [will only warn once]", false, Colors.Yellow);
						WarnMissingSkillGetterOnceList.Add(hashCode);
					}
				}
			}

			// get user modifier
			var userModifier = skillUser switch
			{
				// player skill modifier
				SkillUserEnum.Player => PlayerSkillXPModifier,
				// clan skill modifier
				SkillUserEnum.Clan => ClanSkillXPModifier,
				// companion skill modifier
				SkillUserEnum.Companion => CompanionSkillXPModifier,
				// NPC skill modifier
				_ => NPCSkillXPModifier,
			};
#if DEBUG_PRINT
			GeneralUtility.Message($"{nameof(GetSkillModifier)}: {skillUser} -> {userModifier} (user) * {skillModifier} (skill) = {userModifier * skillModifier} (total)", false);
#endif
			return userModifier * skillModifier;
		}
		#endregion

		#region PRIVATE METHODS
		private SkillUserEnum GetSkillUser(Hero hero)
		{
			var output = SkillUserEnum.NPC;
			if (hero != null)
			{
				if (hero.CharacterObject?.IsPlayerCharacter == true)
					output = SkillUserEnum.Player;
				else if (hero.CompanionOf != null)
					output = SkillUserEnum.Companion;
				else if (hero.Clan == Clan.PlayerClan)
					output = SkillUserEnum.Clan;
			}
#if DEBUG_PRINT
			GeneralUtility.Message($"{nameof(GetSkillUser)}: '{hero}' '{hero?.Clan}' / '{hero?.MapFaction}' -> {output}", false);
#endif
			return output;
		}

		private void InitializeSkillModifierGetters()
		{
			// clear previous skill modifier getters just in case
			SkillModifierGetters.Clear();

			// initialize skill modifier getters from skill object getters
			foreach (var item in SkillObjectGetters)
			{
				var id = item.Key;
				try
				{
					var skill = item.Value();
					var hashCode = skill.GetHashCode();
					SkillModifierGetters[hashCode] = skillUser =>
					{
						var found = false;
						var modifier = 1f;
						switch (skillUser)
						{
							// Player skill modifier
							case SkillUserEnum.Player:
								if (SkillXPModifiers.TryGetValue(PlayerTag + id, out modifier))
									found = true;
								break;
							// NPC skill modifier
							default:
							case SkillUserEnum.NPC:
								if (SkillXPModifiers.TryGetValue(NPCTag + id, out modifier))
									found = true;
								break;
							// Clan skill modifier
							case SkillUserEnum.Clan:
								if (SkillXPModifiers.TryGetValue(ClanTag + id, out modifier))
									found = true;
								break;
							// Companion skill modifier
							case SkillUserEnum.Companion:
								if (SkillXPModifiers.TryGetValue(CompanionTag + id, out modifier))
									found = true;
								break;
						}

						// check if skill was found, show warning and return 1
						if (!found && !WarnMissingSkillModifierOnceList.Contains(hashCode))
						{
							GeneralUtility.Message($"WARNING: Adjustable Leveling could not find skill xp modifier '{id}' ({skill.Name}) for '{skillUser}' in {nameof(SkillXPModifiers)}, defaulting 1x [will only warn once]", false, Colors.Yellow);
							WarnMissingSkillModifierOnceList.Add(hashCode);
						}
#if DEBUG_PRINT
						GeneralUtility.Message($"{nameof(SkillModifierGetters)}: {skillUser} {skill.Name} -> {modifier}", false);
#endif
						return modifier;
					};
				}
				catch (Exception exception)
				{
					GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(InitializeSkillModifierGetters)}): id: {id}" +
						$"\n{exception.GetType()}: {exception.Message}\n{exception.StackTrace}");
				}
			}
		}
		#endregion
	}
}