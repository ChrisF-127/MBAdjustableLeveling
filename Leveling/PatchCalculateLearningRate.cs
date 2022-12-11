using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace AdjustableLeveling.Leveling;

[HarmonyPatch(typeof(DefaultCharacterDevelopmentModel), "CalculateLearningRate", new Type[]
{
	typeof(int),
	typeof(int),
	typeof(int),
	typeof(int),
	typeof(TextObject),
	typeof(bool)
})]
internal class PatchCalculateLearningRate
{
	public static bool Prefix(
		ref ExplainedNumber __result, 
		ref DefaultCharacterDevelopmentModel __instance, 
		int attributeValue, 
		int focusValue, 
		int skillValue, 
		//int characterLevel, 
		TextObject attributeName, 
		bool includeDescriptions,
		TextObject ____skillFocusText,
		TextObject ____overLimitText)
	{
		int focusLimit = MathF.Round(__instance.CalculateLearningLimit(attributeValue, focusValue, null).ResultNumber);
		int attributeLimitIncrease = (int)(attributeValue * AdjustableLeveling.Settings.MaxSkillLimitIncreasePerAttributePoint);
		var explainedNumber = new ExplainedNumber(0f, includeDescriptions);
		if (skillValue < focusLimit)
		{
			var skillFactor = 1f - skillValue / (float)focusLimit;
			explainedNumber.Add((0.25f + 0.5f * skillFactor) * attributeValue, attributeName);
			explainedNumber.Add((0.50f +        skillFactor) * focusValue, ____skillFocusText);
		}
		else if (skillValue < focusLimit + attributeLimitIncrease)
		{
			var skillFactor = 1f - (skillValue - focusLimit) / (float)attributeLimitIncrease;
			explainedNumber.Add(0.25f * skillFactor * attributeValue, attributeName);
			explainedNumber.Add(0.50f * skillFactor * focusValue, ____skillFocusText);
		}
		else
		{
			explainedNumber.Add(-1f, ____overLimitText);
		}

		explainedNumber.LimitMin(0f);
		__result = explainedNumber;
		return false;
	}
}
