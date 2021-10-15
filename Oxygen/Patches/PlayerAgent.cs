using HarmonyLib;
using InControl;
using Oxygen.Components;
using Oxygen.Utils;
using Player;

namespace Oxygen.Patches
{
    [HarmonyPatch(typeof(PlayerAgent), nameof(PlayerAgent.ReceiveModification))]
    class PlayerAgent_ReceiveModification
    {
        public static void Prefix(PlayerAgent __instance, ref EV_ModificationData data)
        {
            if (data.health != 0.0)
            {
                AirManager.Current.RemoveAir(data.health);
            }
            else
            {
                AirManager.Current.AddAir();
            }
            
            // Prevent not implemented error
            data.health = 0.0f;
        }
    }
}