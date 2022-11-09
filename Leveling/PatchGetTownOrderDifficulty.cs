using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace AdjustableLeveling;

[HarmonyPatch(typeof(CraftingCampaignBehavior), "GetTownOrderDifficulty")]
internal class PatchGetTownOrderDifficulty
{
	public static bool Prefix(Town town, int orderSlot, ref float __result)
	{
		int num = MBRandom.RandomInt(0, 6);
		int num2 = 0;
		switch (num)
		{
		case 0:
			num2 = MBRandom.RandomInt(40, 80);
			break;
		case 1:
			num2 = MBRandom.RandomInt(80, 120);
			break;
		case 2:
			num2 = MBRandom.RandomInt(120, 160);
			break;
		case 3:
			num2 = MBRandom.RandomInt(160, 200);
			break;
		case 4:
			num2 = MBRandom.RandomInt(200, 241);
			break;
		case 5:
			num2 = Hero.MainHero.GetSkillValue(DefaultSkills.Crafting);
			break;
		}
		__result = Math.Min(299f, (float)num2 + town.Prosperity / 500f);
		return false;
	}
}
