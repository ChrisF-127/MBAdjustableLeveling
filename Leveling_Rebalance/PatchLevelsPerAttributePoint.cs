using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
public static class PatchLevelsPerAttributePoint
{
	private static void Postfix(ref int __result)
	{
		__result = 2;
	}
}
