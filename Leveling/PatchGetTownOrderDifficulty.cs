using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(CraftingCampaignBehavior), "GetTownOrderDifficulty")]
internal class PatchGetTownOrderDifficulty
{
	//public static bool Prefix(Town town, int orderSlot, ref float __result)
	//{
	//	var num = MBRandom.RandomInt(40, 300); // all order slots random without regard for skill
	//	__result = MathF.Min(num + town.Prosperity / 500f, 299f);
	//	return false;
	//}

	public static void Postfix(ref float __result)
	{
		__result = MathF.Min(__result, 299f);
	}
}
