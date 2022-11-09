using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "InitializeSkillsRequiredForLevel")]
internal class PatchSkillRequiredForLevel
{
	public static bool Prefix(ref int[] ____skillsRequiredForLevel)
	{
		____skillsRequiredForLevel[0] = 0;
		____skillsRequiredForLevel[1] = 1;
		for (int i = 2; i < ____skillsRequiredForLevel.Length; i++)
		{
			____skillsRequiredForLevel[i] = ____skillsRequiredForLevel[i - 1] + (int)(MathF.Pow(i, 3.3f) + i * 1500f); 
		}
		return false;
	}
}
