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
		public const string SkillHintText =
			"Adjusts this skill's learning rate for everyone. [Native: 100%]";
		public const string NPCSkillHintText =
			"Additional adjustment for this skill's learning rate for non-player characters only. " +
			"Can be used to increase (> 100%) or decrease (< 100%) the learning rate compared to the player. [Native: 100%]";

		public override string Id => "AdjustableLeveling";

		public override string DisplayName => "Adjustable Leveling";

		public override string FolderName => "AdjustableLeveling";

		public override string FormatType => "json";

		#region CHARACTER LEVELING MODIFIERS
		[SettingPropertyInteger(
			"Levels per Attribute Point",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Native: 4]",
			Order = 0)]
		[SettingPropertyGroup(
			"Character Leveling",
			GroupOrder = 0)]
		public int LevelsPerAttributePoint { get; set; } = 4;

		[SettingPropertyFloatingInteger(
			"Character Level XP Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust how skill xp is converted into level xp, default is 1-to-1 at 100%. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Character Leveling",
			GroupOrder = 0)]
		public float LevelXPMultiplier { get; set; } = 1f;
		#endregion


		#region SKILL LEVELING MODIFIERS
		#region GENERAL
		[SettingPropertyFloatingInteger(
			"Skill XP Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust the overall skill learning rate for everyone. [Native: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			"Skill Leveling",
			GroupOrder = 1)]
		public float SkillXPMultiplier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"NPC Skill XP Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Additional adjustment for the overall skill learning rate for non-player characters only. " +
				"Can be used to increase (> 100%) or decrease (< 100%) the learning rate compared to the player. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Skill Leveling",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier { get; set; } = 1f;
		#endregion

		#region SKILL MODIFIERS
		#region VIGOR
		[SettingPropertyFloatingInteger(
			"One Handed",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 0)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_OneHanded { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Two Handed",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 1)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_TwoHanded { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Polearm",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 2)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Polearm { get; set; } = 1f;
		#endregion

		#region CONTROL
		[SettingPropertyFloatingInteger(
			"Bow",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 3)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Bow { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Crossbow",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 4)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Crossbow { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Throwing",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 5)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Throwing { get; set; } = 1f;
		#endregion

		#region ENDURANCE
		[SettingPropertyFloatingInteger(
			"Riding",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 6)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Riding { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Athletics",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 7)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Athletics { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Smithing",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 8)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Crafting { get; set; } = 1f;
		#endregion

		#region CUNNING
		[SettingPropertyFloatingInteger(
			"Scouting",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 9)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Scouting { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Tactics",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 10)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Tactics { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Roguery",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 11)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Roguery { get; set; } = 1f;
		#endregion

		#region SOCIAL
		[SettingPropertyFloatingInteger(
			"Charm",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 12)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Charm { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Leadership",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 13)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Leadership { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Trade",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 14)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Trade { get; set; } = 1f;
		#endregion

		#region INTELLIGENCE
		[SettingPropertyFloatingInteger(
			"Steward",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 15)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Steward { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Medicine",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 16)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Medicine { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Engineering",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = SkillHintText,
			Order = 17)]
		[SettingPropertyGroup(
			"Skill Leveling/Skills",
			GroupOrder = 0)]
		public float SkillXPMultiplier_Engineering { get; set; } = 1f;
		#endregion
		#endregion

		#region NPC SKILL MODIFIERS
		#region VIGOR
		[SettingPropertyFloatingInteger(
			"One Handed (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 0)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_OneHanded { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Two Handed (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 1)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_TwoHanded { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Polearm (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 2)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Polearm { get; set; } = 1f;
		#endregion

		#region CONTROL
		[SettingPropertyFloatingInteger(
			"Bow (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 3)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Bow { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Crossbow (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 4)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Crossbow { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Throwing (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 5)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Throwing { get; set; } = 1f;
		#endregion

		#region ENDURANCE
		[SettingPropertyFloatingInteger(
			"Riding (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 6)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Riding { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Athletics (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 7)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Athletics { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Smithing (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 8)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Crafting { get; set; } = 1f;
		#endregion

		#region CUNNING
		[SettingPropertyFloatingInteger(
			"Scouting (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 9)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Scouting { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Tactics (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 10)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Tactics { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Roguery (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 11)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Roguery { get; set; } = 1f;
		#endregion

		#region SOCIAL
		[SettingPropertyFloatingInteger(
			"Charm (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 12)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Charm { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Leadership (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 13)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Leadership { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Trade (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 14)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Trade { get; set; } = 1f;
		#endregion

		#region INTELLIGENCE
		[SettingPropertyFloatingInteger(
			"Steward (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 15)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Steward { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Medicine (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 16)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Medicine { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Engineering (NPC)",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = NPCSkillHintText,
			Order = 17)]
		[SettingPropertyGroup(
			"Skill Leveling/NPC Skills",
			GroupOrder = 1)]
		public float NPCSkillXPMultiplier_Engineering { get; set; } = 1f;
		#endregion
		#endregion
		#endregion


		#region SMITHING PART RESEARCH MODIFIERS
		[SettingPropertyFloatingInteger(
			"Part Research Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust smithing part research gain rate for smithing and smelting weapons. [Native: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			"Smithing Research",
			GroupOrder = 1)]
		public float SmithingResearchModifier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"Free Build Part Research Multiplier",
			0.01f,
			1.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Native: 10%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Smithing Research",
			GroupOrder = 1)]
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
	}
}