using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "GetSkillLevelChange")]
internal class PatchGetSkillLevelChange
{
	public static bool Prefix(ref DefaultCharacterDevelopmentModel __instance, ref int __result, Hero hero, SkillObject skill, float skillXp)
	{
		int[] array = (int[])typeof(DefaultCharacterDevelopmentModel).GetField("_xpRequiredForSkillLevel", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);
		int start = hero.GetSkillValue(skill);
		int next = start;
		while (next < 300)
		{
			if ((double)skillXp < (double)array[next])
				break;
			next++;
		}
		__result = next - start;
		return false;
	}
}