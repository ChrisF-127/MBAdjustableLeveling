using HarmonyLib;
using MCM.Abstractions.Base.Global;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace Leveling_Rebalance;

public class Leveling_Rebalance_SubModule : MBSubModuleBase
{
	public static MCMSettings Settings { get; private set; }

	private bool isInitialized = false;

	public override void OnBeforeInitialModuleScreenSetAsRoot()
	{
		base.OnBeforeInitialModuleScreenSetAsRoot();
		if (isInitialized)
			return;

		InformationManager.DisplayMessage(new InformationMessage("Leveling Rebalance REWORK"));

		Settings = GlobalSettings<MCMSettings>.Instance;
		if (Settings == null)
			throw new Exception("Settings is null");

		isInitialized = true;
	}

	public override void OnSubModuleLoad()
	{
		base.OnSubModuleLoad();
		Harmony harmony = new Harmony("levelingrebalance");
		harmony.PatchAll(Assembly.GetExecutingAssembly());
	}
}
