using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeXpRequiredForSkillLevel")]
internal static class PatchXpRequiredForSkillLevel
{
	public static bool Prefix(ref int[] ____xpRequiredForSkillLevel)
	{
		int num = 0;
		____xpRequiredForSkillLevel[0] = num;
		for (int i = 1; i < ____xpRequiredForSkillLevel.Length; i++)
		{
			if (i < 300)
			{
				num += 30;
				____xpRequiredForSkillLevel[i] = ____xpRequiredForSkillLevel[i - 1] + num;
			}
			else
			{
				____xpRequiredForSkillLevel[i] = int.MaxValue;
			}
		}
		return false;
	}
}
