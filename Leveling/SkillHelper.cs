using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace AdjustableLeveling.Leveling
{
	internal static class SkillHelper
	{
		internal static float SkillToModifier(this SkillObject skill)
		{
			// Vigor
			if (skill == DefaultSkills.OneHanded)
				return AdjustableLeveling.Settings.SkillXPMultiplier_OneHanded;
			if (skill == DefaultSkills.TwoHanded)
				return AdjustableLeveling.Settings.SkillXPMultiplier_TwoHanded;
			if (skill == DefaultSkills.Polearm)
				return AdjustableLeveling.Settings.SkillXPMultiplier_OneHanded;

			// Control
			if (skill == DefaultSkills.Bow)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Bow;
			if (skill == DefaultSkills.Crossbow)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Crossbow;
			if (skill == DefaultSkills.Throwing)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Throwing;

			// Endurance
			if (skill == DefaultSkills.Riding)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Riding;
			if (skill == DefaultSkills.Athletics)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Athletics;
			if (skill == DefaultSkills.Crafting)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Crafting;

			// Cunning
			if (skill == DefaultSkills.Scouting)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Scouting;
			if (skill == DefaultSkills.Tactics)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Tactics;
			if (skill == DefaultSkills.Roguery)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Roguery;

			// Social
			if (skill == DefaultSkills.Charm)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Charm;
			if (skill == DefaultSkills.Leadership)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Leadership;
			if (skill == DefaultSkills.Trade)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Trade;

			// Intelligence
			if (skill == DefaultSkills.Steward)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Steward;
			if (skill == DefaultSkills.Medicine)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Medicine;
			if (skill == DefaultSkills.Engineering)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Engineering;

			// Others / Unknown
			return 1f;
		}

		internal static float NPCSkillToModifier(this SkillObject skill)
		{
			// Vigor
			if (skill == DefaultSkills.OneHanded)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_OneHanded;
			if (skill == DefaultSkills.TwoHanded)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_TwoHanded;
			if (skill == DefaultSkills.Polearm)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_OneHanded;

			// Control
			if (skill == DefaultSkills.Bow)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Bow;
			if (skill == DefaultSkills.Crossbow)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Crossbow;
			if (skill == DefaultSkills.Throwing)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Throwing;

			// Endurance
			if (skill == DefaultSkills.Riding)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Riding;
			if (skill == DefaultSkills.Athletics)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Athletics;
			if (skill == DefaultSkills.Crafting)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Crafting;

			// Cunning
			if (skill == DefaultSkills.Scouting)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Scouting;
			if (skill == DefaultSkills.Tactics)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Tactics;
			if (skill == DefaultSkills.Roguery)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Roguery;

			// Social
			if (skill == DefaultSkills.Charm)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Charm;
			if (skill == DefaultSkills.Leadership)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Leadership;
			if (skill == DefaultSkills.Trade)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Trade;

			// Intelligence
			if (skill == DefaultSkills.Steward)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Steward;
			if (skill == DefaultSkills.Medicine)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Medicine;
			if (skill == DefaultSkills.Engineering)
				return AdjustableLeveling.Settings.NPCSkillXPMultiplier_Engineering;

			// Others / Unknown
			return 1f;
		}
	}
}
