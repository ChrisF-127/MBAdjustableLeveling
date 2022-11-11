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
	private static readonly TextObject _skillFocusText = new TextObject("{=MRktqZwu}Skill Focus");
	private static readonly TextObject _overLimitText = new TextObject("{=bcA7ZuyO}Learning Limit Exceeded");

	public static bool Prefix(ref ExplainedNumber __result, ref DefaultCharacterDevelopmentModel __instance, int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false)
	{
		int limit = MathF.Round(__instance.CalculateLearningLimit(attributeValue, focusValue, null).ResultNumber);
		var explainedNumber = new ExplainedNumber(1f, includeDescriptions: true);

		var attributeFactor = (attributeValue - 1f) * (1f - skillValue / (float)limit);
		var focusFactor = focusValue - 0.5f;

		explainedNumber.AddFactor(attributeFactor, attributeName);
		explainedNumber.AddFactor(focusFactor, _skillFocusText);
		
		if (skillValue >= limit) // set learning rate to 0
			explainedNumber.AddFactor(-(1f + attributeFactor + focusFactor), _overLimitText);

		explainedNumber.LimitMin(0f);
		__result = explainedNumber;
		return false;

		//var explainedNumber = new ExplainedNumber(0.5f, includeDescriptions: true);
		//explainedNumber.AddFactor(1f * attributeValue - 1f, attributeName);
		//explainedNumber.AddFactor(focusValue * 2f, _skillFocusText);
		//int num = MathF.Round(__instance.CalculateLearningLimit(attributeValue, focusValue, null).ResultNumber);
		//if (skillValue >= num)
		//{
		//	explainedNumber.AddFactor(0f - 1f * attributeValue - focusValue * 2f, _overLimitText);
		//}
		//explainedNumber.LimitMin(0f);
		//__result = explainedNumber;
		//return false;
	}
}
