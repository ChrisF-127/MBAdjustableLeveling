using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultSkillLevelingManager))]
//[HarmonyPatch("OnBribeGiven")]
//public static class PatchOnBribeGiven
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (list[i].opcode == OpCodes.Ldc_R4)
//			{
//				list[i].operand = 0.5f;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
