using HarmonyLib;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(HeroDeveloper), "<SetInitialLevelFromSkills>b__43_0")]
//internal class Patchb__43_0
//{
//	public static bool Prefix(ref HeroDeveloper __instance, ref float __result, SkillObject s)
//	{
//		float num = __instance.Hero.GetSkillValue(s);
//		__result = 7.5f * num * ((num + 1f) / 2f);
//		return false;
//	}
//}