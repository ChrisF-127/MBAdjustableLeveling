using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(SkillLevelingManager), "OnCombatHit")]
internal class PatchOnCombatHit
{
	public static bool Prefix(CharacterObject affectorCharacter, CharacterObject affectedCharacter, CharacterObject captain, Hero commander, float speedBonusFromMovement, float shotDifficulty, WeaponComponentData affectorWeapon, float hitPointRatio, CombatXpModel.MissionTypeEnum missionType, bool isAffectorMounted, bool isTeamKill, bool isAffectorUnderCommand, float damageAmount, bool isFatal, bool isSiegeEngineHit, bool isHorseCharge)
	{
		float num = 1f;
		if (isTeamKill)
		{
			return false;
		}
		if (affectorCharacter.IsHero)
		{
			Hero heroObject = affectorCharacter.HeroObject;
			Campaign.Current.Models.CombatXpModel.GetXpFromHit(heroObject.CharacterObject, captain, affectedCharacter, heroObject.PartyBelongedTo?.Party, (int)damageAmount, isFatal, missionType, out var xpAmount);
			num = xpAmount;
			if (affectorWeapon != null)
			{
				SkillObject skillForWeapon = Campaign.Current.Models.CombatXpModel.GetSkillForWeapon(affectorWeapon, isSiegeEngineHit);
				float num2 = ((skillForWeapon == DefaultSkills.Bow) ? 0.75f : ((skillForWeapon == DefaultSkills.Throwing) ? 1.5f : 1f));
				if ((double)shotDifficulty > 0.0)
				{
					num += (float)MathF.Floor(num * num2 * Campaign.Current.Models.CombatXpModel.GetXpMultiplierFromShotDifficulty(shotDifficulty));
					num *= 0.66f;
				}
				if (!affectorCharacter.IsPlayerCharacter)
				{
					num *= 2.5f;
					//num = ((skillForWeapon != DefaultSkills.Bow && skillForWeapon != DefaultSkills.Crossbow && skillForWeapon != DefaultSkills.Throwing) ? (num * 10f) : (num * 5f));
				}
				num *= 0.66f;
				heroObject.AddSkillXp(skillForWeapon, MBRandom.RoundRandomized(num));
			}
			else
			{
				if (!affectorCharacter.IsPlayerCharacter)
				{
					num *= 2.5f; // 10f
				}
				if (isHorseCharge)
				{
					heroObject.AddSkillXp(DefaultSkills.Riding, MBRandom.RoundRandomized(num));
				}
				else
				{
					heroObject.AddSkillXp(DefaultSkills.Athletics, MBRandom.RoundRandomized(num));
				}
			}
			if (!isSiegeEngineHit && !isHorseCharge)
			{
				if (isAffectorMounted)
				{
					float num3 = 0.5f;
					if ((double)speedBonusFromMovement > 0.0)
					{
						num3 += speedBonusFromMovement / 4f;
					}
					if ((double)num3 > 0.0)
					{
						heroObject.AddSkillXp(DefaultSkills.Riding, MBRandom.RoundRandomized(num3 * num));
					}
				}
				else
				{
					float num4 = 0.5f;
					if ((double)speedBonusFromMovement > 0.0)
					{
						num4 += 1f * speedBonusFromMovement;
					}
					if ((double)num4 > 0.0)
					{
						heroObject.AddSkillXp(DefaultSkills.Athletics, MBRandom.RoundRandomized(num4 * num));
					}
				}
			}
		}
		if (commander == null || commander == affectorCharacter.HeroObject || commander.PartyBelongedTo == null)
		{
			return false;
		}
		return false;
	}
}
