using HarmonyLib;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultSkillLevelingManager), "OnSimulationCombatKill")]
internal class PatchOnSimulationCombatKill
{
	private static bool Prefix(CharacterObject affectorCharacter, CharacterObject affectedCharacter, PartyBase affectorParty, PartyBase commanderParty)
	{
		int num = Campaign.Current.Models.PartyTrainingModel.GetXpReward(affectedCharacter);
		if (affectorCharacter.IsHero)
		{
			if (!affectorCharacter.IsPlayerCharacter)
			{
				num *= 5;
			}
			ItemObject defaultWeapon = CharacterHelper.GetDefaultWeapon(affectorCharacter);
			Hero heroObject = affectorCharacter.HeroObject;
			if (defaultWeapon != null)
			{
				SkillObject skillForWeapon = Campaign.Current.Models.CombatXpModel.GetSkillForWeapon(defaultWeapon.GetWeaponWithUsageIndex(0), isSiegeEngineHit: false);
				heroObject.AddSkillXp(skillForWeapon, num);
			}
			else
			{
				heroObject.AddSkillXp(DefaultSkills.Athletics, num);
			}
			if (affectorCharacter.IsMounted)
			{
				float f = (float)num * 0.75f;
				heroObject.AddSkillXp(DefaultSkills.Riding, MBRandom.RoundRandomized(f));
			}
			else
			{
				float f2 = (float)num * 0.75f;
				heroObject.AddSkillXp(DefaultSkills.Athletics, MBRandom.RoundRandomized(f2));
			}
		}
		if (commanderParty == null || !commanderParty.IsMobile || commanderParty.LeaderHero == null || commanderParty.LeaderHero == affectedCharacter.HeroObject)
		{
			return false;
		}
		SkillLevelingManager.OnTacticsUsed(commanderParty.MobileParty, MathF.Ceiling(0.04f * (float)num * ((!affectorCharacter.IsPlayerCharacter) ? 0.5f : 1f)));
		return false;
	}
}
