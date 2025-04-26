using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(HeroDeveloper), "GainRawXp")]
internal static class PatchGainRawXp
{
	public static void Prefix(ref float rawXp)
	{
		rawXp *= MCMSettings.Settings.LevelXPModifier;
	}
}
