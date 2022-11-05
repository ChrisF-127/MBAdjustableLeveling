using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Leveling_Rebalance;

//[HarmonyPatch(typeof(DefaultMapTrackModel), "GetSkillFromTrackDetected")]
//internal class PatchGetSkillFromTrackDetected
//{
//	private static bool Prefix(ref float __result, Track track, float detectionDifficulty)
//	{
//		__result = 5f;
//		return false;
//	}
//}
