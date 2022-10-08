using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultSmithingModel))]
//[HarmonyPatch("GetSkillXpForRefining")]
//public static class PatchGetSkillXpForRefining
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 0.1f;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
