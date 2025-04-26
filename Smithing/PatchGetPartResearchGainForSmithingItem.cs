using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;

namespace AdjustableLeveling.Smithing;

[HarmonyPatch(typeof(DefaultSmithingModel), "GetPartResearchGainForSmithingItem")]
internal static class PatchGetPartResearchGainForSmithingItem
{
	public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
	{
		//call static AdjustableLeveling.MCMSettings AdjustableLeveling.AdjustableLeveling::get_Settings()
		//callvirt System.Single AdjustableLeveling.MCMSettings::get_SmithingFreeBuildResearchModifier()
		var patched = false;
		foreach (var instruction in instructions)
		{
			if (!patched 
				&& instruction.opcode == OpCodes.Ldc_R4 
				&& instruction.operand is 0.1f)
			{
				yield return new CodeInstruction(
					OpCodes.Call, 
					typeof(MCMSettings).GetProperty(nameof(MCMSettings.Settings), BindingFlags.Static | BindingFlags.Public).GetGetMethod());
				yield return new CodeInstruction(
					OpCodes.Callvirt, 
					typeof(MCMSettings).GetProperty(nameof(MCMSettings.SmithingFreeBuildResearchModifier), BindingFlags.Instance | BindingFlags.Public).GetGetMethod());
				patched = true;
			}
			else
			{
				yield return instruction;
			}
		}
		if (!patched)
			AdjLvlUtility.Message($"{nameof(AdjustableLeveling)}: failed to patch 'GetPartResearchGainForSmithingItem'");
	}
}