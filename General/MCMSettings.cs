using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace AdjustableLeveling
{
	public class MCMSettings : AttributeGlobalSettings<MCMSettings>
	{
		public override string Id => "AdjustableLeveling";
		public override string DisplayName => "Adjustable Leveling";
		public override string FolderName => "AdjustableLeveling";
		public override string FormatType => "json";

		#region CHARACTER LEVELING MODIFIERS
		private const string CharacterLevelingGroupName = "{=adjlvl_group_CharacterLeveling}Character Leveling";

		[SettingPropertyBool(
			"{=adjlvl_name_FasterLevelingCurve}Faster Leveling Curve",
			RequireRestart = true,
			HintText = "{=adjlvl_hint_FasterLevelingCurve}Slower earlier but faster later levels, level 62 total: 40.7m [ON] vs 95.4m [OFF]. [Default: OFF]\nWARNING: Backup save recommended, changing this in an ongoing save will reset the level xp to half-way to the next level (if total xp is out of bounds for the current level after conversion)!",
			Order = 0)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public bool UseFasterLevelingCurve { get; set; } = false;
		

		[SettingPropertyInteger(
			"{=adjlvl_name_MaxCharacterLevel}Maximum Character Level",
			5,
			200,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_MaxCharacterLevel}Adjust the maximum achievable character level. Higher levels require much more xp! [Default: 62]",
			Order = 1)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxCharacterLevel { get; set; } = 62;

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_CharacterLevelXPModifier}Character Level XP Modifier",
			0.01f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_CharacterLevelXPModifier}Adjust how skill xp is converted into level xp, default is 1-to-1 at 1.00. [Default: 1.00]",
			Order = 2)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public float LevelXPModifier { get; set; } = 1f;


		[SettingPropertyInteger(
			"{=adjlvl_name_LevelsPerAttributePoint}Levels per Attribute Point",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_LevelsPerAttributePoint}Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Default: 4]",
			Order = 3)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int LevelsPerAttributePoint { get; set; } = 4;

		[SettingPropertyInteger(
			"{=adjlvl_name_FocusPointsPerLevel}Focus Points per Level",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_FocusPointsPerLevel}Focus points gained per level. [Default: 1]",
			Order = 4)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int FocusPointsPerLevel { get; set; } = 1;


		[SettingPropertyInteger(
			"{=adjlvl_name_MaxAttributePointsForAttribute}Max Attribute Points for Attribute",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_MaxAttributePointsForAttribute}Attribute point limit per attribute. [Default: 10]",
			Order = 5)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxAttribute { get; set; } = 10;

		[SettingPropertyInteger(
			"{=adjlvl_name_MaxFocusPointsForSkill}Max Focus Points for Skill",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_MaxFocusPointsForSkill}Focus point limit per skill. (UI will at most show 5 points) [Default: 5]",
			Order = 6)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxFocusPerSkill { get; set; } = 5;


		[SettingPropertyInteger(
			"{=adjlvl_name_AttributePointsAtStart}Attribute Points at Start",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_AttributePointsAtStart}Apparently affects the attribute points with which NPCs start, but not the player. [Default: 15]",
			Order = 7)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int AttributePointsAtStart { get; set; } = 15;

		[SettingPropertyInteger(
			"{=adjlvl_name_FocusPointsAtStart}Focus Points at Start",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_FocusPointsAtStart}Apparently affects the focus points with which NPCs start, but not the player. [Default: 5]",
			Order = 8)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int FocusPointsAtStart { get; set; } = 5;
		#endregion


		#region SKILL LEVELING MODIFIERS
		private const string SkillLevelingGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling";

		#region GENERAL
		[SettingPropertyInteger(
			"{=adjlvl_name_LearningLimitAttributePoint}Learning Limit Increase per Attribute Point",
			0,
			50,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_LearningLimitAttributePoint}E.g. at 3 and with 10 AP an additional 30 skill points can be gained at reducing learning rate; at 5 an additional 50 can be gained. [Default: 3]",
			Order = 0)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int LearningLimitIncreasePerAttributePoint { get; set; } = 3;

		[SettingPropertyInteger(
			"{=adjlvl_name_LearningLimitFocusPoint}Learning Limit Increase per Focus Point",
			0,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_LearningLimitFocusPoint}Adjust the learning limit increase per focus point. [Default: 50]",
			Order = 1)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int LearningLimitIncreasePerFocusPoint { get; set; } = 50;

		[SettingPropertyInteger(
			"{=adjlvl_name_BaseLearningLimit}Base Learning Limit",
			0,
			100,
			"0",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_BaseLearningLimit}The base learning limit. [Default: 50]",
			Order = 2)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int BaseLearningLimit { get; set; } = 50;


		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_MinLearningRate}Minimum Learning Rate",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_MinLearningRate}Set a minimum learning rate. [Default: 0.00]",
			Order = 3)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float MinLearningRate { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_MaxLearningRate}Maximum Learning Rate",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_MaxLearningRate}Set a maximum learning rate, zero disables it. [Default: 0.00]",
			Order = 4)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float MaxLearningRate { get; set; } = 0f;


		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_SkillXPModifier}Skill XP Modifier",
			0.01f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_SkillXPModifier}Adjust the overall skill learning rate. [Default: 1.00]",
			Order = 5)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float SkillXPModifier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_NPCSkillXPModifier}NPC Skill XP Modifier",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_NPCSkillXPModifier}Overrides 'Skill XP Modifier' for NPCs, when not 0. [Default: 0.00]",
			Order = 6)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_ClanSkillXPModifier}Clan/Companion Skill XP Modifier",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_ClanSkillXPModifier}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for clan members or companions, when not 0. [Default: 0.00]",
			Order = 7)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float ClanSkillXPModifier { get; set; } = 0f;

		[SettingPropertyBool(
			"{=adjlvl_name_ClanAsCompanionOnly}Clan Modifiers affect Companions only",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_ClanAsCompanionOnly}Clan modifiers only affect Companions. [Default: OFF]",
			Order = 8)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public bool ClanAsCompanionOnly { get; set; } = false;
		#endregion

		#region SKILL MODIFIERS
		private const string SkillLevelingSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_Skills}Skills";
		private const string OverrideHintText = "{=adjlvl_hint_SkillOverride}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for this specific skill, when not 0. [Default: 0.00]";

		#region VIGOR
		[SettingPropertyFloatingInteger(
			"{=PiHpR4QL}One Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 0)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_OneHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=t78atYqH}Two Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 1)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_TwoHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=haax8kMa}Polearm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 2)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		[SettingPropertyFloatingInteger(
			"{=5rj7xQE4}Bow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 3)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Bow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=TTWL7RLe}Crossbow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 4)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Crossbow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=2wclahIJ}Throwing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 5)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		[SettingPropertyFloatingInteger(
			"{=p9i3zRm9}Riding",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 6)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Riding { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=skZS2UlW}Athletics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 7)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Athletics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=smithingskill}Smithing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 8)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		[SettingPropertyFloatingInteger(
			"{=LJ6Krlbr}Scouting",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 9)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Scouting { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=m8o51fc7}Tactics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 10)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Tactics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=V0ZMJ0PX}Roguery",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 11)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		[SettingPropertyFloatingInteger(
			"{=EGeY1gfs}Charm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 12)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Charm { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=HsLfmEmb}Leadership",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 13)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Leadership { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=GmcgoiGy}Trade",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 14)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		[SettingPropertyFloatingInteger(
			"{=stewardskill}Steward",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 15)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Steward { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=JKH59XNp}Medicine",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 16)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Medicine { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=engineeringskill}Engineering",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = OverrideHintText,
			Order = 17)]
		[SettingPropertyGroup(
			SkillLevelingSkillsGroupName,
			GroupOrder = 0)]
		public float SkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
		#endregion

		#region NPC SKILL MODIFIERS
		private const string SkillLevelingNPCSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_NPCSkills}NPC Skills";
		private const string NPCOverrideHintText = "{=adjlvl_hint_NPCSkillOverride}Overrides modifiers for this specific skill for NPCs only, when not 0. [Default: 0.00]";

		#region VIGOR
		[SettingPropertyFloatingInteger(
			"{=PiHpR4QL}One Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 0)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_OneHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=t78atYqH}Two Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 1)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_TwoHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=haax8kMa}Polearm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 2)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		[SettingPropertyFloatingInteger(
			"{=5rj7xQE4}Bow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 3)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Bow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=TTWL7RLe}Crossbow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 4)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Crossbow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=2wclahIJ}Throwing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 5)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		[SettingPropertyFloatingInteger(
			"{=p9i3zRm9}Riding",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 6)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Riding { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=skZS2UlW}Athletics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 7)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Athletics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=smithingskill}Smithing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 8)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		[SettingPropertyFloatingInteger(
			"{=LJ6Krlbr}Scouting",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 9)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Scouting { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=m8o51fc7}Tactics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 10)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Tactics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=V0ZMJ0PX}Roguery",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 11)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		[SettingPropertyFloatingInteger(
			"{=EGeY1gfs}Charm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 12)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Charm { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=HsLfmEmb}Leadership",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 13)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Leadership { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=GmcgoiGy}Trade",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 14)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		[SettingPropertyFloatingInteger(
			"{=stewardskill}Steward",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 15)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Steward { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=JKH59XNp}Medicine",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 16)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Medicine { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=engineeringskill}Engineering",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = NPCOverrideHintText,
			Order = 17)]
		[SettingPropertyGroup(
			SkillLevelingNPCSkillsGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
		#endregion

		#region CLAN SKILL MODIFIERS
		private const string SkillLevelingClanSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_ClanSkills}Clan Member or Companion Skills";
		private const string ClanOverrideHintText = "{=adjlvl_hint_ClanSkillOverride}Overrides modifiers for this specific skill for clan members or companions only, when not 0. [Default: 0.00]";

		#region VIGOR
		[SettingPropertyFloatingInteger(
			"{=PiHpR4QL}One Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 0)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_OneHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=t78atYqH}Two Handed",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 1)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_TwoHanded { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=haax8kMa}Polearm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 2)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Polearm { get; set; } = 0f;
		#endregion

		#region CONTROL
		[SettingPropertyFloatingInteger(
			"{=5rj7xQE4}Bow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 3)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Bow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=TTWL7RLe}Crossbow",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 4)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Crossbow { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=2wclahIJ}Throwing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 5)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Throwing { get; set; } = 0f;
		#endregion

		#region ENDURANCE
		[SettingPropertyFloatingInteger(
			"{=p9i3zRm9}Riding",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 6)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Riding { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=skZS2UlW}Athletics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 7)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Athletics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=smithingskill}Smithing",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 8)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Crafting { get; set; } = 0f;
		#endregion

		#region CUNNING
		[SettingPropertyFloatingInteger(
			"{=LJ6Krlbr}Scouting",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 9)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Scouting { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=m8o51fc7}Tactics",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 10)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Tactics { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=V0ZMJ0PX}Roguery",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 11)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Roguery { get; set; } = 0f;
		#endregion

		#region SOCIAL
		[SettingPropertyFloatingInteger(
			"{=EGeY1gfs}Charm",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 12)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Charm { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=HsLfmEmb}Leadership",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 13)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Leadership { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=GmcgoiGy}Trade",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 14)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Trade { get; set; } = 0f;
		#endregion

		#region INTELLIGENCE
		[SettingPropertyFloatingInteger(
			"{=stewardskill}Steward",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 15)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Steward { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=JKH59XNp}Medicine",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 16)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Medicine { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"{=engineeringskill}Engineering",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = ClanOverrideHintText,
			Order = 17)]
		[SettingPropertyGroup(
			SkillLevelingClanSkillsGroupName,
			GroupOrder = 2)]
		public float ClanSkillXPModifier_Engineering { get; set; } = 0f;
		#endregion
		#endregion
		#endregion


		#region OTHER STUFF
		private const string OtherGroupName = "{=adjlvl_group_Other}Other";

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_TroopXPModifier}Troop XP Modifier",
			0.01f,
			100f,
			"0%",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_TroopXPModifier}Adjust experience gain for troops from battles, equipment, etc. [Default: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			OtherGroupName,
			GroupOrder = 2)]
		public float TroopXPModifier { get; set; } = 1f;
		#endregion


		#region SMITHING PART RESEARCH MODIFIERS
		private const string SmithingResearchGroupName = "{=adjlvl_group_SmithingResearch}Smithing Research";

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_PartResearch}Part Research Modifier",
			0.01f,
			100f,
			"0%",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_PartResearch}Adjust smithing part research gain rate for smithing and smelting weapons. [Default: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			SmithingResearchGroupName,
			GroupOrder = 3)]
		public float SmithingResearchModifier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"{=adjlvl_name_FreeBuildPartResearch}Free Build Part Research Modifier",
			0.01f,
			1.0f,
			"0%",
			RequireRestart = false,
			HintText = "{=adjlvl_hint_FreeBuildPartResearch}Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Default: 10%]",
			Order = 1)]
		[SettingPropertyGroup(
			SmithingResearchGroupName,
			GroupOrder = 3)]
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
	}
}