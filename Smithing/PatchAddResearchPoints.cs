using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using static TaleWorlds.Core.Crafting;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(CraftingCampaignBehavior), "AddResearchPoints")]
internal class PatchAddResearchPoints
{
	public static void Prefix(ref int researchPoints)
	{
		researchPoints = MathF.Round(researchPoints * AdjustableLeveling.Settings.SmithingResearchModifier);
	}
}