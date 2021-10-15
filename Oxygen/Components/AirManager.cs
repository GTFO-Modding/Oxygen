using UnityEngine;
using System;
using Player;
using Oxygen.Utils;


namespace Oxygen.Components
{
    public class AirManager : MonoBehaviour
    {
        public static AirManager Current;
        private PlayerAgent m_playerAgent;
        private HUDGlassShatter m_hudGlass;
        private Dam_PlayerDamageBase Damage;
        
        public float m_air = 1f;
        public float m_airGain = 0.02f;
        public float m_airLoss = 0.02f;
        public float m_damageTick = 0f;
        public float m_damageTime = 2f;
        public float m_damageAmount = 1f;
        public float m_glassShatter = 0f;
        public float m_shatterAmount = 0.05f;
        
        public AirManager(IntPtr value) : base(value) { }

        public static void Setup()
        {
            AirManager.Current =
                    PlayerManager.Current.m_localPlayerAgentInLevel.gameObject.AddComponent<AirManager>();
        }


        void Awake()
        {
            m_playerAgent = PlayerManager.GetLocalPlayerAgent();
            m_hudGlass = m_playerAgent.FPSCamera.GetComponent<HUDGlassShatter>();
            Damage = m_playerAgent.gameObject.GetComponent<Dam_PlayerDamageBase>();
        }

        void Update()
        {
            if (!RundownManager.ExpeditionIsStarted) return;

            // Breathing intensity, Coughing, and Damage Tick
            if (m_air == 1f)
            {
                AirBar.Current.SetVisible(false);
            }
            else
            {
                AirBar.Current.SetVisible(true);
            }   
            
            if (m_air > 0.8f && m_air <= 1.0f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(0);
            }
            else if (m_air > 0.6f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(1);
                PlayerDialogManager.WantToStartDialog(174U, m_playerAgent);
            }
            else if (m_air > 0.4f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(2);
                PlayerDialogManager.WantToStartDialog(174U, m_playerAgent);
            }
            else if (m_air > 0.2)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(3);
                PlayerDialogManager.WantToStartDialog(173U, m_playerAgent);
            }
            else if (m_air < 0.2f)
            {
                m_damageTick += Time.deltaTime;
                PlayerDialogManager.WantToStartDialog(173U, m_playerAgent);
            }

            if (m_damageTick > m_damageTime)
            {
                AirDamage();
            }
        }
        
        public void AddAir()
        {
            float amount = this.m_airGain;
            m_air = Mathf.Clamp01(m_air + amount);
            AirBar.Current.UpdateAirBar(m_air);
        }

        public void RemoveAir(float amount)
        {
            m_air = Mathf.Clamp01(m_air - amount);
            AirBar.Current.UpdateAirBar(m_air);
        }

        public void AirDamage()
        {
            Damage.NoAirDamage(m_damageAmount);
                
            m_glassShatter += m_shatterAmount;
            m_hudGlass.SetGlassShatterProgression(m_glassShatter);
                
            m_damageTick = 0f;
        }
    }
}