using HarmonyLib;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.TwoDimension;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(TroopRoster), "AddXpToTroopAtIndex")]
internal static class PatchAddXpToTroopAtIndex
{
	public static void Prefix(ref int xpAmount)
	{
		//var oriXp = xpAmount;
		xpAmount = (int)Mathf.Round(xpAmount * MCMSettings.Instance.TroopXPModifier);
		//AdjustableLeveling.Message($"{nameof(PatchAddXpToTroopAtIndex)}: {oriXp} {xpAmount}", false);
	}
}

