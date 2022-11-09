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
		[SettingPropertyInteger(
			"Levels per Attribute Point",
			1,
			10,
			"0",
			RequireRestart = false,
			HintText = "Number of level ups required to gain an attribute point. [Native: 4]",
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
			HintText = "Adjust character level xp gain rate. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Character Leveling",
			GroupOrder = 0)]
		public float LevelXPMultiplier { get; set; } = 1f;
		#endregion


		#region SKILL LEVELING MODIFIERS
		[SettingPropertyFloatingInteger(
			"Skill XP Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust skill xp gain rate. [Native: 100%]",
			Order = 0)]
		[SettingPropertyGroup(
			"Skill Leveling",
			GroupOrder = 0)]
		public float SkillXPMultiplier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"NPC Skill XP Multiplier",
			0.01f,
			10.0f,
			"0%",
			RequireRestart = false,
			HintText = "Adjust npc skill xp gain rate, multiplicative with 'Skill XP Multiplier'. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Skill Leveling",
			GroupOrder = 0)]
		public float NPCSkillXPMultiplier { get; set; } = 1f;
		#endregion

		#region SKILL MODIFIERS
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