using MCM.Abstractions.Attributes;
using MCM.Abstractions.Attributes.v2;
using MCM.Abstractions.Base.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.MountAndBlade;

namespace Leveling_Rebalance
{
	public class MCMSettings : AttributeGlobalSettings<MCMSettings>
	{
		public override string Id => "LevelingRebalance";

		public override string DisplayName => "Leveling Rebalance";

		public override string FolderName => "LevelingRebalance";

		public override string FormatType => "json";


		[SettingPropertyFloatingInteger(
			"Skill XP Multiplier",
			0.01f,
			100.0f,
			"#0%",
			RequireRestart = false,
			HintText = "Adjust skill xp gain rate. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Multiplier",
			GroupOrder = 0)]
		public float SkillXPMultiplier { get; set; } = 1f;

		[SettingPropertyFloatingInteger(
			"NPC Skill XP Multiplier",
			0.01f,
			100.0f,
			"#0%",
			RequireRestart = false,
			HintText = "Adjust npc skill xp gain rate. [Native: 100%]",
			Order = 1)]
		[SettingPropertyGroup(
			"Multiplier",
			GroupOrder = 0)]
		public float NPCSkillXPMultiplier { get; set; } = 1f;
	}
}