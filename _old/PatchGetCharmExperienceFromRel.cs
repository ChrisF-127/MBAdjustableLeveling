using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultDiplomacyModel))]
//[HarmonyPatch("GetCharmExperienceFromRelationGain")]
//public static class PatchGetCharmExperienceFromRelationGain
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		int num = 0;
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (num == 0 && list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 40f;
//				num++;
//				i++;
//			}
//			if (num == 1 && list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 5f;
//				num++;
//				i++;
//			}
//			if (num == 2 && list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 3f;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
