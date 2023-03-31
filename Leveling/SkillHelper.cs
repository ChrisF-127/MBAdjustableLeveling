using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace AdjustableLeveling.Leveling
{
	internal enum SkillUserEnum
	{
		Default,
		NPC,
		Companion,
	}

	internal static class SkillHelper
	{
		private static readonly Dictionary<SkillObject, Func<SkillUserEnum, float>> SkillModifiers = new();

		static SkillHelper()
		{
			// Vigor
			AddSkill(nameof(DefaultSkills.OneHanded));
			AddSkill(nameof(DefaultSkills.TwoHanded));
			AddSkill(nameof(DefaultSkills.Polearm));

			// Control
			AddSkill(nameof(DefaultSkills.Bow));
			AddSkill(nameof(DefaultSkills.Crossbow));
			AddSkill(nameof(DefaultSkills.Throwing));

			// Endurance
			AddSkill(nameof(DefaultSkills.Riding));
			AddSkill(nameof(DefaultSkills.Athletics));
			AddSkill(nameof(DefaultSkills.Crafting));

			// Cunning
			AddSkill(nameof(DefaultSkills.Scouting));
			AddSkill(nameof(DefaultSkills.Tactics));
			AddSkill(nameof(DefaultSkills.Roguery));

			// Social
			AddSkill(nameof(DefaultSkills.Charm));
			AddSkill(nameof(DefaultSkills.Leadership));
			AddSkill(nameof(DefaultSkills.Trade));

			// Intelligence
			AddSkill(nameof(DefaultSkills.Steward));
			AddSkill(nameof(DefaultSkills.Medicine));
			AddSkill(nameof(DefaultSkills.Engineering));
		}

		internal static float GetSkillModifier(this SkillObject skill, Hero hero)
		{
			float modifier;

			var skillUser = 
				hero?.CompanionOf != null ? 
				SkillUserEnum.Companion : 
				hero?.CharacterObject.IsPlayerCharacter == false ? 
				SkillUserEnum.NPC : 
				SkillUserEnum.Default;

			// check skill specific modifiers
			if (SkillModifiers.TryGetValue(skill, out var func))
			{
				modifier = func(skillUser);
				if (modifier > 0f)
					return modifier;
			}

			switch (skillUser)
			{
				// overall companion skill modifier
				case SkillUserEnum.Companion:
					modifier = AdjustableLeveling.Settings.NPCSkillXPModifier;
					if (modifier > 0f)
						return modifier;
					goto case SkillUserEnum.NPC;

				// overall NPC skill modifier
				case SkillUserEnum.NPC:
					modifier = AdjustableLeveling.Settings.NPCSkillXPModifier;
					if (modifier > 0f)
						return modifier;
					goto case SkillUserEnum.Default;

				// overall default skill modifier
				default:
				case SkillUserEnum.Default:
					return AdjustableLeveling.Settings.SkillXPModifier;
			}
		}

		/// <summary>
		/// Disgusting method that makes me wish C# had C/C++-macros, which would make this so much easier to handle.
		/// </summary>
		/// <param name="name"></param>
		private static void AddSkill(string name)
		{
			try
			{
				var skill = (SkillObject)typeof(DefaultSkills).GetProperty(name, BindingFlags.Static | BindingFlags.Public).GetValue(null);
				var modifierGetter = typeof(MCMSettings).GetProperty("SkillXPModifier_" + name).GetGetMethod();
				var npcModifierGetter = typeof(MCMSettings).GetProperty("NPCSkillXPModifier_" + name).GetGetMethod();
				var companionModifierGetter = typeof(MCMSettings).GetProperty("CompanionSkillXPModifier_" + name).GetGetMethod();

				SkillModifiers[skill] = (skillUser) =>
				{
					float modifier;
					switch (skillUser)
					{
						// Companion skill modifier
						case SkillUserEnum.Companion:
							modifier = (float)companionModifierGetter.Invoke(AdjustableLeveling.Settings, null);
							if (modifier > 0f)
								return modifier;
							goto case SkillUserEnum.NPC;

						// NPC skill modifier
						case SkillUserEnum.NPC:
							modifier = (float)npcModifierGetter.Invoke(AdjustableLeveling.Settings, null);
							if (modifier > 0f)
								return modifier;
							goto case SkillUserEnum.Default;

						// Default skill modifier
						default:
						case SkillUserEnum.Default:
							return (float)modifierGetter.Invoke(AdjustableLeveling.Settings, null);
					}
				};
			}
			catch (Exception exc)
			{
				FileLog.Log(exc.ToString());
			}
		}
	}
}
