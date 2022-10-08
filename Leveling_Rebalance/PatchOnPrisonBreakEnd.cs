using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultSkillLevelingManager), "OnPrisonBreakEnd")]
internal class PatchOnPrisonBreakEnd
{
	public static bool Prefix(Hero prisonerHero, bool isSucceeded)
	{
		float rogueryRewardOnPrisonBreak = Campaign.Current.Models.PrisonBreakModel.GetRogueryRewardOnPrisonBreak(prisonerHero, isSucceeded);
		if ((double)rogueryRewardOnPrisonBreak <= 0.0)
		{
			return false;
		}
		Hero.MainHero.AddSkillXp(DefaultSkills.Roguery, 0.5f * rogueryRewardOnPrisonBreak);
		return false;
	}
}
