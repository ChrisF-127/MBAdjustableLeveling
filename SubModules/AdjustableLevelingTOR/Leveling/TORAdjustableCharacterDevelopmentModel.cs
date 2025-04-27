using AdjustableLeveling.Settings;
using AdjustableLeveling.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TOR_Core.Models;

namespace AdjustableLeveling.Leveling
{
	public class TORAdjustableCharacterDevelopmentModel : TORCharacterDevelopmentModel
	{
		private readonly int[] _skillsRequiredForLevel;

		public override int MaxAttribute =>
			MCMSettings.Settings.MaxAttribute;
		public override int MaxFocusPerSkill =>
			MCMSettings.Settings.MaxFocusPerSkill;
		public override int FocusPointsPerLevel =>
			MCMSettings.Settings.FocusPointsPerLevel;
		public override int FocusPointsAtStart =>
			MCMSettings.Settings.FocusPointsAtStart;
		public override int AttributePointsAtStart =>
			MCMSettings.Settings.AttributePointsAtStart;
		public override int LevelsPerAttributePoint =>
			MCMSettings.Settings.LevelsPerAttributePoint;

		public TORAdjustableCharacterDevelopmentModel() : base()
		{
			AdjustableCharDevModelUtility.Initialize(this, out _skillsRequiredForLevel);
		}

		public override int SkillsRequiredForLevel(int level) =>
			AdjustableCharDevModelUtility.SkillsRequiredForLevel(level, _skillsRequiredForLevel);
		public override ExplainedNumber CalculateLearningLimit(int attributeValue, int focusValue, TextObject attributeName, bool includeDescriptions = false) =>
			AdjustableCharDevModelUtility.CalculateLearningLimit(attributeValue, focusValue, attributeName, includeDescriptions);
		public override ExplainedNumber CalculateLearningRate(int attributeValue, int focusValue, int skillValue, int characterLevel, TextObject attributeName, bool includeDescriptions = false) =>
			AdjustableCharDevModelUtility.CalculateLearningRate(attributeValue, focusValue, skillValue, characterLevel, attributeName, includeDescriptions);
	}
}
