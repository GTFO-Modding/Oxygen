using System;
using BepInEx;
using BepInEx.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using UnhollowerRuntimeLib;
using Oxygen.Components;
using Oxygen.Utils;

namespace Oxygen
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    [BepInProcess("GTFO.exe")]
    public class Plugin : BasePlugin
    {
        private const string
            MODNAME = "Oxygen",
            AUTHOR = "chasetug",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0";
        
        public static ManualLogSource log;
        
        public override void Load()
        {
            ClassInjector.RegisterTypeInIl2Cpp<AirDamage>();
            RundownManager.add_OnExpeditionGameplayStarted((Action) AirDamage.Setup);
            
            ClassInjector.RegisterTypeInIl2Cpp<AirBar>();
            Globals.Global.add_OnAllManagersSetup((Action) AirBar.Setup);
            
            ClassInjector.RegisterTypeInIl2Cpp<AirPlane>();
            RundownManager.add_OnExpeditionGameplayStarted((Action) AirPlane.Setup);
            
            log = Log;
            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }
    }
}

