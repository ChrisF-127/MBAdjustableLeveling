using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "GetSkillLevelChange")]
internal class PatchGetSkillLevelChange
{
	public static bool Prefix(ref DefaultCharacterDevelopmentModel __instance, ref int __result, Hero hero, SkillObject skill, float skillXp)
	{
		int[] array = (int[])typeof(DefaultCharacterDevelopmentModel).GetField("_xpRequiredForSkillLevel", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);
		int num = 0;
		int skillValue = hero.GetSkillValue(skill);
		for (int i = 0; i < 300; i++)
		{
			int num2 = skillValue + i;
			if (num2 < 300)
			{
				if ((double)skillXp < (double)array[num2])
				{
					break;
				}
				num++;
			}
		}
		__result = num;
		return false;
	}
}
