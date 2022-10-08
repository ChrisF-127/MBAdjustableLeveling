using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

// Changes Max Level; already set to 62 so not required

//[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel))]
//[HarmonyPatch("SkillsRequiredForLevel")]
//public static class PatchSkillsRequiredForLevel
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (list[i].opcode == OpCodes.Ldc_I4_S)
//			{
//				list[i].operand = 62;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
