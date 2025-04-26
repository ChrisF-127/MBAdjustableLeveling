using AdjustableLeveling;
using AdjustableLeveling.Leveling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace AdjustableLevelingTOR
{
	public static class AdjLvlTORUtility
	{
		public static CharacterDevelopmentModel GetCDM() =>
			new TORAdjustableCharacterDevelopmentModel();
	}
}
