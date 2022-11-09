using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace AdjustableLeveling
{
	[HarmonyPatch(typeof(DefaultPerks))]
	[HarmonyPatch("InitializeAll")]
	public static class PatchInitializeAll
	{
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].opcode == OpCodes.Ldstr && list[i].operand.Equals("{=a8tFTecO}Character has 5% chance to create a 'Legendary' weapon, if difficulty requirements are met. The chance is increased by 1% for each skill point above 300."))
				{
					list[i].operand = "{=a8tFTecO}Character has 5% chance to create a 'Legendary' weapon, if difficulty requirements are met. The chance is increased by 1% for each skill point above 275.";
					break;
				}
			}
			return list.AsEnumerable();
		}
	}
}
