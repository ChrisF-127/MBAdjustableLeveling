using AdjustableLeveling.Leveling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace AdjustableLeveling.Utility
{
	public static class AdjLvlTORUtility
	{
		public static CharacterDevelopmentModel CreateCDM()
		{
			var cdm = new TORAdjustableCharacterDevelopmentModel();

#warning TODO settings

			return cdm;
		}
	}
}
