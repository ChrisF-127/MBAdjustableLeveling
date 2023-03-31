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
		private const string CharacterLevelingGroupName = "Character Leveling";

		[SettingPropertyBool(
			"Faster Leveling Curve",
			RequireRestart = true,
			HintText =
			"Slower earlier but faster later levels, level 62 total: 40.7m [ON] vs 95.4m [OFF]. [Default: OFF]" +
			"\nWARNING: Backup save recommended, changing this in an ongoing save will reset the level xp to half-way to the next level (if total xp is out of bounds for the current level after conversion)!",
			Order = 0)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public bool UseFasterLevelingCurve { get;set; } = false;
		

		[SettingPropertyInteger(
			"Max Character Level",
			5,
			200,
			"0",
			RequireRestart = false,
			HintText = "Adjust the maximum achievable character level. Higher levels require much more xp! [Default: 62]",
			Order = 1)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxCharacterLevel { get; set; } = 62;

		[SettingPropertyFloatingInteger(
			"Character Level XP Modifier",
			0.01f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "Adjust how skill xp is converted into level xp, default is 1-to-1 at 1.00. [Default: 1.00]",
			Order = 2)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public float LevelXPModifier { get; set; } = 1f;


		[SettingPropertyInteger(
			"Levels per Attribute Point",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Default: 4]",
			Order = 3)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int LevelsPerAttributePoint { get; set; } = 4;

		[SettingPropertyInteger(
			"Focus Points per Level",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "Focus points gained per level. [Default: 1]",
			Order = 4)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int FocusPointsPerLevel { get; set; } = 1;


		[SettingPropertyInteger(
			"Max Attribute Points for Attribute",
			1,
			20,
			"0",
			RequireRestart = false,
			HintText = "Attribute point limit per attribute. [Default: 10]",
			Order = 5)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxAttribute { get; set; } = 10;

		[SettingPropertyInteger(
			"Max Focus Points for Skill",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "Focus point limit per skill. (UI will at most show 5 points) [Default: 5]",
			Order = 6)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int MaxFocusPerSkill { get; set; } = 5;


		[SettingPropertyInteger(
			"Attribute Points at Start",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "Apparently affects the attribute points with which NPCs start, but not the player. [Default: 15]",
			Order = 7)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int AttributePointsAtStart { get; set; } = 15;

		[SettingPropertyInteger(
			"Focus Points at Start",
			1,
			100,
			"0",
			RequireRestart = false,
			HintText = "Apparently affects the focus points with which NPCs start, but not the player. [Default: 5]",
			Order = 8)]
		[SettingPropertyGroup(
			CharacterLevelingGroupName,
			GroupOrder = 0)]
		public int FocusPointsAtStart { get; set; } = 5;
		#endregion


		#region SKILL LEVELING MODIFIERS
		private const string SkillLevelingGroupName = "Skill Leveling";

		#region GENERAL
		[SettingPropertyInteger(
			"Learning Limit Increase per Attribute Point",
			0,
			50,
			"0",
			RequireRestart = false,
			HintText = "E.g. at 3 and with 10 AP an additional 30 skill points can be gained after the learning limit at reducing learning rate; at 5 an additional 50 can be gained. [Default: 3]",
			Order = 0)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int LearningLimitIncreasePerAttributePoint { get; set; } = 3;

		[SettingPropertyInteger(
			"Learning Limit Increase per Focus Point",
			0,
			100,
			"0",
			RequireRestart = false,
			HintText = "Adjust the learning limit increase per focus point. [Default: 50]",
			Order = 1)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int LearningLimitIncreasePerFocusPoint { get; set; } = 50;

		[SettingPropertyInteger(
			"Base Learning Limit",
			0,
			100,
			"0",
			RequireRestart = false,
			HintText = "The base learning limit. [Default: 50]",
			Order = 2)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public int BaseLearningLimit { get; set; } = 50;


		[SettingPropertyFloatingInteger(
			"Minimum Learning Rate",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "Set a minimum learning rate. [Default: 0.00]",
			Order = 3)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float MinLearningRate { get; set; } = 0f;

		[SettingPropertyFloatingInteger(
			"Maximum Learning Rate",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "Set a maximum learning rate, zero disables it. [Default: 0.00]",
			Order = 4)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float MaxLearningRate { get; set; } = 0f;


		[SettingPropertyFloatingInteger(
			"Skill XP Modifier",
			0.01f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "Adjust the overall skill learning rate. [Default: 1.00]",
			Order = 5)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float SkillXPModifier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"NPC Skill XP Modifier",
			0f,
			100f,
			"0.00",
			RequireRestart = false,
			HintText = "Overrides 'Skill XP Modifier' for NPCs when not 0. [Default: 0.00]",
			Order = 6)]
		[SettingPropertyGroup(
			SkillLevelingGroupName,
			GroupOrder = 1)]
		public float NPCSkillXPModifier { get; set; } = 0f;
		#endregion

		#region SKILL MODIFIERS
		private const string SkillLevelingSkillsGroupName = "Skill Leveling/Skills";
		private const string OverrideHintText = "Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for this specific skill when not 0. [Default: 0.00]";

		#region VIGOR
		[SettingPropertyFloatingInteger(
			"One Handed",
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
			"Two Handed",
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
			"Polearm",
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
			"Bow",
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
			"Crossbow",
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
			"Throwing",
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
			"Riding",
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
			"Athletics",
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
			"Smithing",
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
			"Scouting",
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
			"Tactics",
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
			"Roguery",
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
			"Charm",
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
			"Leadership",
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
			"Trade",
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
			"Steward",
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
			"Medicine",
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
			"Engineering",
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
		private const string SkillLevelingNPCSkillsGroupName = "Skill Leveling/NPC Skills";
		private const string NPCOverrideHintText = "Overrides modifiers for this specific skill for NPCs only when not 0. [Default: 0.00]";

		#region VIGOR
		[SettingPropertyFloatingInteger(
			"One Handed (NPC)",
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
			"Two Handed (NPC)",
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
			"Polearm (NPC)",
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
			"Bow (NPC)",
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
			"Crossbow (NPC)",
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
			"Throwing (NPC)",
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
			"Riding (NPC)",
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
			"Athletics (NPC)",
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
			"Smithing (NPC)",
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
			"Scouting (NPC)",
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
			"Tactics (NPC)",
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
			"Roguery (NPC)",
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
			"Charm (NPC)",
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
			"Leadership (NPC)",
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
			"Trade (NPC)",
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
			"Steward (NPC)",
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
			"Medicine (NPC)",
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
			"Engineering (NPC)",
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
		#endregion


		#region SMITHING PART RESEARCH MODIFIERS
		private const string SmithingResearchGroupName = "Smithing Research";

		[SettingPropertyFloatingInteger(
			"Part Research Modifier",
			0.01f,
			100f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust smithing part research gain rate for smithing and smelting weapons. [Default: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			SmithingResearchGroupName,
			GroupOrder = 1)]
		public float SmithingResearchModifier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Free Build Part Research Modifier",
			0.01f,
			1.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Default: 10%]",
			Order = 1)]
		[SettingPropertyGroup(
			SmithingResearchGroupName,
			GroupOrder = 1)]
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
	}
}