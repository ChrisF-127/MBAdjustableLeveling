﻿using AdjustableLeveling.Settings;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(HeroDeveloper), "AddSkillXp")]
internal static class PatchAddSkillXp
{
	public static void Prefix(HeroDeveloper __instance, SkillObject skill, ref float rawXp)
	{
		//var oriXp = rawXp;
		rawXp *= MCMSettings.Settings.GetSkillModifier(skill, __instance?.Hero);
		//AdjustableLevelingUtility.Message($"{__instance.Hero.Name} {skill.Name} {oriXp} {rawXp}", false);
	}
}
