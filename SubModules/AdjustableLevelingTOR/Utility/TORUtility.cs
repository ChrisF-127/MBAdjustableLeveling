using AdjustableLeveling.Leveling;
using AdjustableLeveling.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TOR_Core.CharacterDevelopment;

namespace AdjustableLeveling.Utility
{
	public static class TORUtility
	{
		public static CharacterDevelopmentModel InitializeCompatibility()
		{
			// DISCIPLINE
			MCMSettings.Settings.AddSkill("Faith", "{=tor_skill_faith_str}Faith", () => TORSkills.Faith);
			MCMSettings.Settings.AddSkill("GunPowder", "{=tor_skill_gunpowder_str}Gunpowder", () => TORSkills.GunPowder);
			MCMSettings.Settings.AddSkill("SpellCraft", "{=tor_skill_spellcraft_str}Spellcraft", () => TORSkills.SpellCraft);

			// create TOR character development model
			return new TORAdjustableCharacterDevelopmentModel();
		}
	}
}
