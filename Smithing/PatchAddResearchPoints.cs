using System;
using System.Collections.Generic;
using System.Reflection;
using AdjustableLeveling.Settings;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Library;

namespace AdjustableLeveling.Smithing;

[HarmonyPatch(typeof(CraftingCampaignBehavior), "AddResearchPoints")]
internal static class PatchAddResearchPoints
{
	public static void Prefix(ref int researchPoints)
	{
		researchPoints = MathF.Round(researchPoints * MCMSettings.Settings.SmithingResearchModifier);
	}
}