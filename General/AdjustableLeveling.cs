using HarmonyLib;
using MCM.Abstractions.Base.Global;
using System;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace AdjustableLeveling;

public class AdjustableLeveling : MBSubModuleBase
{
	public static MCMSettings Settings { get; private set; }

	private bool _isInitialized = false;

	public override void OnBeforeInitialModuleScreenSetAsRoot()
	{
		base.OnBeforeInitialModuleScreenSetAsRoot();
		if (_isInitialized)
			return;

		Settings = GlobalSettings<MCMSettings>.Instance;
		if (Settings == null)
			throw new Exception("Settings is null");

		InformationManager.DisplayMessage(new InformationMessage("Adjustable Leveling initialized"));

		_isInitialized = true;
	}

	public override void OnSubModuleLoad()
	{
		base.OnSubModuleLoad();
		var harmony = new Harmony("sy.adjustableleveling");
		harmony.PatchAll(Assembly.GetExecutingAssembly());
	}
}
