using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace AdjustableLeveling
{
	public static class AdjCharDevModelUtility
	{
		private static readonly TextObject _skillFocusText;
		private static readonly TextObject _overLimitText;

		static AdjCharDevModelUtility()
		{
			try
			{
				_skillFocusText = (TextObject)AccessTools.Field(typeof(DefaultCharacterDevelopmentModel), nameof(_skillFocusText)).GetValue(null);
				_overLimitText = (TextObject)AccessTools.Field(typeof(DefaultCharacterDevelopmentModel), nameof(_overLimitText)).GetValue(null);
			}
			catch (Exception exc)
			{
				AdjLvlUtility.Message($"ERROR: Adjustable Leveling failed to initialize (static {nameof(AdjCharDevModelUtility)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
			}
		}

		public static void Initialize(DefaultCharacterDevelopmentModel cdm, out int[] skillsRequiredForLevel)
		{
			skillsRequiredForLevel = new int[1025];
			try
			{
				//skillsRequiredForLevel[0] = 0;
				for (int i = 1; i < skillsRequiredForLevel.Length; i++)
					skillsRequiredForLevel[i] = skillsRequiredForLevel[i - 1] + (int)(MCMSettings.Settings.UseFasterLevelingCurve ? 500f * MathF.Pow(i, 2f) : 25f * MathF.Pow(i, 3f));

				// overwrite private _skillsRequiredForLevel-field
				AccessTools.Field(typeof(DefaultCharacterDevelopmentModel), "_skillsRequiredForLevel").SetValue(cdm, skillsRequiredForLevel);
			}
			catch (Exception exc)
			{
				AdjLvlUtility.Message($"ERROR: Adjustable Leveling failed to initialize (public {nameof(AdjCharDevModelUtility)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
			}
		}

		public static int SkillsRequiredForLevel(int level, int[] skillsRequiredForLevel) =>
			(level > MCMSettings.Settings.MaxCharacterLevel || level >= skillsRequiredForLevel.Length) ? int.MaxValue : skillsRequiredForLevel[level];

		public static ExplainedNumber CalculateLearningLimit(
			int attributeValue,
			int focusValue,
			TextObject attributeName,
			bool includeDescriptions = false)
		{
			var explainedNumber = new ExplainedNumber(MCMSettings.Settings.BaseLearningLimit, includeDescriptions);
			explainedNumber.Add(
				focusValue * MCMSettings.Settings.LearningLimitIncreasePerFocusPoint,
				_skillFocusText);
			explainedNumber.Add(
				attributeValue * MCMSettings.Settings.LearningLimitIncreasePerAttributePoint,
				attributeName);
			explainedNumber.LimitMin(0f);
			return explainedNumber;
		}

		public static ExplainedNumber CalculateLearningRate(
			int attributeValue,
			int focusValue,
			int skillValue,
			int characterLevel,
			TextObject attributeName,
			bool includeDescriptions = false)
		{
			int baseLimit = MCMSettings.Settings.BaseLearningLimit;
			int focusLimit = focusValue * MCMSettings.Settings.LearningLimitIncreasePerFocusPoint;
			int attributeLimit = attributeValue * MCMSettings.Settings.LearningLimitIncreasePerAttributePoint;

			int focusMax = baseLimit + focusLimit;
			int finalMax = focusMax + attributeLimit;

			var explainedNumber = new ExplainedNumber(0f, includeDescriptions);
			if (skillValue < focusMax)
			{
				var factor = 1f - skillValue / (float)focusMax;
				explainedNumber.Add((0.25f + 0.5f * factor) * attributeValue, attributeName);
				explainedNumber.Add((0.50f + factor) * focusValue, _skillFocusText);
			}
			else if (skillValue < finalMax)
			{
				var factor = 1f - (skillValue - focusMax) / (float)attributeLimit;
				explainedNumber.Add(0.25f * factor * attributeValue, attributeName);
				explainedNumber.Add(0.50f * factor * focusValue, _skillFocusText);
			}
			else
			{
				explainedNumber.Add(-1f, _overLimitText);
			}

			explainedNumber.LimitMin(MCMSettings.Settings.MinLearningRate);
			if (MCMSettings.Settings.MaxLearningRate > 0f)
				explainedNumber.LimitMax(MCMSettings.Settings.MaxLearningRate);
			return explainedNumber;
		}
	}
}
