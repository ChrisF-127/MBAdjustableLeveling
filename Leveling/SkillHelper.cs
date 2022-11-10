using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using static TaleWorlds.InputSystem.HotKey;

namespace AdjustableLeveling.Leveling
{
	internal static class SkillHelper
	{
		private static Dictionary<SkillObject, Func<bool, float>> SkillModifiers = new();

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
			var isNPC = hero?.CharacterObject.IsPlayerCharacter == false;

			float modifier;
			
			// check skill specific modifiers
			if (SkillModifiers.TryGetValue(skill, out var func))
			{
				modifier = func(isNPC);
				if (modifier > 0f)
					return modifier;
			}

			if (isNPC)
			{
				// overall NPC skill modifier
				modifier = AdjustableLeveling.Settings.NPCSkillXPModifier;
				if (modifier > 0f)
					return modifier;
			}

			// overall skill modifier
			return AdjustableLeveling.Settings.SkillXPModifier;
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

				SkillModifiers[skill] = (isNPC) =>
				{
					float modifier;
					if (isNPC)
					{
						// NPC skill modifier
						modifier = (float)npcModifierGetter.Invoke(AdjustableLeveling.Settings, null);
						if (modifier > 0f)
							return modifier;
					}

					// skill modifier
					return (float)modifierGetter.Invoke(AdjustableLeveling.Settings, null);
				};
			}
			catch (Exception exc)
			{
				FileLog.Log(exc.ToString());
			}
		}
	}
}
