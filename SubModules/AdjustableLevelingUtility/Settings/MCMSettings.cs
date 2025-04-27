using AdjustableLeveling.Utility;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.FluentBuilder;
using MCM.Common;
using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling.Settings
{
	public enum SkillUserEnum
	{
		Default,
		NPC,
		Clan,
	}

	public class MCMSettings
	{
		#region CONSTANTS
		public const string Id = "AdjustableLeveling";
		public const string DisplayName = "Adjustable Leveling";
		public const string FolderName = "Global/AdjustableLeveling";
		public const string FormatType = "json";

		public const string BaseTag = "Base_";
		public const string NPCTag = "NPC_";
		public const string ClanTag = "Clan_";

		private const string SkillLevelingSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_Skills}Skills";
		private const string BaseOverrideHintText = "{=adjlvl_hint_SkillOverride}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for this specific skill, when not 0. [Default: 0.00]";

		private const string SkillLevelingNPCSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_NPCSkills}NPC Skills";
		private const string NPCOverrideHintText = "{=adjlvl_hint_NPCSkillOverride}Overrides modifiers for this specific skill for NPCs only, when not 0. [Default: 0.00]";

		private const string SkillLevelingClanSkillsGroupName = "{=adjlvl_group_SkillLeveling}Skill Leveling/{=adjlvl_group_ClanSkills}Clan Member or Companion Skills";
		private const string ClanOverrideHintText = "{=adjlvl_hint_ClanSkillOverride}Overrides modifiers for this specific skill for clan members or companions only, when not 0. [Default: 0.00]";
		#endregion

		#region PROPERTIES
		public static MCMSettings Settings { get; set; }

		public FluentGlobalSettings GlobalSettings { get; private set; }
		public FluentPerCampaignSettings PerCampaignSettings { get; private set; }

		private Dictionary<string, Func<SkillObject>> SkillObjectGetters { get; } = [];
		private Dictionary<int, Func<SkillUserEnum, float>> SkillModifierGetters { get; } = [];
		private List<int> WarnOnceList { get; } = [];

		#region SETTINGS PROPERTIES
		#region CHARACTER LEVELING MODIFIERS
		public bool UseFasterLevelingCurve { get; set; } = false;

		public int MaxCharacterLevel { get; set; } = 62;
		public float LevelXPModifier { get; set; } = 1f;

		public int LevelsPerAttributePoint { get; set; } = 4;
		public int FocusPointsPerLevel { get; set; } = 1;

		public int MaxAttribute { get; set; } = 10;
		public int MaxFocusPerSkill { get; set; } = 5;

		public int AttributePointsAtStart { get; set; } = 15;
		public int FocusPointsAtStart { get; set; } = 5;
		#endregion

		#region SKILL LEVELING MODIFIERS
		#region GENERAL
		public int LearningLimitIncreasePerAttributePoint { get; set; } = 3;
		public int LearningLimitIncreasePerFocusPoint { get; set; } = 50;
		public int BaseLearningLimit { get; set; } = 50;

		public float MinLearningRate { get; set; } = 0f;
		public float MaxLearningRate { get; set; } = 0f;

		public float SkillXPModifier { get; set; } = 1f;
		public float NPCSkillXPModifier { get; set; } = 0f;
		public float ClanSkillXPModifier { get; set; } = 0f;
		public bool ClanAsCompanionOnly { get; set; } = false;
		#endregion

		#region SKILL MODIFIERS
		public Dictionary<string, float> SkillXPModifiers { get; } = [];
		#endregion
		#endregion

		#region OTHER STUFF
		public float TroopXPModifier { get; set; } = 1f;
		#endregion

		#region SMITHING PART RESEARCH MODIFIERS
		public float SmithingResearchModifier { get; set; } = 1f;
		public float SmithingFreeBuildResearchModifier { get; set; } = 0.1f;
		#endregion
		#endregion
		#endregion

		#region FIELDS
		private readonly ISettingsBuilder _settingsBuilder;
		private ISettingsPropertyGroupBuilder _baseSkillsGroupBuilder;
		private ISettingsPropertyGroupBuilder _npcSkillsGroupBuilder;
		private ISettingsPropertyGroupBuilder _clanSkillsGroupBuilder;

		private int _groupOrder = 0;
		private int _characterLevelingPropertyOrder = 0;
		private int _baseSkillLevelingPropertyOrder = 0;
		private int _npcSkillLevelingPropertyOrder = 0;
		private int _clanSkillLevelingPropertyOrder = 0;
		private int _otherPropertyOrder = 0;
		private int _smithingPropertyOrder = 0;
		#endregion

		#region CONSTRUCTORS
		public MCMSettings()
		{
			#region SETTINGS
			_settingsBuilder = BaseSettingsBuilder.Create(Id, DisplayName)
				.SetFormat(FormatType)
				.SetFolderName(FolderName)
				.SetSubFolder(string.Empty)

			#region CHARACTER LEVELING MODIFIERS
				.CreateGroup("{=adjlvl_group_CharacterLeveling}Character Leveling", g => g
					.SetGroupOrder(_groupOrder++)
					.AddBool(
						nameof(UseFasterLevelingCurve),
						"{=adjlvl_name_FasterLevelingCurve}Faster Leveling Curve",
						new ProxyRef<bool>(() => UseFasterLevelingCurve, v => UseFasterLevelingCurve = v), b => b
						.SetHintText("{=adjlvl_hint_FasterLevelingCurve}Slower earlier but faster later levels, level 62 total: 40.7m [ON] vs 95.4m [OFF]. [Default: OFF]\n-WARNING: Backup save recommended, changing this in an ongoing save will reset the level xp to half-way to the next level (if total xp is out of bounds for the current level after conversion)!")
						.SetOrder(_characterLevelingPropertyOrder++))

					.AddInteger(
						nameof(MaxCharacterLevel),
						"{=adjlvl_name_MaxCharacterLevel}Maximum Character Level",
						5,
						1024,
						new ProxyRef<int>(() => MaxCharacterLevel, v => MaxCharacterLevel = v), b => b
						.SetHintText("{=adjlvl_hint_MaxCharacterLevel}Adjust the maximum achievable character level. Higher levels require much more xp! [Default: 62]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddFloatingInteger(
						nameof(LevelXPModifier),
						"{=adjlvl_name_CharacterLevelXPModifier}Character Level XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => LevelXPModifier, v => LevelXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_CharacterLevelXPModifier}Adjust how skill xp is converted into level xp, default is 1-to-1 at 1.00. [Default: 1.00]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddInteger(
						nameof(LevelsPerAttributePoint),
						"{=adjlvl_name_LevelsPerAttributePoint}Levels per Attribute Point",
						1,
						10,
						new ProxyRef<int>(() => LevelsPerAttributePoint, v => LevelsPerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LevelsPerAttributePoint}Number of level ups required to gain an attribute point. Only affects future level ups, so it should be changed before starting a new campaign to take full effect! [Default: 4]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsPerLevel),
						"{=adjlvl_name_FocusPointsPerLevel}Focus Points per Level",
						1,
						10,
						new ProxyRef<int>(() => FocusPointsPerLevel, v => FocusPointsPerLevel = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsPerLevel}Focus points gained per level. [Default: 1]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(MaxAttribute),
						"{=adjlvl_name_MaxAttributePointsForAttribute}Max Attribute Points for Attribute",
						1,
						1000,
						new ProxyRef<int>(() => MaxAttribute, v => MaxAttribute = v), b => b
						.SetHintText("{=adjlvl_hint_MaxAttributePointsForAttribute}Attribute point limit per attribute. [Default: 10]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(MaxFocusPerSkill),
						"{=adjlvl_name_MaxFocusPointsForSkill}Max Focus Points for Skill",
						1,
						1000,
						new ProxyRef<int>(() => MaxFocusPerSkill, v => MaxFocusPerSkill = v), b => b
						.SetHintText("{=adjlvl_hint_MaxFocusPointsForSkill}Focus point limit per skill. (UI will at most show 5 points) [Default: 5]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddInteger(
						nameof(AttributePointsAtStart),
						"{=adjlvl_name_AttributePointsAtStart}Attribute Points at Start",
						1,
						100,
						new ProxyRef<int>(() => AttributePointsAtStart, v => AttributePointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_AttributePointsAtStart}Apparently affects the attribute points with which NPCs start, but not the player. [Default: 15]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(FocusPointsAtStart),
						"{=adjlvl_name_FocusPointsAtStart}Focus Points at Start",
						1,
						100,
						new ProxyRef<int>(() => FocusPointsAtStart, v => FocusPointsAtStart = v), b => b
						.SetHintText("{=adjlvl_hint_FocusPointsAtStart}Apparently affects the focus points with which NPCs start, but not the player. [Default: 5]")
						.SetOrder(_characterLevelingPropertyOrder++)
						.AddValueFormat("0"))
					)
			#endregion

			#region SKILL LEVELING SETTINGS
				.CreateGroup("{=adjlvl_group_SkillLeveling}Skill Leveling", g => g
					.SetGroupOrder(_groupOrder++)
					.AddInteger(
						nameof(LearningLimitIncreasePerAttributePoint),
						"{=adjlvl_name_LearningLimitAttributePoint}Learning Limit Increase per Attribute Point",
						0,
						50,
						new ProxyRef<int>(() => LearningLimitIncreasePerAttributePoint, v => LearningLimitIncreasePerAttributePoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitAttributePoint}E.g. at 3 and with 10 AP an additional 30 skill points can be gained at reducing learning rate; at 5 an additional 50 can be gained. [Default: 3]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(LearningLimitIncreasePerFocusPoint),
						"{=adjlvl_name_LearningLimitFocusPoint}Learning Limit Increase per Focus Point",
						0,
						100,
						new ProxyRef<int>(() => LearningLimitIncreasePerFocusPoint, v => LearningLimitIncreasePerFocusPoint = v), b => b
						.SetHintText("{=adjlvl_hint_LearningLimitFocusPoint}Adjust the learning limit increase per focus point. [Default: 50]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))
					.AddInteger(
						nameof(BaseLearningLimit),
						"{=adjlvl_name_BaseLearningLimit}Base Learning Limit",
						0,
						100,
						new ProxyRef<int>(() => BaseLearningLimit, v => BaseLearningLimit = v), b => b
						.SetHintText("{=adjlvl_hint_BaseLearningLimit}The base learning limit. [Default: 50]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0"))

					.AddFloatingInteger(
						nameof(MinLearningRate),
						"{=adjlvl_name_MinLearningRate}Minimum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MinLearningRate, v => MinLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MinLearningRate}Set a minimum learning rate. [Default: 0.00]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(MaxLearningRate),
						"{=adjlvl_name_MaxLearningRate}Maximum Learning Rate",
						0f,
						100f,
						new ProxyRef<float>(() => MaxLearningRate, v => MaxLearningRate = v), b => b
						.SetHintText("{=adjlvl_hint_MaxLearningRate}Set a maximum learning rate, zero disables it. [Default: 0.00]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))

					.AddFloatingInteger(
						nameof(SkillXPModifier),
						"{=adjlvl_name_SkillXPModifier}Skill XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SkillXPModifier, v => SkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_SkillXPModifier}Adjust the overall skill learning rate. [Default: 1.00]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(NPCSkillXPModifier),
						"{=adjlvl_name_NPCSkillXPModifier}NPC Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => NPCSkillXPModifier, v => NPCSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_NPCSkillXPModifier}Overrides 'Skill XP Modifier' for NPCs, when not 0. [Default: 0.00]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(ClanSkillXPModifier),
						"{=adjlvl_name_ClanSkillXPModifier}Clan/Companion Skill XP Modifier",
						0f,
						100f,
						new ProxyRef<float>(() => ClanSkillXPModifier, v => ClanSkillXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_ClanSkillXPModifier}Overrides 'Skill XP Modifier' and 'NPC Skill XP Modifier' for clan members or companions, when not 0. [Default: 0.00]")
						.SetOrder(_baseSkillLevelingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddBool(
						nameof(ClanAsCompanionOnly),
						"{=adjlvl_name_ClanAsCompanionOnly}Clan Modifiers affect Companions only",
						new ProxyRef<bool>(() => ClanAsCompanionOnly, v => ClanAsCompanionOnly = v), b => b
						.SetHintText("{=adjlvl_hint_ClanAsCompanionOnly}Clan modifiers only affect Companions. [Default: OFF]")
						.SetOrder(_baseSkillLevelingPropertyOrder++))
					)

				 // Skill Leveling Modifier Sub-Groups
				.CreateGroup(SkillLevelingSkillsGroupName, g =>
					_baseSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
				.CreateGroup(SkillLevelingNPCSkillsGroupName, g =>
					_npcSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
				.CreateGroup(SkillLevelingClanSkillsGroupName, g =>
					_clanSkillsGroupBuilder = g.SetGroupOrder(_groupOrder++))
			#endregion

			#region OTHER STUFF
				.CreateGroup("{=adjlvl_group_Other}Other", g => g
					.SetGroupOrder(_groupOrder++)
					.AddFloatingInteger(
						nameof(TroopXPModifier),
						"{=adjlvl_name_TroopXPModifier}Troop XP Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => TroopXPModifier, v => TroopXPModifier = v), b => b
						.SetHintText("{=adjlvl_hint_TroopXPModifier}Modifies XP gained for upgrading troops. (Required XP numbers in roster will show unmodified values, but dropping equipment is limited to modified value.) [Default 1.00]")
						.SetOrder(_otherPropertyOrder++)
						.AddValueFormat("0.00"))
					)
			#endregion

			#region SMITHING PART RESEARCH MODIFIERS
				.CreateGroup("{=adjlvl_group_SmithingResearch}Smithing Research", g => g
					.SetGroupOrder(_groupOrder++)
					.AddFloatingInteger(
						nameof(SmithingResearchModifier),
						"{=adjlvl_name_PartResearch}Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingResearchModifier, v => SmithingResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_PartResearch}Adjust smithing part research gain rate for smithing and smelting weapons. [Default: 100%]")
						.SetOrder(_smithingPropertyOrder++)
						.AddValueFormat("0.00"))
					.AddFloatingInteger(
						nameof(SmithingFreeBuildResearchModifier),
						"{=adjlvl_name_FreeBuildPartResearch}Free Build Part Research Modifier",
						0.01f,
						100f,
						new ProxyRef<float>(() => SmithingFreeBuildResearchModifier, v => SmithingFreeBuildResearchModifier = v), b => b
						.SetHintText("{=adjlvl_hint_FreeBuildPartResearch}Adjust smithing part research gain rate when in free build mode. With the default setting, unlocking parts is slow in free build mode. [Default: 10%]")
						.SetOrder(_smithingPropertyOrder++)
						.AddValueFormat("0.00"))
				);
			#endregion

			#region SKILL LEVELING MODIFIERS
			// VIGOR
			AddSkill("OneHanded", "{=PiHpR4QL}One Handed", () => DefaultSkills.OneHanded);
			AddSkill("TwoHanded", "{=t78atYqH}Two Handed", () => DefaultSkills.TwoHanded);
			AddSkill("Polearm", "{=haax8kMa}Polearm", () => DefaultSkills.Polearm);
			// CONTROL
			AddSkill("Bow", "{=5rj7xQE4}Bow", () => DefaultSkills.Bow);
			AddSkill("Crossbow", "{=TTWL7RLe}Crossbow", () => DefaultSkills.Crossbow);
			AddSkill("Throwing", "{=2wclahIJ}Throwing", () => DefaultSkills.Throwing);
			// ENDURANCE
			AddSkill("Riding", "{=p9i3zRm9}Riding", () => DefaultSkills.Riding);
			AddSkill("Athletics", "{=skZS2UlW}Athletics", () => DefaultSkills.Athletics);
			AddSkill("Crafting", "{=smithingskill}Smithing", () => DefaultSkills.Crafting);
			// CUNNING
			AddSkill("Scouting", "{=LJ6Krlbr}Scouting", () => DefaultSkills.Scouting);
			AddSkill("Tactics", "{=m8o51fc7}Tactics", () => DefaultSkills.Tactics);
			AddSkill("Roguery", "{=V0ZMJ0PX}Roguery", () => DefaultSkills.Roguery);
			// SOCIAL
			AddSkill("Charm", "{=EGeY1gfs}Charm", () => DefaultSkills.Charm);
			AddSkill("Leadership", "{=HsLfmEmb}Leadership", () => DefaultSkills.Leadership);
			AddSkill("Trade", "{=GmcgoiGy}Trade", () => DefaultSkills.Trade);
			// INTELLIGENCE
			AddSkill("Steward", "{=stewardskill}Steward", () => DefaultSkills.Steward);
			AddSkill("Medicine", "{=JKH59XNp}Medicine", () => DefaultSkills.Medicine);
			AddSkill("Engineering", "{=engineeringskill}Engineering", () => DefaultSkills.Engineering);
			#endregion
			#endregion
		}
		#endregion

		#region PUBLIC METHODS
		public void Build()
		{
			// create global settings
			GlobalSettings = _settingsBuilder.BuildAsGlobal();
			// create per campaign settings
			PerCampaignSettings = _settingsBuilder.BuildAsPerCampaign();

			// register global settings
			GlobalSettings.Register();
		}
		public void OnGameEnd()
		{
			SkillModifierGetters.Clear();

			PerCampaignSettings.Unregister();
			GlobalSettings.Register();
		}
		public void OnGameInitializationFinished()
		{
			InitializeSkillModifierGetters();

			GlobalSettings.Unregister();
			PerCampaignSettings.Register();
		}

		public void AddSkill(string id, string name, Func<SkillObject> getSkillObject)
		{
			try
			{
				// Base settings
				SkillXPModifiers.Add(BaseTag + id, 0f);
				_baseSkillsGroupBuilder.AddFloatingInteger(
					"SkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(BaseTag + id), b => b
					.SetHintText(BaseOverrideHintText)
					.SetOrder(_baseSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// NPC settings
				SkillXPModifiers.Add(NPCTag + id, 0f);
				_npcSkillsGroupBuilder.AddFloatingInteger(
					"NPCSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(NPCTag + id), b => b
					.SetHintText(NPCOverrideHintText)
					.SetOrder(_npcSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// Clan settings
				SkillXPModifiers.Add(ClanTag + id, 0f);
				_clanSkillsGroupBuilder.AddFloatingInteger(
					"ClanSkillXPModifier_" + id,
					name,
					0f,
					100f,
					createSkillProxy(ClanTag + id), b => b
					.SetHintText(ClanOverrideHintText)
					.SetOrder(_clanSkillLevelingPropertyOrder++)
					.AddValueFormat("0.00"));

				// Intialize SkillObject-Getter for SkillHelper
				SkillObjectGetters.Add(id, getSkillObject);
			}
			catch (Exception exc)
			{
				GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(MCMSettings)}.{nameof(AddSkill)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
			}

			ProxyRef<float> createSkillProxy(string name) =>
				new(() => SkillXPModifiers[name], v => SkillXPModifiers[name] = v);
		}

		public float GetSkillModifier(SkillObject skill, Hero hero)
		{
			float modifier;
			var skillUser = GetSkillUser(hero);

			// check skill specific modifiers
			if (skill != null && SkillModifierGetters.TryGetValue(skill.GetHashCode(), out var func))
			{
				modifier = func(skillUser);
				//AdjustableLevelingUtility.Message($"Specific {modifier}", false);
				if (modifier > 0f)
					return modifier;
			}

			switch (skillUser)
			{
				// overall clan skill modifier
				case SkillUserEnum.Clan:
					modifier = ClanSkillXPModifier;
					//AdjustableLevelingUtility.Message($"ClanSkillXPModifier {modifier}", false);
					if (modifier > 0f)
						return modifier;

					// fallthrough
					goto case SkillUserEnum.NPC;

				// overall NPC skill modifier
				case SkillUserEnum.NPC:
					modifier = NPCSkillXPModifier;
					//AdjustableLevelingUtility.Message($"NPCSkillXPModifier {modifier}", false);
					if (modifier > 0f)
						return modifier;

					// fallthrough
					goto case SkillUserEnum.Default;

				// overall default skill modifier
				default:
				case SkillUserEnum.Default:
					//AdjustableLevelingUtility.Message($"SkillXPModifier {Settings.SkillXPModifier}", false);
					return SkillXPModifier;
			}
		}
		#endregion

		#region PRIVATE METHODS
		private SkillUserEnum GetSkillUser(Hero hero)
		{
			SkillUserEnum output;
			if (hero?.CharacterObject.IsPlayerCharacter == false)
			{
				if (hero.Clan == Clan.PlayerClan
					&& !(ClanAsCompanionOnly && hero.CompanionOf == null))
					output = SkillUserEnum.Clan;
				else
					output = SkillUserEnum.NPC;
			}
			else
				output = SkillUserEnum.Default;
			//AdjustableLevelingUtility.Message($"'{hero}' '{hero?.Clan}' / '{hero?.MapFaction}' -> {output}", false);
			return output;
		}

		private void InitializeSkillModifierGetters()
		{
			// clear previous skill modifier getters just in case
			SkillModifierGetters.Clear();

			// initialize skill modifier getters from skill object getters
			foreach (var item in SkillObjectGetters)
			{
				var id = item.Key;
				try
				{
					var skill = item.Value();
					var hashCode = skill.GetHashCode();
					SkillModifierGetters[hashCode] = skillUser =>
					{
						float modifier;
						switch (skillUser)
						{
							// Clan skill modifier
							case SkillUserEnum.Clan:
								if (SkillXPModifiers.TryGetValue(ClanTag + id, out modifier) && modifier > 0f)
									return modifier;
								goto case SkillUserEnum.NPC;

							// NPC skill modifier
							case SkillUserEnum.NPC:
								if (SkillXPModifiers.TryGetValue(NPCTag + id, out modifier) && modifier > 0f)
									return modifier;
								goto case SkillUserEnum.Default;

							// Default skill modifier
							default:
							case SkillUserEnum.Default:
								if (SkillXPModifiers.TryGetValue(BaseTag + id, out modifier))
									return modifier;

								// skill not found, show warning and return 1
								if (!WarnOnceList.Contains(hashCode))
								{
									GeneralUtility.Message($"WARNING: Adjustable Leveling could not find skill '{id}' ({skill?.Name}) for '{skillUser}' in dictionary, defaulting 1x [will only warn once]", false, Colors.Yellow);
									WarnOnceList.Add(hashCode);
								}
								return 1f;
						}
					};
				}
				catch (Exception exception)
				{
					GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(InitializeSkillModifierGetters)}): id: {id}" +
						$"\n{exception.GetType()}: {exception.Message}\n{exception.StackTrace}");
				}
			}
		}
		#endregion
	}
}