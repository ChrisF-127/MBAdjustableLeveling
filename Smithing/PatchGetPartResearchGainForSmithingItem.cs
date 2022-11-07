using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultSmithingModel), "GetPartResearchGainForSmithingItem")]
internal class PatchGetPartResearchGainForSmithingItem
{
	public static void Prefix(ref bool isFreeBuild)
	{
		isFreeBuild = false;
	}
}