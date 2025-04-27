using AdjustableLeveling.Settings;
using AdjustableLeveling.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace AdjustableLeveling.Leveling
{
	public enum SkillUserEnum
	{
		Default,
		NPC,
		Clan,
	}

	public static class SkillHelper
	{
		public static Dictionary<int, Func<SkillUserEnum, float>> SkillModifierGetters { get; } = [];
		public static List<int> WarnOnceList { get; } = [];

		static SkillHelper()
		{
			// Vigor
			AddSkill("OneHanded", DefaultSkills.OneHanded);
			AddSkill("TwoHanded", DefaultSkills.TwoHanded);
			AddSkill("Polearm", DefaultSkills.Polearm);
			// Control
			AddSkill("Bow", DefaultSkills.Bow);
			AddSkill("Crossbow", DefaultSkills.Crossbow);
			AddSkill("Throwing", DefaultSkills.Throwing);
			// Endurance
			AddSkill("Riding", DefaultSkills.Riding);
			AddSkill("Athletics", DefaultSkills.Athletics);
			AddSkill("Crafting", DefaultSkills.Crafting);
			// Cunning
			AddSkill("Scouting", DefaultSkills.Scouting);
			AddSkill("Tactics", DefaultSkills.Tactics);
			AddSkill("Roguery", DefaultSkills.Roguery);
			// Social
			AddSkill("Charm", DefaultSkills.Charm);
			AddSkill("Leadership", DefaultSkills.Leadership);
			AddSkill("Trade", DefaultSkills.Trade);
			// Intelligence
			AddSkill("Steward", DefaultSkills.Steward);
			AddSkill("Medicine", DefaultSkills.Medicine);
			AddSkill("Engineering", DefaultSkills.Engineering);
		}

		public static SkillUserEnum GetSkillUser(this Hero hero)
		{
			SkillUserEnum output;
			if (hero?.CharacterObject.IsPlayerCharacter == false)
			{
				if (hero.Clan == Clan.PlayerClan 
					&& !(MCMSettings.Settings.ClanAsCompanionOnly && hero.CompanionOf == null))
					output = SkillUserEnum.Clan;
				else
					output = SkillUserEnum.NPC;
			}
			else
				output = SkillUserEnum.Default;
			//AdjustableLevelingUtility.Message($"'{hero}' '{hero?.Clan}' / '{hero?.MapFaction}' -> {output}", false);
			return output;
		}

		public static float GetSkillModifier(this SkillObject skill, Hero hero)
		{
			float modifier;
			var skillUser = hero.GetSkillUser();

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
					modifier = MCMSettings.Settings.ClanSkillXPModifier;
					//AdjustableLevelingUtility.Message($"ClanSkillXPModifier {modifier}", false);
					if (modifier > 0f)
						return modifier;

					// fallthrough
					goto case SkillUserEnum.NPC;

				// overall NPC skill modifier
				case SkillUserEnum.NPC:
					modifier = MCMSettings.Settings.NPCSkillXPModifier;
					//AdjustableLevelingUtility.Message($"NPCSkillXPModifier {modifier}", false);
					if (modifier > 0f)
						return modifier;

					// fallthrough
					goto case SkillUserEnum.Default;

				// overall default skill modifier
				default:
				case SkillUserEnum.Default:
					//AdjustableLevelingUtility.Message($"SkillXPModifier {MCMSettings.Settings.SkillXPModifier}", false);
					return MCMSettings.Settings.SkillXPModifier;
			}
		}

		public static void AddSkill(string id, SkillObject skill)
		{
			try
			{
				var hashCode = skill.GetHashCode();
				SkillModifierGetters[hashCode] = (skillUser) =>
				{
					float modifier;
					switch (skillUser)
					{
						// Clan skill modifier
						case SkillUserEnum.Clan:
							if (MCMSettings.Settings.SkillXPModifiers.TryGetValue(MCMSettings.ClanTag + id, out modifier) && modifier > 0f)
								return modifier;
							goto case SkillUserEnum.NPC;

						// NPC skill modifier
						case SkillUserEnum.NPC:
							if (MCMSettings.Settings.SkillXPModifiers.TryGetValue(MCMSettings.NPCTag + id, out modifier) && modifier > 0f)
								return modifier;
							goto case SkillUserEnum.Default;

						// Default skill modifier
						default:
						case SkillUserEnum.Default:
							if (MCMSettings.Settings.SkillXPModifiers.TryGetValue(MCMSettings.BaseTag + id, out modifier))
								return modifier;

							// skill not found, show warning and return 1
							if (!WarnOnceList.Contains(hashCode))
							{
								GeneralUtility.Message($"WARNING: {nameof(SkillHelper)} could not find skill '{id}' ({skill?.Name}) for '{skillUser}' in dictionary, defaulting skill modifier to 1x", false, Colors.Yellow);
								WarnOnceList.Add(hashCode);
							}
							return 1f;
					}
				};
			}
			catch (Exception exc)
			{
				GeneralUtility.Message($"ERROR: Adjustable Leveling failed at ({nameof(SkillHelper)}.{nameof(AddSkill)}): {exc.GetType()}: {exc.Message}\n{exc.StackTrace}");
			}
		}
	}
}
