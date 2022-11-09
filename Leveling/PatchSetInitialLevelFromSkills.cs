using HarmonyLib;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(HeroDeveloper), "SetInitialLevelFromSkills")]
internal class PatchSetInitialLevelFromSkills
{
	public static bool Prefix(ref HeroDeveloper __instance)
	{
		int totalXP = 0;
		foreach (var skill in Skills.All)
		{
			var skillValue = __instance.Hero.GetSkillValue(skill);
			if (skillValue >= 300)
				skillValue = 299;
			var xp = PatchXpRequiredForSkillLevel.XpRequiredForSkillLevel[skillValue];
			totalXP += xp;
		}
		typeof(HeroDeveloper).GetProperty(nameof(HeroDeveloper.TotalXp)).GetSetMethod(true).Invoke(__instance, new object[] { totalXP });
		return false;
	}
}