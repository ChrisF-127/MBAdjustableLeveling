using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeXpRequiredForSkillLevel")]
internal static class PatchXpRequiredForSkillLevel
{
	public static int[] XpRequiredForSkillLevel;
	public static bool Prefix(ref int[] ____xpRequiredForSkillLevel)
	{
		int num = 0;
		____xpRequiredForSkillLevel[0] = num;
		for (int i = 1; i < ____xpRequiredForSkillLevel.Length; i++)
		{
			if (i < 300)
			{
				//num += 12 + (int)(i / 2.25);
				//____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i - 1] + num;
				____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i - 1] + (int)(MathF.Pow(i * 0.02f, 5.5f) + MathF.Pow(i, 1.78f) + i * 10f);
			}
			else
			{
				____xpRequiredForSkillLevel[i] = int.MaxValue;
			}
		}
		XpRequiredForSkillLevel = ____xpRequiredForSkillLevel;
		return false;
	}
}
