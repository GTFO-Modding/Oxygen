using TMPro;
using System;
using UnityEngine;
using Oxygen.Utils;
using Player;

namespace Oxygen.Components
{
    public class AirBar : MonoBehaviour
    {
        public static AirDamage m_AirDamage;
        public float m_air = 1f;
        public TextMeshPro m_airText;
        public RectTransform m_air1;
        public RectTransform m_air2;
        public SpriteRenderer m_airBar1;
        public SpriteRenderer m_airBar2;
        
        private float m_airWidth = 300f;
        private float m_barHeightMin = 3f;
        private float m_barHeightMax = 9f;

        private Color m_airLow = new Color(0, 1f, 0);
        private Color m_airHigh = new Color(0.1f, 0.1f, 0.7f);

        public AirBar(IntPtr value) : base(value) { }

        public static void Setup()
        {
            // Add AirBar script to playerStatus
            GuiManager.Current.m_playerLayer.m_playerStatus.gameObject.AddComponent<AirBar>();
        }
        
        void Awake()
        {
            // Instantiate air bar and text
            m_airText = GuiManager.Current.m_playerLayer.m_playerStatus.m_healthText.gameObject.Instantiate<TextMeshPro>("AirText");
            m_air1 =
                GuiManager.Current.m_playerLayer.m_playerStatus.m_health1.gameObject.transform.parent.gameObject
                    .Instantiate<RectTransform>("AirFill Right");
            m_air2 =
                GuiManager.Current.m_playerLayer.m_playerStatus.m_health2.gameObject.transform.parent.gameObject
                    .Instantiate<RectTransform>("AirFill Left");
            
            // Move air bar down
            m_airText.transform.Translate(0, -30f, 0);
            m_air1.transform.Translate(0, -30f, 0);
            m_air2.transform.Translate(0, 30f, 0);
            
            // Get left & right air bars
            m_airBar1 = m_air1.GetChild(1).GetComponent<SpriteRenderer>();
            m_airBar2 = m_air2.GetChild(1).GetComponent<SpriteRenderer>();
            
            // Remove yellow damage bars
            m_air1.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            m_air2.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            
            // Hide air bar
            SetVisible(m_airText, m_air1, m_air2, false);
        }

        void Update()
        {
            if (m_AirDamage == null) return;
            // If air value is not synced
            if (this.m_air != m_AirDamage.m_air)
            {
                // Sync air value
                this.m_air = m_AirDamage.m_air;
                
                // Make bar visible
                SetVisible(m_airText, m_air1, m_air2, true);
                
                // Update bar values
                SetAirText(m_airText, m_air);
                SetAirBar(m_airBar1, m_air);
                SetAirBar(m_airBar2, m_air);
            }
            
            // If air bar is full, hide it
            if (m_air * 100 > 99)
            {
                SetVisible(m_airText, m_air1, m_air2, false);
            }
        }
        
        // Set bar length and color
        private void SetAirBar(SpriteRenderer bar, float val)
        {
            bar.size = new Vector2(val * m_airWidth, Mathf.Lerp(this.m_barHeightMin, this.m_barHeightMax, val));
            bar.color = Color.Lerp(m_airLow, m_airHigh, val);
        }

        // Set air text and color
        private void SetAirText(TextMeshPro text, float val)
        {
            text.text = (val * 100f).ToString("N0") + "%";
            text.color = Color.Lerp(this.m_airLow, this.m_airHigh, val);
            text.ForceMeshUpdate(true);
        }
        
        // Set visibility of air bar
        private void SetVisible(TextMeshPro text, RectTransform air1, RectTransform air2, bool vis)
        {
            air1.gameObject.SetActive(vis);
            air2.gameObject.SetActive(vis);
            text.gameObject.SetActive(vis);
        }
    }
}