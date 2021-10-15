using UnityEngine;
using System;
using GameData;
using Player;
using Oxygen.Utils;


namespace Oxygen.Components
{
    public class AirPlane : MonoBehaviour
    {
        public static AirPlane Current;
        private EV_Plane airPlane = new EV_Plane();
        private float airAmount = 1f;

        public AirPlane(IntPtr value) : base(value)
        {
        }

        public void SetAirPlane(FogSettingsDataBlock fogSettings)
        {
            //TODO Implement unregister 
            if (airAmount > 0.0)
            {
                this.airPlane.invert = (double) fogSettings.DensityHeightMaxBoost > (double) fogSettings.FogDensity;
                this.airPlane.contents = eEffectVolumeContents.Health;
                this.airPlane.modification = eEffectVolumeModification.Inflict;
                this.airPlane.modificationScale = airAmount;
                this.airPlane.lowestAltitude = fogSettings.DensityHeightAltitude;
                this.airPlane.highestAltitude = fogSettings.DensityHeightAltitude + fogSettings.DensityHeightRange;
                EffectVolumeManager.RegisterVolume((EffectVolume) this.airPlane);
            }
        }

        public void OnExpeditionStarted()
        {
            ExpeditionInTierData activeExpedition = RundownManager.ActiveExpedition;
            if (activeExpedition != null && activeExpedition.Expedition.FogSettings > 0U)
            {
                SetAirPlane(GameDataBlockBase<FogSettingsDataBlock>.GetBlock(activeExpedition.Expedition.FogSettings));
            }
            else
            {
                SetAirPlane(GameDataBlockBase<FogSettingsDataBlock>.GetBlock(21U));
            }
        }

        public static void Setup()
        {
            if (AirPlane.Current == null)
            {
                AirPlane.Current = LocalPlayerAgentSettings.Current.gameObject.AddComponent<AirPlane>();
            }
            AirPlane.Current.OnExpeditionStarted();
        }

    }
}