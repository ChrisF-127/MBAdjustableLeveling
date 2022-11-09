using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "get_LevelsPerAttributePoint")]
public static class PatchLevelsPerAttributePoint
{
	public static void Postfix(ref int __result)
	{
		__result = AdjustableLeveling.Settings.LevelsPerAttributePoint;
	}
}
