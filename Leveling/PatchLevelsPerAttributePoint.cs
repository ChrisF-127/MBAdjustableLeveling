using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
public static class PatchLevelsPerAttributePoint
{
	public static void Postfix(ref int __result)
	{
		__result = 2;
	}
}
