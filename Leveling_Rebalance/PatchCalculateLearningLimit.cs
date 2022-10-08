using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Localization;

namespace Leveling_Rebalance;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningLimit", new Type[]
{
	typeof(int),
	typeof(int),
	typeof(TextObject),
	typeof(bool)
})]
internal class PatchCalculateLearningLimit
{
	private static TextObject _skillFocusText = new TextObject("{=MRktqZwu}Skill Focus");

	public static bool Prefix(ref ExplainedNumber __result, ref DefaultCharacterDevelopmentModel __instance, int attributeValue, int focusValue, TextObject attributeName, bool includeDescriptions = false)
	{
		ExplainedNumber explainedNumber = new ExplainedNumber(50f, includeDescriptions);
		explainedNumber.Add(focusValue * 50, _skillFocusText);
		explainedNumber.LimitMin(0f);
		__result = explainedNumber;
		return false;
	}
}
