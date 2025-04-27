using AdjustableLeveling.Leveling;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace AdjustableLeveling.Settings
{
	public class MCMSettings
	{
		#region CONSTANTS
		public const string BaseTag = "Base_";
		public const string NPCTag = "NPC_";
		public const string ClanTag = "Clan_";

		private const string SkillLevelingSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_Skills}Skills";
		private const string BaseOverrideHintText = "{=adjlvl_hint_SkillOverride}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for this specific skill, when not 0. [Default: 0.00]";

		private const string SkillLevelingNPCSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_NPCSkills}NPC Skills";
		private const string NPCOverrideHintText = "{=adjlvl_hint_NPCSkillOverride}Overrides modifiers for this specific skill for NPCs only, when not 0. [Default: 0.00]";

		private const string SkillLevelingClanSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_ClanSkills}Clan Member or Companion Skills";
		private const string ClanOverrideHintText = "{=adjlvl_hint_ClanSkillOverride}Overrides modifiers for this specific skill for clan members or companions only, when not 0. [Default: 0.00]";
		#endregion

		#region PROPERTIES
		public static MCMSettings Settings { get; set; }

		public ISettingsBuilder SettingsBuilder { get; }
		public FluentGlobalSettings GlobalSettings { get; private set; }
		public FluentPerCampaignSettings PerCampaignSettings { get; private set; }

		public string Id => "AdjustableLeveling";
		public string DisplayName => "Adjustable Leveling";
		public string FolderName => "Global/AdjustableLeveling";
		public string FormatType => "json";

		private ISettingsPropertyGroupBuilder BaseSkillsGroupBuilder { get; set; }
		private ISettingsPropertyGroupBuilder NPCSkillsGroupBuilder { get; set; }
		private ISettingsPropertyGroupBuilder ClanSkillsGroupBuilder { get; set; }

		private int GroupOrder { get; set; } = 0;
		private int PropertyOrderCharacterLeveling { get; set; } = 0;
		private int BaseSkillLevelingPropertyOrder { get; set; } = 0;
		private int NPCSkillLevelingPropertyOrder { get; set; } = 0;
		private int ClanSkillLevelingPropertyOrder { get; set; } = 0;
		private int PropertyOrderOther { get; set; } = 0;
		private int PropertyOrderSmithing { get; set; } = 0;

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

		public float SkillXPModifier { get; set; } = 1f;
		public float NPCSkillXPModifier { get; set; } = 0f;
		public float ClanSkillXPModifier { get; set; } = 0f;
		public bool ClanAsCompanionOnly { get; set; } = false;
		#endregion

		#region SKILL MODIFIERS
		public Dictionary<string, float> SkillXPModifiers { get; } = [];
		#endregion
		#endregion

		#region OTHER STUFF
		public float TroopXPModifier { get; set; } = 1f;
		#endregion

		#region SMITHING PART RESEARCH MODIFIERS
		public float SmithingResearchModifier { get; set; } = 1f;
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
		#endregion
		#endregion

		#region CONSTRUCTORS
		public MCMSettings()
		{
			#region SETTINGS
			SettingsBuilder = BaseSettingsBuilder.Create(Id, DisplayName)
				.SetFormat(FormatType)
				.SetFolderName(FolderName)
				.SetSubFolder(string.Empty)

			#region CHARACTER LEVELING MODIFIERS
				.CreateGroup("{=adjlvl_group_CharacterLeveling}Character Leveling", g => g
					.SetGroupOrder(GroupOrder++)
					.AddBool(
						nameof(UseFasterLevelingCurve),
						"{=adjlvl_name_FasterLevelingCurve}Faster Leveling Curve",
						new ProxyRef<bool>(() => UseFasterLevelingCurve, v => UseFasterLevelingCurve = v), b => b
						.SetHintText("{=adjlvl_hint_FasterLevelingCurve}Slower earlier but faster later levels, level 62 total: 40.7m [ON] vs 95.4m [OFF]. [Default: OFF]\n-WARNING: Backup save recommended, changing this in an ongoing save will reset the level xp to half-way to the next level (if total xp is out of bounds for the current level after conversion)!")
						.SetOrder(PropertyOrderCharacterLeveling++))

					.AddInteger(
						nameof(MaxCharacterLevel),
						"{=adjlvl_name_MaxCharacterLevel}Maximum Character Level",
						5,
						1024,
						new ProxyRef<int>(() => MaxCharacterLevel, v => MaxCharacterLevel = v), b => b
						.SetHintText("{=adjlvl_hint_MaxCharacterLevel}Adjust the maximum achievable character level. Higher levels require much more xp! [Default: 62]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddFloatingInteger(
						nameof(LevelXPModifier),
						"{=adjlvl_name_CharacterLevelXPModifier}Character Level XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => LevelXPModifier, v => LevelXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_CharacterLevelXPModifier}Adjust how skill xp is converted into level xp, default is 1-to-1 at 1.00. [Default: 1.00]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0.00"))

					.AddInteger(
						nameof(LevelsPerAttributePoint),
						"{=adjlvl_name_LevelsPerAttributePoint}Levels per Attribute Point",
						1,
						10,
						new ProxyRef<int>(() => LevelsPerAttributePoint, v => LevelsPerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LevelsPerAttributePoint}Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Default: 4]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsPerLevel),
						"{=adjlvl_name_FocusPointsPerLevel}Focus Points per Level",
						1,
						10,
						new ProxyRef<int>(() => FocusPointsPerLevel, v => FocusPointsPerLevel = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsPerLevel}Focus points gained per level. [Default: 1]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(MaxAttribute),
						"{=adjlvl_name_MaxAttributePointsForAttribute}Max Attribute Points for Attribute",
						1,
						1000,
						new ProxyRef<int>(() => MaxAttribute, v => MaxAttribute = v), b => b
						.SetHintText("{=adjlvl_hint_MaxAttributePointsForAttribute}Attribute point limit per attribute. [Default: 10]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(MaxFocusPerSkill),
						"{=adjlvl_name_MaxFocusPointsForSkill}Max Focus Points for Skill",
						1,
						1000,
						new ProxyRef<int>(() => MaxFocusPerSkill, v => MaxFocusPerSkill = v), b => b
						.SetHintText("{=adjlvl_hint_MaxFocusPointsForSkill}Focus point limit per skill. (UI will at most show 5 points) [Default: 5]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(AttributePointsAtStart),
						"{=adjlvl_name_AttributePointsAtStart}Attribute Points at Start",
						1,
						100,
						new ProxyRef<int>(() => AttributePointsAtStart, v => AttributePointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_AttributePointsAtStart}Apparently affects the attribute points with which NPCs start, but not the player. [Default: 15]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsAtStart),
						"{=adjlvl_name_FocusPointsAtStart}Focus Points at Start",
						1,
						100,
						new ProxyRef<int>(() => FocusPointsAtStart, v => FocusPointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsAtStart}Apparently affects the focus points with which NPCs start, but not the player. [Default: 5]")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					)
			#endregion

			#region SKILL LEVELING SETTINGS
				.CreateGroup("{=adjlvl_group_SkillLeveling}Skill Leveling", g => g
					.SetGroupOrder(GroupOrder++)
					.AddInteger(
						nameof(LearningLimitIncreasePerAttributePoint),
						"{=adjlvl_name_LearningLimitAttributePoint}Learning Limit Increase per Attribute Point",
						0,
						50,
						new ProxyRef<int>(() => LearningLimitIncreasePerAttributePoint, v => LearningLimitIncreasePerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitAttributePoint}E.g. at 3 and with 10 AP an additional 30 skill points can be gained at reducing learning rate; at 5 an additional 50 can be gained. [Default: 3]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(LearningLimitIncreasePerFocusPoint),
						"{=adjlvl_name_LearningLimitFocusPoint}Learning Limit Increase per Focus Point",
						0,
						100,
						new ProxyRef<int>(() => LearningLimitIncreasePerFocusPoint, v => LearningLimitIncreasePerFocusPoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitFocusPoint}Adjust the learning limit increase per focus point. [Default: 50]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(BaseLearningLimit),
						"{=adjlvl_name_BaseLearningLimit}Base Learning Limit",
						0,
						100,
						new ProxyRef<int>(() => BaseLearningLimit, v => BaseLearningLimit = v), b => b
						.SetHintText("{=adjlvl_hint_BaseLearningLimit}The base learning limit. [Default: 50]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddFloatingInteger(
						nameof(MinLearningRate),
						"{=adjlvl_name_MinLearningRate}Minimum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MinLearningRate, v => MinLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MinLearningRate}Set a minimum learning rate. [Default: 0.00]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(MaxLearningRate),
						"{=adjlvl_name_MaxLearningRate}Maximum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MaxLearningRate, v => MaxLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MaxLearningRate}Set a maximum learning rate, zero disables it. [Default: 0.00]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddFloatingInteger(
						nameof(SkillXPModifier),
						"{=adjlvl_name_SkillXPModifier}Skill XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier, v => SkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_SkillXPModifier}Adjust the overall skill learning rate. [Default: 1.00]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier),
						"{=adjlvl_name_NPCSkillXPModifier}NPC Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier, v => NPCSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_NPCSkillXPModifier}Overrides 'Skill XP Modifier' for NPCs, when not 0. [Default: 0.00]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier),
						"{=adjlvl_name_ClanSkillXPModifier}Clan/Companion Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier, v => ClanSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_ClanSkillXPModifier}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for clan members or companions, when not 0. [Default: 0.00]")
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddBool(
						nameof(ClanAsCompanionOnly),
						"{=adjlvl_name_ClanAsCompanionOnly}Clan Modifiers affect Companions only",
						new ProxyRef<bool>(() => ClanAsCompanionOnly, v => ClanAsCompanionOnly = v), b => b
						.SetHintText("{=adjlvl_hint_ClanAsCompanionOnly}Clan modifiers only affect Companions. [Default: OFF]")
						.SetOrder(BaseSkillLevelingPropertyOrder++))
					)

				// Skill Leveling Modifier Sub-Groups
				.CreateGroup(SkillLevelingSkillsGroupName, g => 
					BaseSkillsGroupBuilder = g.SetGroupOrder(GroupOrder++))
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => 
					NPCSkillsGroupBuilder = g.SetGroupOrder(GroupOrder++))
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => 
					ClanSkillsGroupBuilder = g.SetGroupOrder(GroupOrder++))
			#endregion

			#region OTHER STUFF
				.CreateGroup("{=adjlvl_group_Other}Other", g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(TroopXPModifier),
						"{=adjlvl_name_TroopXPModifier}Troop XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => TroopXPModifier, v => TroopXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_TroopXPModifier}Modifies XP gained for upgrading troops. (Required XP numbers in roster will show unmodified values, but dropping equipment is limited to modified value.) [Default 1.00]")
						.SetOrder(PropertyOrderOther++)
						.AddValueFormat("0.00"))
					)
			#endregion

			#region SMITHING PART RESEARCH MODIFIERS
				.CreateGroup("{=adjlvl_group_SmithingResearch}Smithing Research", g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SmithingResearchModifier),
						"{=adjlvl_name_PartResearch}Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingResearchModifier, v => SmithingResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_PartResearch}Adjust smithing part research gain rate for smithing and smelting weapons. [Default: 100%]")
						.SetOrder(PropertyOrderSmithing++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SmithingFreeBuildResearchModifier),
						"{=adjlvl_name_FreeBuildPartResearch}Free Build Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingFreeBuildResearchModifier, v => SmithingFreeBuildResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_FreeBuildPartResearch}Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Default: 10%]")
						.SetOrder(PropertyOrderSmithing++)
						.AddValueFormat("0.00"))
					);
			#endregion

			#region SKILL LEVELING MODIFIERS
			// VIGOR
			AddSkill(DefaultSkills.OneHanded, "OneHanded", "{=PiHpR4QL}One Handed");
			AddSkill(DefaultSkills.TwoHanded, "TwoHanded", "{=t78atYqH}Two Handed");
			AddSkill(DefaultSkills.Polearm, "Polearm", "{=haax8kMa}Polearm");
			// CONTROL
			AddSkill(DefaultSkills.Bow, "Bow", "{=5rj7xQE4}Bow");
			AddSkill(DefaultSkills.Crossbow, "Crossbow", "{=TTWL7RLe}Crossbow");
			AddSkill(DefaultSkills.Throwing, "Throwing", "{=2wclahIJ}Throwing");
			// ENDURANCE
			AddSkill(DefaultSkills.Riding, "Riding", "{=p9i3zRm9}Riding");
			AddSkill(DefaultSkills.Athletics, "Athletics", "{=skZS2UlW}Athletics");
			AddSkill(DefaultSkills.Crafting, "Crafting", "{=smithingskill}Smithing");
			// CUNNING
			AddSkill(DefaultSkills.Scouting, "Scouting", "{=LJ6Krlbr}Scouting");
			AddSkill(DefaultSkills.Tactics, "Tactics", "{=m8o51fc7}Tactics");
			AddSkill(DefaultSkills.Roguery, "Roguery", "{=V0ZMJ0PX}Roguery");
			// SOCIAL
			AddSkill(DefaultSkills.Charm, "Charm", "{=EGeY1gfs}Charm");
			AddSkill(DefaultSkills.Leadership, "Leadership", "{=HsLfmEmb}Leadership");
			AddSkill(DefaultSkills.Trade, "Trade", "{=GmcgoiGy}Trade");
			// INTELLIGENCE
			AddSkill(DefaultSkills.Steward, "Steward", "{=stewardskill}Steward");
			AddSkill(DefaultSkills.Medicine, "Medicine", "{=JKH59XNp}Medicine");
			AddSkill(DefaultSkills.Engineering, "Engineering", "{=engineeringskill}Engineering");
			#endregion
			#endregion

			// create global settings
			GlobalSettings = SettingsBuilder.BuildAsGlobal();
			// create per campaign settings
			PerCampaignSettings = SettingsBuilder.BuildAsPerCampaign();

			// register global settings
			GlobalSettings.Register();
		}
		#endregion

		#region METHODS
		public void RegisterGlobal()
		{
			PerCampaignSettings.Unregister();
			GlobalSettings.Register();
		}
		public void RegisterPerCampaign()
		{
			GlobalSettings.Unregister();
			PerCampaignSettings.Register();
		}

		public void AddSkill(SkillObject skill, string id, string name)
		{
			// Base settings
			SkillXPModifiers.Add(BaseTag + id, 1f);
			BaseSkillsGroupBuilder.AddFloatingInteger(
				"SkillXPModifier_" + id,
				name,
				0f,
				100f,
				CreateSkillProxy(BaseTag + id), b => b
				.SetHintText(BaseOverrideHintText)
				.SetOrder(BaseSkillLevelingPropertyOrder++)
				.AddValueFormat("0.00"));

			// NPC settings
			SkillXPModifiers.Add(NPCTag + id, 0f);
			NPCSkillsGroupBuilder.AddFloatingInteger(
				"NPCSkillXPModifier_" + id,
				name,
				0f,
				100f,
				CreateSkillProxy(NPCTag + id), b => b
				.SetHintText(NPCOverrideHintText)
				.SetOrder(NPCSkillLevelingPropertyOrder++)
				.AddValueFormat("0.00"));

			// Clan settings
			SkillXPModifiers.Add(ClanTag + id, 0f);
			ClanSkillsGroupBuilder.AddFloatingInteger(
				"ClanSkillXPModifier_" + id,
				name,
				0f,
				100f,
				CreateSkillProxy(ClanTag + id), b => b
				.SetHintText(ClanOverrideHintText)
				.SetOrder(ClanSkillLevelingPropertyOrder++)
				.AddValueFormat("0.00"));

			// Add to SkillHelper
			SkillHelper.AddSkill(id, skill);

			ProxyRef<float> CreateSkillProxy(string name) =>
				new(() => SkillXPModifiers[name], v => SkillXPModifiers[name] = v);
		}
		#endregion
	}
}