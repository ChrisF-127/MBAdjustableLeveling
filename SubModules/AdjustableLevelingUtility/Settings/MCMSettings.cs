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
		public Dictionary<string, float> SkillXPModifiers { get; } = new Dictionary<string, float>
		{
			// BASE SKILL MODIFIERS
			// VIGOR
			[BaseTag + "OneHanded"] = 0f,
			[BaseTag + "TwoHanded"] = 0f,
			[BaseTag + "Polearm"] = 0f,
			// CONTROL
			[BaseTag + "Bow"] = 0f,
			[BaseTag + "Crossbow"] = 0f,
			[BaseTag + "Throwing"] = 0f,
			// ENDURANCE
			[BaseTag + "Riding"] = 0f,
			[BaseTag + "Athletics"] = 0f,
			[BaseTag + "Crafting"] = 0f,
			// CUNNING
			[BaseTag + "Scouting"] = 0f,
			[BaseTag + "Tactics"] = 0f,
			[BaseTag + "Roguery"] = 0f,
			// SOCIAL
			[BaseTag + "Charm"] = 0f,
			[BaseTag + "Leadership"] = 0f,
			[BaseTag + "Trade"] = 0f,
			// INTELLIGENCE
			[BaseTag + "Steward"] = 0f,
			[BaseTag + "Medicine"] = 0f,
			[BaseTag + "Engineering"] = 0f,

			// NPC SKILL MODIFIERS
			// VIGOR
			[NPCTag + "OneHanded"] = 0f,
			[NPCTag + "TwoHanded"] = 0f,
			[NPCTag + "Polearm"] = 0f,
			// CONTROL
			[NPCTag + "Bow"] = 0f,
			[NPCTag + "Crossbow"] = 0f,
			[NPCTag + "Throwing"] = 0f,
			// ENDURANCE
			[NPCTag + "Riding"] = 0f,
			[NPCTag + "Athletics"] = 0f,
			[NPCTag + "Crafting"] = 0f,
			// CUNNING
			[NPCTag + "Scouting"] = 0f,
			[NPCTag + "Tactics"] = 0f,
			[NPCTag + "Roguery"] = 0f,
			// SOCIAL
			[NPCTag + "Charm"] = 0f,
			[NPCTag + "Leadership"] = 0f,
			[NPCTag + "Trade"] = 0f,
			// INTELLIGENCE
			[NPCTag + "Steward"] = 0f,
			[NPCTag + "Medicine"] = 0f,
			[NPCTag + "Engineering"] = 0f,

			// CLAN SKILL MODIFIERS
			// VIGOR
			[ClanTag + "OneHanded"] = 0f,
			[ClanTag + "TwoHanded"] = 0f,
			[ClanTag + "Polearm"] = 0f,
			// CONTROL
			[ClanTag + "Bow"] = 0f,
			[ClanTag + "Crossbow"] = 0f,
			[ClanTag + "Throwing"] = 0f,
			// ENDURANCE
			[ClanTag + "Riding"] = 0f,
			[ClanTag + "Athletics"] = 0f,
			[ClanTag + "Crafting"] = 0f,
			// CUNNING
			[ClanTag + "Scouting"] = 0f,
			[ClanTag + "Tactics"] = 0f,
			[ClanTag + "Roguery"] = 0f,
			// SOCIAL
			[ClanTag + "Charm"] = 0f,
			[ClanTag + "Leadership"] = 0f,
			[ClanTag + "Trade"] = 0f,
			// INTELLIGENCE
			[ClanTag + "Steward"] = 0f,
			[ClanTag + "Medicine"] = 0f,
			[ClanTag + "Engineering"] = 0f,
		};
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

			#region GENERAL SKILL LEVELING SETTINGS
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
			#endregion
			#region SKILL LEVELING MODIFIERS
				.CreateGroup(SkillLevelingSkillsGroupName, g =>
				{
					BaseSkillsGroupBuilder = g;
					g.SetGroupOrder(GroupOrder++)
					#region VIGOR
					.AddFloatingInteger(
						"SkillXPModifier_OneHanded",
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "OneHanded"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_TwoHanded",
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "TwoHanded"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Polearm",
						"{=haax8kMa}Polearm",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Polearm"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CONTROL
					.AddFloatingInteger(
						"SkillXPModifier_Bow",
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Bow"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Crossbow",
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Crossbow"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Throwing",
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Throwing"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region ENDURANCE
					.AddFloatingInteger(
						"SkillXPModifier_Riding",
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Riding"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Athletics",
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Athletics"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Crafting",
						"{=smithingskill}Smithing",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Crafting"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CUNNING
					.AddFloatingInteger(
						"SkillXPModifier_Scouting",
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Scouting"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Tactics",
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Tactics"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Roguery",
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Roguery"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region SOCIAL
					.AddFloatingInteger(
						"SkillXPModifier_Charm",
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Charm"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Leadership",
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Leadership"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Trade",
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Trade"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region INTELLIGENCE
					.AddFloatingInteger(
						"SkillXPModifier_Steward",
						"{=stewardskill}Steward",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Steward"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Medicine",
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Medicine"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"SkillXPModifier_Engineering",
						"{=engineeringskill}Engineering",
						0f,
						100f,
						CreateSkillProxy(BaseTag + "Engineering"), b => b
						.SetHintText(BaseOverrideHintText)
						.SetOrder(BaseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"));
					#endregion
					}
				)
			#endregion
			#region NPC SKILL MODIFIERS
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g =>
				{
					NPCSkillsGroupBuilder = g;
					g.SetGroupOrder(GroupOrder++)
					#region VIGOR
					.AddFloatingInteger(
						"NPCSkillXPModifier_OneHanded",
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "OneHanded"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_TwoHanded",
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "TwoHanded"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Polearm",
						"{=haax8kMa}Polearm",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Polearm"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CONTROL
					.AddFloatingInteger(
						"NPCSkillXPModifier_Bow",
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Bow"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Crossbow",
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Crossbow"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Throwing",
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Throwing"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region ENDURANCE
					.AddFloatingInteger(
						"NPCSkillXPModifier_Riding",
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Riding"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Athletics",
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Athletics"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Crafting",
						"{=smithingskill}Smithing",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Crafting"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CUNNING
					.AddFloatingInteger(
						"NPCSkillXPModifier_Scouting",
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Scouting"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Tactics",
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Tactics"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Roguery",
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Roguery"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region SOCIAL
					.AddFloatingInteger(
						"NPCSkillXPModifier_Charm",
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Charm"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Leadership",
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Leadership"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Trade",
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Trade"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region INTELLIGENCE
					.AddFloatingInteger(
						"NPCSkillXPModifier_Steward",
						"{=stewardskill}Steward",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Steward"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Medicine",
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Medicine"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(NPCSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"NPCSkillXPModifier_Engineering",
						"{=engineeringskill}Engineering",
						0f,
						100f,
						CreateSkillProxy(NPCTag + "Engineering"), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0.00"));
					#endregion
					}
				)
			#endregion
			#region CLAN SKILL MODIFIERS
				.CreateGroup(SkillLevelingClanSkillsGroupName, g =>
				{
					ClanSkillsGroupBuilder = g;
					g.SetGroupOrder(GroupOrder++)
					#region VIGOR
					.AddFloatingInteger(
						"ClanSkillXPModifier_OneHanded",
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "OneHanded"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_TwoHanded",
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "TwoHanded"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Polearm",
						"{=haax8kMa}Polearm",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Polearm"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CONTROL
					.AddFloatingInteger(
						"ClanSkillXPModifier_Bow",
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Bow"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Crossbow",
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Crossbow"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Throwing",
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Throwing"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region ENDURANCE
					.AddFloatingInteger(
						"ClanSkillXPModifier_Riding",
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Riding"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Athletics",
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Athletics"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Crafting",
						"{=smithingskill}Smithing",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Crafting"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region CUNNING
					.AddFloatingInteger(
						"ClanSkillXPModifier_Scouting",
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Scouting"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Tactics",
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Tactics"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Roguery",
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Roguery"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region SOCIAL
					.AddFloatingInteger(
						"ClanSkillXPModifier_Charm",
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Charm"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Leadership",
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Leadership"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Trade",
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Trade"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					#endregion
					#region INTELLIGENCE
					.AddFloatingInteger(
						"ClanSkillXPModifier_Steward",
						"{=stewardskill}Steward",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Steward"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Medicine",
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Medicine"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						"ClanSkillXPModifier_Engineering",
						"{=engineeringskill}Engineering",
						0f,
						100f,
						CreateSkillProxy(ClanTag + "Engineering"), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(ClanSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"));
					#endregion
					}
				)
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
			#endregion

			GlobalSettings = SettingsBuilder.BuildAsGlobal();
			PerCampaignSettings = SettingsBuilder.BuildAsPerCampaign();

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

		public void AddSkill(string id, SkillObject skill, string name)
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

			SkillHelper.AddSkill(id, skill);
		}

		private ProxyRef<float> CreateSkillProxy(string name) =>
			new(() => SkillXPModifiers[name], v => SkillXPModifiers[name] = v);
		#endregion
	}
}