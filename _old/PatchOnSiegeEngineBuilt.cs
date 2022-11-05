using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace Leveling_Rebalance;

// Increases skill gain for building siege engines (2 -> 5)

//[HarmonyPatch(typeof(DefaultSkillLevelingManager), "OnSiegeEngineBuilt")]
//internal class PatchOnSiegeEngineBuilt
//{
//	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//	{
//		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
//		for (int i = 0; i < list.Count; i++)
//		{
//			if (list[i].opcode == OpCodes.Ldc_R4 && (float)list[i].operand == 2f)
//			{
//				list[i].operand = 5f;
//				break;
//			}
//		}
//		return list.AsEnumerable();
//	}
//}
