using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultSmithingModel), "GetSkillXpForSmithingInCraftingOrderMode")]
//internal class PatchGetSkillXpForSmithingInCraftingOrderMode
//{
//	public static bool Prefix(ref int __result, ItemObject item)
//	{
//		__result = MathF.Round(252f * MathF.Pow(item.Value, 0.165f));
//		return false;
//	}
//}
