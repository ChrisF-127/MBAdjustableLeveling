using MCM.Abstractions.FluentBuilder;
using MCM.Common;

namespace AdjustableLeveling
{
	public class MCMSettings
	{
		#region PROPERTIES
		public static MCMSettings Settings { get; set; }

		public ISettingsBuilder SettingsBuilder { get; }

		public string Id => "AdjustableLeveling";
		public string DisplayName => "Adjustable Leveling";
		public string FolderName => "Global/AdjustableLeveling";
		public string FormatType => "json";

		public int GroupOrder { get; set; } = 0;
		public int PropertyOrderCharacterLeveling { get; set; } = 0;
		public int PropertyOrderSkillLeveling { get; set; } = 0;
		public int PropertyOrderNPCSkillLeveling { get; set; } = 0;
		public int PropertyOrderClanSkillLeveling { get; set; } = 0;
		public int PropertyOrderOther { get; set; } = 0;
		public int PropertyOrderSmithing { get; set; } = 0;

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
		#region VIGOR
		public float SkillXPModifier_OneHanded { get; set; } = 0f;
		public float SkillXPModifier_TwoHanded { get; set; } = 0f;
		public float SkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		public float SkillXPModifier_Bow { get; set; } = 0f;
		public float SkillXPModifier_Crossbow { get; set; } = 0f;
		public float SkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		public float SkillXPModifier_Riding { get; set; } = 0f;
		public float SkillXPModifier_Athletics { get; set; } = 0f;
		public float SkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		public float SkillXPModifier_Scouting { get; set; } = 0f;
		public float SkillXPModifier_Tactics { get; set; } = 0f;
		public float SkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		public float SkillXPModifier_Charm { get; set; } = 0f;
		public float SkillXPModifier_Leadership { get; set; } = 0f;
		public float SkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		public float SkillXPModifier_Steward { get; set; } = 0f;
		public float SkillXPModifier_Medicine { get; set; } = 0f;
		public float SkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
		#endregion

		#region NPC SKILL MODIFIERS
		#region VIGOR
		public float NPCSkillXPModifier_OneHanded { get; set; } = 0f;
		public float NPCSkillXPModifier_TwoHanded { get; set; } = 0f;
		public float NPCSkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		public float NPCSkillXPModifier_Bow { get; set; } = 0f;
		public float NPCSkillXPModifier_Crossbow { get; set; } = 0f;
		public float NPCSkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		public float NPCSkillXPModifier_Riding { get; set; } = 0f;
		public float NPCSkillXPModifier_Athletics { get; set; } = 0f;
		public float NPCSkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		public float NPCSkillXPModifier_Scouting { get; set; } = 0f;
		public float NPCSkillXPModifier_Tactics { get; set; } = 0f;
		public float NPCSkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		public float NPCSkillXPModifier_Charm { get; set; } = 0f;
		public float NPCSkillXPModifier_Leadership { get; set; } = 0f;
		public float NPCSkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		public float NPCSkillXPModifier_Steward { get; set; } = 0f;
		public float NPCSkillXPModifier_Medicine { get; set; } = 0f;
		public float NPCSkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
		#endregion

		#region CLAN SKILL MODIFIERS
		#region VIGOR
		public float ClanSkillXPModifier_OneHanded { get; set; } = 0f;
		public float ClanSkillXPModifier_TwoHanded { get; set; } = 0f;
		public float ClanSkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		public float ClanSkillXPModifier_Bow { get; set; } = 0f;
		public float ClanSkillXPModifier_Crossbow { get; set; } = 0f;
		public float ClanSkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		public float ClanSkillXPModifier_Riding { get; set; } = 0f;
		public float ClanSkillXPModifier_Athletics { get; set; } = 0f;
		public float ClanSkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		public float ClanSkillXPModifier_Scouting { get; set; } = 0f;
		public float ClanSkillXPModifier_Tactics { get; set; } = 0f;
		public float ClanSkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		public float ClanSkillXPModifier_Charm { get; set; } = 0f;
		public float ClanSkillXPModifier_Leadership { get; set; } = 0f;
		public float ClanSkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		public float ClanSkillXPModifier_Steward { get; set; } = 0f;
		public float ClanSkillXPModifier_Medicine { get; set; } = 0f;
		public float ClanSkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
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
			const string SkillLevelingSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_Skills}Skills";
			const string OverrideHintText = "{=adjlvl_hint_SkillOverride}HINT_TEXT";
			
