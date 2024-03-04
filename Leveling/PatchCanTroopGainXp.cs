using HarmonyLib;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.TwoDimension;

namespace AdjustableLeveling.Leveling
{
	[HarmonyPatch(typeof(MobilePartyHelper), "CanTroopGainXp")]
	internal static class PatchCanTroopGainXp
	{
		public static void Postfix(ref int gainableMaxXp)
		{
			gainableMaxXp = (int)Mathf.Round(gainableMaxXp / MCMSettings.Instance.TroopXPModifier);
		}
	}
}
