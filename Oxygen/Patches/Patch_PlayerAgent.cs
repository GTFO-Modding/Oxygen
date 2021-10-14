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
                AirDamage.RemoveAir(data.health * 0.1f);
            }
            else
            {
                AirDamage.AddAir(0.05f);
            }

            data.health = 0.0f;
        }
    }
}

//private static int targetUpdateIndex;
//public static void Postfix()
//{
//    Log.Warning("1");
//    int count = EffectVolumeManager.targets.Count;
//    Log.Warning("2");
//    if (count == 0) return;
//    Log.Warning("3");
//    if (targetUpdateIndex >= count) targetUpdateIndex = 0;
//    Log.Warning("4");
//    IEffectVolumeTarget target = EffectVolumeManager.targets[targetUpdateIndex];
//    Log.Warning("5");
//    EV_TargetData volumeTargetData = target.EffectVolumeTargetData;
//    Log.Warning("6");
//    if ((double) volumeTargetData.lastModificationTime < (double) Clock.Time - 0.5)
//    {
//        Log.Warning("7");
//        EV_ModificationData modificationData = new EV_ModificationData();
//        float delta = Clock.Time - volumeTargetData.lastModificationTime;
//        Log.Warning("8");
//        for (int index = 0; index < EffectVolumeManager.volumes.Count; ++index)
//        { 
//            EffectVolumeManager.volumes[index].Update(ref volumeTargetData, ref modificationData, delta);
//            Log.Warning("10");
//            if (EffectVolumeManager.volumes[index].contents == (eEffectVolumeContents) 4)
//            {
//                Log.Warning("11");
//                Log.Warning("Applying air damage!");
//            }
//        }
//    }
//    Log.Warning("12");
//    ++targetUpdateIndex;
//}