			const string SkillLevelingNPCSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_NPCSkills}NPC Skills";
			const string NPCOverrideHintText = "{=adjlvl_hint_NPCSkillOverride}HINT_TEXT";

			const string SkillLevelingClanSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_ClanSkills}Clan Member or Companion Skills";
			const string ClanOverrideHintText = "{=adjlvl_hint_ClanSkillOverride}HINT_TEXT";

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
						.SetHintText("{=adjlvl_hint_FasterLevelingCurve}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++))

					.AddInteger(
						nameof(MaxCharacterLevel),
						"{=adjlvl_name_MaxCharacterLevel}Maximum Character Level",
						5,
						1024,
						new ProxyRef<int>(() => MaxCharacterLevel, v => MaxCharacterLevel = v), b => b
						.SetHintText("{=adjlvl_hint_MaxCharacterLevel}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddFloatingInteger(
						nameof(LevelXPModifier),
						"{=adjlvl_name_CharacterLevelXPModifier}Character Level XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => LevelXPModifier, v => LevelXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_CharacterLevelXPModifier}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0.00"))

					.AddInteger(
						nameof(LevelsPerAttributePoint),
						"{=adjlvl_name_LevelsPerAttributePoint}Levels per Attribute Point",
						1,
						10,
						new ProxyRef<int>(() => LevelsPerAttributePoint, v => LevelsPerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LevelsPerAttributePoint}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsPerLevel),
						"{=adjlvl_name_FocusPointsPerLevel}Focus Points per Level",
						1,
						10,
						new ProxyRef<int>(() => FocusPointsPerLevel, v => FocusPointsPerLevel = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsPerLevel}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(MaxAttribute),
						"{=adjlvl_name_MaxAttributePointsForAttribute}Max Attribute Points for Attribute",
						1,
						1000,
						new ProxyRef<int>(() => MaxAttribute, v => MaxAttribute = v), b => b
						.SetHintText("{=adjlvl_hint_MaxAttributePointsForAttribute}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(MaxFocusPerSkill),
						"{=adjlvl_name_MaxFocusPointsForSkill}Max Focus Points for Skill",
						1,
						1000,
						new ProxyRef<int>(() => MaxFocusPerSkill, v => MaxFocusPerSkill = v), b => b
						.SetHintText("{=adjlvl_hint_MaxFocusPointsForSkill}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(AttributePointsAtStart),
						"{=adjlvl_name_AttributePointsAtStart}Attribute Points at Start",
						1,
						100,
						new ProxyRef<int>(() => AttributePointsAtStart, v => AttributePointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_AttributePointsAtStart}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsAtStart),
						"{=adjlvl_name_FocusPointsAtStart}Focus Points at Start",
						1,
						100,
						new ProxyRef<int>(() => FocusPointsAtStart, v => FocusPointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsAtStart}HINT_TEXT")
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0"))
					)
			#endregion

			#region SKILL LEVELING MODIFIERS
			#region GENERAL
				.CreateGroup("{=adjlvl_group_SkillLeveling}Skill Leveling", g => g
					.SetGroupOrder(GroupOrder++)
					.AddInteger(
						nameof(LearningLimitIncreasePerAttributePoint),
						"{=adjlvl_name_LearningLimitAttributePoint}Learning Limit Increase per Attribute Point",
						0,
						50,
						new ProxyRef<int>(() => LearningLimitIncreasePerAttributePoint, v => LearningLimitIncreasePerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitAttributePoint}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(LearningLimitIncreasePerFocusPoint),
						"{=adjlvl_name_LearningLimitFocusPoint}Learning Limit Increase per Focus Point",
						0,
						100,
						new ProxyRef<int>(() => LearningLimitIncreasePerFocusPoint, v => LearningLimitIncreasePerFocusPoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitFocusPoint}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(BaseLearningLimit),
						"{=adjlvl_name_BaseLearningLimit}Base Learning Limit",
						0,
						100,
						new ProxyRef<int>(() => BaseLearningLimit, v => BaseLearningLimit = v), b => b
						.SetHintText("{=adjlvl_hint_BaseLearningLimit}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0"))

					.AddFloatingInteger(
						nameof(MinLearningRate),
						"{=adjlvl_name_MinLearningRate}Minimum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MinLearningRate, v => MinLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MinLearningRate}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(MaxLearningRate),
						"{=adjlvl_name_MaxLearningRate}Maximum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MaxLearningRate, v => MaxLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MaxLearningRate}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))

					.AddFloatingInteger(
						nameof(SkillXPModifier),
						"{=adjlvl_name_SkillXPModifier}Skill XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier, v => SkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_SkillXPModifier}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier),
						"{=adjlvl_name_NPCSkillXPModifier}NPC Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier, v => NPCSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_NPCSkillXPModifier}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier),
						"{=adjlvl_name_ClanSkillXPModifier}Clan/Companion Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier, v => ClanSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_ClanSkillXPModifier}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddBool(
						nameof(ClanAsCompanionOnly),
						"{=adjlvl_name_ClanAsCompanionOnly}Clan Modifiers affect Companions only",
						new ProxyRef<bool>(() => ClanAsCompanionOnly, v => ClanAsCompanionOnly = v), b => b
						.SetHintText("{=adjlvl_hint_ClanAsCompanionOnly}HINT_TEXT")
						.SetOrder(PropertyOrderSkillLeveling++))
					)
			#endregion
			#region VIGOR
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_OneHanded),
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_OneHanded, v => SkillXPModifier_OneHanded = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_TwoHanded),
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_TwoHanded, v => SkillXPModifier_TwoHanded = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Polearm),
						"{=haax8kMa}Polearm",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Polearm, v => SkillXPModifier_Polearm = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CONTROL
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_Bow),
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Bow, v => SkillXPModifier_Bow = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Crossbow),
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Crossbow, v => SkillXPModifier_Crossbow = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Throwing),
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Throwing, v => SkillXPModifier_Throwing = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region ENDURANCE
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_Riding),
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Riding, v => SkillXPModifier_Riding = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Athletics),
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Athletics, v => SkillXPModifier_Athletics = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Crafting),
						"{=smithingskill}Smithing",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Crafting, v => SkillXPModifier_Crafting = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CUNNING
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_Scouting),
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Scouting, v => SkillXPModifier_Scouting = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Tactics),
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Tactics, v => SkillXPModifier_Tactics = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Roguery),
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Roguery, v => SkillXPModifier_Roguery = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region SOCIAL
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_Charm),
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Charm, v => SkillXPModifier_Charm = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Leadership),
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Leadership, v => SkillXPModifier_Leadership = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Trade),
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Trade, v => SkillXPModifier_Trade = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region INTELLIGENCE
				.CreateGroup(SkillLevelingSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(SkillXPModifier_Steward),
						"{=stewardskill}Steward",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Steward, v => SkillXPModifier_Steward = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Medicine),
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Medicine, v => SkillXPModifier_Medicine = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SkillXPModifier_Engineering),
						"{=engineeringskill}Engineering",
						0f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier_Engineering, v => SkillXPModifier_Engineering = v), b => b
						.SetHintText(OverrideHintText)
						.SetOrder(PropertyOrderSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#endregion

			#region NPC SKILL MODIFIERS
			#region VIGOR
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_OneHanded),
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_OneHanded, v => NPCSkillXPModifier_OneHanded = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_TwoHanded),
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_TwoHanded, v => NPCSkillXPModifier_TwoHanded = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Polearm),
						"{=haax8kMa}Polearm",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Polearm, v => NPCSkillXPModifier_Polearm = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CONTROL
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Bow),
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Bow, v => NPCSkillXPModifier_Bow = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Crossbow),
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Crossbow, v => NPCSkillXPModifier_Crossbow = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Throwing),
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Throwing, v => NPCSkillXPModifier_Throwing = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region ENDURANCE
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Riding),
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Riding, v => NPCSkillXPModifier_Riding = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Athletics),
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Athletics, v => NPCSkillXPModifier_Athletics = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Crafting),
						"{=smithingskill}Smithing",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Crafting, v => NPCSkillXPModifier_Crafting = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CUNNING
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Scouting),
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Scouting, v => NPCSkillXPModifier_Scouting = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Tactics),
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Tactics, v => NPCSkillXPModifier_Tactics = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Roguery),
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Roguery, v => NPCSkillXPModifier_Roguery = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region SOCIAL
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Charm),
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Charm, v => NPCSkillXPModifier_Charm = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Leadership),
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Leadership, v => NPCSkillXPModifier_Leadership = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Trade),
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Trade, v => NPCSkillXPModifier_Trade = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region INTELLIGENCE
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Steward),
						"{=stewardskill}Steward",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Steward, v => NPCSkillXPModifier_Steward = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Medicine),
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Medicine, v => NPCSkillXPModifier_Medicine = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderNPCSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier_Engineering),
						"{=engineeringskill}Engineering",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier_Engineering, v => NPCSkillXPModifier_Engineering = v), b => b
						.SetHintText(NPCOverrideHintText)
						.SetOrder(PropertyOrderCharacterLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#endregion

			#region CLAN SKILL MODIFIERS
			#region VIGOR
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_OneHanded),
						"{=PiHpR4QL}One Handed",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_OneHanded, v => ClanSkillXPModifier_OneHanded = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_TwoHanded),
						"{=t78atYqH}Two Handed",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_TwoHanded, v => ClanSkillXPModifier_TwoHanded = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Polearm),
						"{=haax8kMa}Polearm",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Polearm, v => ClanSkillXPModifier_Polearm = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CONTROL
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Bow),
						"{=5rj7xQE4}Bow",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Bow, v => ClanSkillXPModifier_Bow = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Crossbow),
						"{=TTWL7RLe}Crossbow",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Crossbow, v => ClanSkillXPModifier_Crossbow = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Throwing),
						"{=2wclahIJ}Throwing",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Throwing, v => ClanSkillXPModifier_Throwing = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region ENDURANCE
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Riding),
						"{=p9i3zRm9}Riding",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Riding, v => ClanSkillXPModifier_Riding = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Athletics),
						"{=skZS2UlW}Athletics",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Athletics, v => ClanSkillXPModifier_Athletics = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Crafting),
						"{=smithingskill}Smithing",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Crafting, v => ClanSkillXPModifier_Crafting = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region CUNNING
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Scouting),
						"{=LJ6Krlbr}Scouting",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Scouting, v => ClanSkillXPModifier_Scouting = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Tactics),
						"{=m8o51fc7}Tactics",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Tactics, v => ClanSkillXPModifier_Tactics = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Roguery),
						"{=V0ZMJ0PX}Roguery",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Roguery, v => ClanSkillXPModifier_Roguery = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region SOCIAL
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Charm),
						"{=EGeY1gfs}Charm",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Charm, v => ClanSkillXPModifier_Charm = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Leadership),
						"{=HsLfmEmb}Leadership",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Leadership, v => ClanSkillXPModifier_Leadership = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Trade),
						"{=GmcgoiGy}Trade",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Trade, v => ClanSkillXPModifier_Trade = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
			#region INTELLIGENCE
				.CreateGroup(SkillLevelingClanSkillsGroupName, g => g
					.SetGroupOrder(GroupOrder++)
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Steward),
						"{=stewardskill}Steward",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Steward, v => ClanSkillXPModifier_Steward = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Medicine),
						"{=JKH59XNp}Medicine",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Medicine, v => ClanSkillXPModifier_Medicine = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier_Engineering),
						"{=engineeringskill}Engineering",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier_Engineering, v => ClanSkillXPModifier_Engineering = v), b => b
						.SetHintText(ClanOverrideHintText)
						.SetOrder(PropertyOrderClanSkillLeveling++)
						.AddValueFormat("0.00"))
					)
			#endregion
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
						.SetHintText("{=adjlvl_hint_TroopXPModifier}HINT_TEXT")
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
						.SetHintText("{=adjlvl_hint_PartResearch}HINT_TEXT")
						.SetOrder(PropertyOrderSmithing++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SmithingFreeBuildResearchModifier),
						"{=adjlvl_name_FreeBuildPartResearch}Free Build Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingFreeBuildResearchModifier, v => SmithingFreeBuildResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_FreeBuildPartResearch}HINT_TEXT")
						.SetOrder(PropertyOrderSmithing++)
						.AddValueFormat("0.00"))
					);
			#endregion

			var globalSettings = SettingsBuilder.BuildAsGlobal();
			globalSettings.Register();
			//globalSettings.Unregister();
		}
		#endregion
	}
}