using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(HeroDeveloper), "AddSkillXp")]
internal static class PatchAddSkillXp
{
	public static void Prefix(HeroDeveloper __instance, SkillObject skill, ref float rawXp)
	{
		//var hero = __instance?.Hero;
		//if (hero != null && (hero.CharacterObject.IsPlayerCharacter || hero.IsWanderer))
		//{
		//FileLog.Log($"{__instance.Hero?.Name} {skill.Name} {rawXp} {rawXp * Leveling_Rebalance_SubModule.Settings.SkillXPMultiplier}");
		//	rawXp *= Leveling_Rebalance_SubModule.Settings.SkillXPMultiplier;
		//}

		rawXp *= Leveling_Rebalance_SubModule.Settings.SkillXPMultiplier;

		if (__instance?.Hero?.CharacterObject.IsPlayerCharacter == false)
			rawXp *= Leveling_Rebalance_SubModule.Settings.NPCSkillXPMultiplier;
	}
}
