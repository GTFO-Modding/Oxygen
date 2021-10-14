using UnityEngine;
using System;
using Player;
using Oxygen.Utils;


namespace Oxygen.Components
{
    public class AirDamage : MonoBehaviour
    {
        static PlayerAgent m_playerAgent;
        public HUDGlassShatter m_hudGlass;
        public float m_glassShatter;
        public float m_damageTime;
        Dam_PlayerDamageBase Damage;
        public static AirBar m_AirBar;
        public float m_air = 1f;
        
        public AirDamage(IntPtr value) : base(value) { }
        
        public static void Setup()
        {
            AirBar.m_AirDamage = PlayerManager.Current.m_localPlayerAgentInLevel.gameObject.AddComponent<AirDamage>();
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

            if (m_air > 0 && m_playerAgent.m_movingCuller.CurrentNode.CourseNode.NodeID % 2 > 0)
            {
                m_air -= Time.deltaTime * 0.05f;
            }
            else if (m_air < 1)
            {
                m_air += Time.deltaTime * 0.05f;
            }

            if (m_air > 0.8f && m_air <= 1.0f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(0);
            }
            else if (m_air > 0.6f && m_air <= 0.8f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(1);
                PlayerDialogManager.WantToStartDialog(174U, m_playerAgent);
            }
            else if (m_air > 0.4f && m_air <= 0.6f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(2);
                PlayerDialogManager.WantToStartDialog(174U, m_playerAgent);
            }
            else if (m_air > 0.2 && m_air <= 0.4f)
            {
                m_playerAgent.Breathing.TryChangeBreathingIntensity(3);
                PlayerDialogManager.WantToStartDialog(173U, m_playerAgent);
            }
            else if (m_air < 0.2f)
            {
                m_damageTime += Time.deltaTime;
                PlayerDialogManager.WantToStartDialog(173U, m_playerAgent);
            }

            if (m_damageTime > 2f)
            {
                Damage.NoAirDamage(1f);
                
                m_glassShatter += 0.05f;
                m_hudGlass.SetGlassShatterProgression(m_glassShatter);
                
                m_damageTime = 0f;
            }
        }
    }
}