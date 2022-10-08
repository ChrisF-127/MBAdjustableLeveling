using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultPartyHealingModel), "GetSkillXpFromHealingTroop")]
internal class PatchGetSkillXpFromHealingTroop
{
	private static void Postfix(ref int __result)
	{
		__result = 100;
	}
}
