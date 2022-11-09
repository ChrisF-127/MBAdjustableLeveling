using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(DefaultSmithingModel))]
[HarmonyPatch("GetModifierTierForSmithedWeapon")]
public static class PatchGetModifierTierForSmithedWeapon
{
	public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
	{
		List<CodeInstruction> list = new List<CodeInstruction>(instructions);
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].opcode == OpCodes.Ldc_I4 && (int)list[i].operand == 300)
			{
				list[i].operand = 275;
				break;
			}
		}
		return list.AsEnumerable();
	}
}
