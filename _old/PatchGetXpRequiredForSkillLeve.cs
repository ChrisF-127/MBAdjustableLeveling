using System;
using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "GetXpRequiredForSkillLevel")]
//internal class PatchGetXpRequiredForSkillLevel
//{
//	public static void Postfix(ref DefaultCharacterDevelopmentModel __instance, ref int __result, int skillLevel)
//	{
//		int[] array = (int[])typeof(DefaultCharacterDevelopmentModel).GetField("_xpRequiredForSkillLevel", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(__instance);
//		__result = ((skillLevel > 0) ? array[Math.Min(skillLevel - 1, 299)] : 0);
//	}
//}
