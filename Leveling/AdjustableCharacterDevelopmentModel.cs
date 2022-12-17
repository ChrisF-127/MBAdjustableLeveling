using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace AdjustableLeveling.Leveling
{
	public class AdjustableCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
	{
		private static readonly TextObject _attributeText;
		private static readonly TextObject _skillFocusText;
		private static readonly TextObject _overLimitText;

		private readonly int[] _skillsRequiredForLevel = new int[201];

		public override int MaxAttribute => 
			AdjustableLeveling.Settings.MaxAttribute;
		public override int MaxFocusPerSkill => 
			AdjustableLeveling.Settings.MaxFocusPerSkill;
		public override int FocusPointsPerLevel => 
			AdjustableLeveling.Settings.FocusPointsPerLevel;
		public override int FocusPointsAtStart => 
			AdjustableLeveling.Settings.FocusPointsAtStart;
		public override int AttributePointsAtStart => 
			AdjustableLeveling.Settings.AttributePointsAtStart;
		public override int LevelsPerAttributePoint => 
			AdjustableLeveling.Settings.LevelsPerAttributePoint;

		static AdjustableCharacterDevelopmentModel() 
		{
			try
			{
				_attributeText = (TextObject)typeof(DefaultCharacterDevelopmentModel).GetField(nameof(_attributeText), BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
				_skillFocusText = (TextObject)typeof(DefaultCharacterDevelopmentModel).GetField(nameof(_skillFocusText), BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
				_overLimitText = (TextObject)typeof(DefaultCharacterDevelopmentModel).GetField(nameof(_overLimitText), BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
			}
			catch (Exception exc)
			{
				var text = $"ERROR: Adjustable Leveling failed to initialize (static {nameof(AdjustableCharacterDevelopmentModel)}):";
				InformationManager.DisplayMessage(new InformationMessage(text + exc.GetType().ToString(), new Color(1f, 0f, 0f)));
				FileLog.Log(text + "\n" + exc.ToString());
			}
		}

		public AdjustableCharacterDevelopmentModel() : base()
		{
			try
			{
				// overwrite private _skillsRequiredForLevel-field
				GenerateSkillsRequiredForLevel();
			}
			catch (Exception exc)
			{
				var text = $"ERROR: Adjustable Leveling failed to initialize (public {nameof(AdjustableCharacterDevelopmentModel)}):";
				InformationManager.DisplayMessage(new InformationMessage(text + exc.GetType().ToString(), new Color(1f, 0f, 0f)));
				FileLog.Log(text + "\n" + exc.ToString());
			}
		}

		public void GenerateSkillsRequiredForLevel()
		{
			_skillsRequiredForLevel[0] = 0;
			for (int i = 1; i < _skillsRequiredForLevel.Length; i++)
				_skillsRequiredForLevel[i] = _skillsRequiredForLevel[i - 1] + (int)(AdjustableLeveling.Settings.UseFasterLevelingCurve ? 500f * MathF.Pow(i, 2f) : 25f * MathF.Pow(i, 3f));
			typeof(DefaultCharacterDevelopmentModel).GetField(nameof(_skillsRequiredForLevel), BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, _skillsRequiredForLevel);
		}


		public override int SkillsRequiredForLevel(int level) =>
			(level > AdjustableLeveling.Settings.MaxCharacterLevel || level >= _skillsRequiredForLevel.Length) ? int.MaxValue : _skillsRequiredForLevel[level];

		public override ExplainedNumber CalculateLearningLimit(int attributeValue, int focusValue, TextObject attributeName, bool includeDescriptions = false)
		{
			var learningLimitPerFP = AdjustableLeveling.Settings.LearningLimitIncreasePerFocusPoint;
			var explainedNumber = new ExplainedNumber(AdjustableLeveling.Settings.BaseLearningLimit, includeDescriptions);
			explainedNumber.Add(focusValue * learningLimitPerFP, _skillFocusText);
			explainedNumber.LimitMin(0f);
			return explainedNumber;
		}

		public override ExplainedNumber CalculateLearningRate(int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false)
		{
			int focusLimit = MathF.Round(CalculateLearningLimit(attributeValue, focusValue, null).ResultNumber);
			int attributeLimitIncrease = (int)(attributeValue * AdjustableLeveling.Settings.MaxSkillLimitIncreasePerAttributePoint);
			var explainedNumber = new ExplainedNumber(0f, includeDescriptions);
			if (skillValue < focusLimit)
			{
				var skillFactor = 1f - skillValue / (float)focusLimit;
				explainedNumber.Add((0.25f + 0.5f * skillFactor) * attributeValue, attributeName);
				explainedNumber.Add((0.50f + skillFactor) * focusValue, _skillFocusText);
			}
			else if (skillValue < focusLimit + attributeLimitIncrease)
			{
				var skillFactor = 1f - (skillValue - focusLimit) / (float)attributeLimitIncrease;
				explainedNumber.Add(0.25f * skillFactor * attributeValue, attributeName);
				explainedNumber.Add(0.50f * skillFactor * focusValue, _skillFocusText);
			}
			else
			{
				explainedNumber.Add(-1f, _overLimitText);
			}

			explainedNumber.LimitMin(0f);
			return explainedNumber;
		}
	}
}
