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
		internal static float SkillToModifier(this SkillObject skill, bool isNPC)
		{
			// Vigor
			if (skill == DefaultSkills.OneHanded)
				return AdjustableLeveling.Settings.SkillXPMultiplier_OneHanded 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_OneHanded : 1f);
			if (skill == DefaultSkills.TwoHanded)
				return AdjustableLeveling.Settings.SkillXPMultiplier_TwoHanded 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_TwoHanded : 1f);
			if (skill == DefaultSkills.Polearm)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Polearm 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Polearm : 1f);

			// Control
			if (skill == DefaultSkills.Bow)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Bow 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Bow : 1f);
			if (skill == DefaultSkills.Crossbow)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Crossbow 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Crossbow : 1f);
			if (skill == DefaultSkills.Throwing)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Throwing 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Throwing : 1f);

			// Endurance
			if (skill == DefaultSkills.Riding)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Riding 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Riding : 1f);
			if (skill == DefaultSkills.Athletics)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Athletics 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Athletics : 1f);
			if (skill == DefaultSkills.Crafting)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Crafting 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Crafting : 1f);

			// Cunning
			if (skill == DefaultSkills.Scouting)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Scouting 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Scouting : 1f);
			if (skill == DefaultSkills.Tactics)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Tactics 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Tactics : 1f);
			if (skill == DefaultSkills.Roguery)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Roguery 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Roguery : 1f);

			// Social
			if (skill == DefaultSkills.Charm)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Charm 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Charm : 1f);
			if (skill == DefaultSkills.Leadership)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Leadership 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Leadership : 1f);
			if (skill == DefaultSkills.Trade)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Trade 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Trade : 1f);

			// Intelligence
			if (skill == DefaultSkills.Steward)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Steward 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Steward : 1f);
			if (skill == DefaultSkills.Medicine)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Medicine 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Medicine : 1f);
			if (skill == DefaultSkills.Engineering)
				return AdjustableLeveling.Settings.SkillXPMultiplier_Engineering 
					* (isNPC ? AdjustableLeveling.Settings.NPCSkillXPMultiplier_Engineering : 1f);

			// Others / Unknown
			return 1f;
		}
	}
}
