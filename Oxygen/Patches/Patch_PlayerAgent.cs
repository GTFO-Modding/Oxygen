using HarmonyLib;
using Oxygen.Components;
using Oxygen.Utils;
using Player;

namespace Oxygen.Patches
{
    [HarmonyPatch(typeof(PlayerAgent), nameof(PlayerAgent.ReceiveModification))]
    class Patch_PlayerAgent
    {
        public static void Prefix(PlayerAgent __instance, ref EV_ModificationData data)
        {
            if (data.health != 0.0)
            {
                AirDamage.Current.RemoveAir(data.health * 0.1f);
            }
            else
            {
                AirDamage.Current.AddAir(0.05f);
            }

            data.health = 0.0f;
        }
    }
}