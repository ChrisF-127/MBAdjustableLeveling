using AdjustableLeveling.Leveling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ComponentInterfaces;

namespace AdjustableLeveling.Utility
{
	public static class TORUtility
	{
		public static CharacterDevelopmentModel SetCDM()
		{
#warning TODO add settings

			return new TORAdjustableCharacterDevelopmentModel();
		}
	}
}
