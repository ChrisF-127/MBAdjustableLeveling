using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeSkillsRequiredForLevel")]
internal class PatchSkillRequiredForLevel
{
	public static bool Prefix(ref int[] ____skillsRequiredForLevel)
	{
		int num = 2000;
		____skillsRequiredForLevel = new int[1000];
		____skillsRequiredForLevel[0] = 0;
		____skillsRequiredForLevel[1] = 1;
		for (int i = 2; i < 1000; i++)
		{
			____skillsRequiredForLevel[i] = ____skillsRequiredForLevel[i - 1] + num;
			num += 2000;
		}
		return false;
	}
}
