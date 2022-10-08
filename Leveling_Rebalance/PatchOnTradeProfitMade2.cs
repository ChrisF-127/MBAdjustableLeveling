using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace Leveling_Rebalance;

// Reduces skil gain for trade

//[HarmonyPatch(typeof(DefaultSkillLevelingManager))]
//[HarmonyPatch("OnTradeProfitMade")]
//[HarmonyPatch(new Type[]
//{
//	typeof(Hero),
//	typeof(int)
//})]
//public static class PatchOnTradeProfitMade2
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 0.25f;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
