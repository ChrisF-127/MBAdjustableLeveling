using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultSkillLevelingManager))]
[HarmonyPatch("OnMainHeroDisguised")]
public static class PatchOnMainHeroDisguised
{
	private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
	{
		int num = 0;
		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].opcode == OpCodes.Ldc_R4 && num == 0)
			{
				list[i].operand = 800f;
				i++;
				num++;
			}
			if (list[i].opcode == OpCodes.Ldc_R4 && num == 1)
			{
				list[i].operand = 600f;
				i++;
				num++;
			}
			if (list[i].opcode == OpCodes.Ldc_R4 && num == 2)
			{
				list[i].operand = 45f;
				i++;
				num++;
			}
			if (list[i].opcode == OpCodes.Ldc_R4 && num == 3)
			{
				list[i].operand = 65f;
				break;
			}
		}
		return list.AsEnumerable();
	}
}
