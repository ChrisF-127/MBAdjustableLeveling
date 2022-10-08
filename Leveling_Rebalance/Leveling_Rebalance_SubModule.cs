using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace Leveling_Rebalance;

public class Leveling_Rebalance_SubModule : MBSubModuleBase
{
	protected override void OnBeforeInitialModuleScreenSetAsRoot()
	{
		base.OnBeforeInitialModuleScreenSetAsRoot();
		InformationManager.DisplayMessage(new InformationMessage("Leveling Rebalance 2.0.2"));
	}

	protected override void OnSubModuleLoad()
	{
		base.OnSubModuleLoad();
		Harmony harmony = new Harmony("levelingrebalance");
		harmony.PatchAll(Assembly.GetExecutingAssembly());
	}
}
