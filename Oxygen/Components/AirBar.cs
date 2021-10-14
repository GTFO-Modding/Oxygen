using TMPro;
using System;
using UnityEngine;
using Oxygen.Utils;

namespace Oxygen.Components
{
    public class AirBar : MonoBehaviour
    {
        private float m_airWidth = 300f;
        private float m_barHeightMin = 3f;
        private float m_barHeightMax = 9f;

        private Color m_airLow = new Color(0, 1f, 0);
        private Color m_airHigh = new Color(0.1f, 0.1f, 0.7f);

        public static AirDamage m_AirDamage;
        public float m_air = 1f;
        public TextMeshPro m_airText;
        public RectTransform m_air1;
        public RectTransform m_air2;
        public SpriteRenderer m_airBar1;
        public SpriteRenderer m_airBar2;
        public SpriteRenderer m_airWarning1;
        public SpriteRenderer m_airWarning2;
        
        public AirBar(IntPtr value) : base(value) { }

        public static void Setup()
        {
            AirDamage.m_AirBar = GuiManager.Current.m_playerLayer.m_playerStatus.gameObject.AddComponent<AirBar>();
        }
        
        void Awake()
        {
            m_airText = GuiManager.Current.m_playerLayer.m_playerStatus.m_healthText.gameObject.Instantiate<TextMeshPro>("AirText");
            m_air1 =
                GuiManager.Current.m_playerLayer.m_playerStatus.m_health1.gameObject.transform.parent.gameObject
                    .Instantiate<RectTransform>("AirFill Right");
            m_air2 =
                GuiManager.Current.m_playerLayer.m_playerStatus.m_health2.gameObject.transform.parent.gameObject
                    .Instantiate<RectTransform>("AirFill Left");

            m_airText.transform.Translate(0, -30f, 0);
            m_air1.transform.Translate(0, -30f, 0);
            m_air2.transform.Translate(0, 30f, 0);

            m_airBar1 = m_air1.GetChild(1).GetComponent<SpriteRenderer>();
            m_airBar2 = m_air2.GetChild(1).GetComponent<SpriteRenderer>();
            
            m_airWarning1 = m_air1.GetChild(2).GetComponent<SpriteRenderer>();
            m_airWarning2 = m_air2.GetChild(2).GetComponent<SpriteRenderer>();

            m_airWarning1.enabled = false;
            m_airWarning2.enabled = false;
            
            SetVisible(m_airText, m_air1, m_air2, false);
        }

        void Update()
        {
            if (m_AirDamage == null) return;
            if (this.m_air != m_AirDamage.m_air)
            {
                this.m_air = m_AirDamage.m_air;
                SetVisible(m_airText, m_air1, m_air2, true);
                SetAirText(m_airText, m_air);
                SetAirBar(m_airBar1, m_air);
                SetAirBar(m_airBar2, m_air);
            }
            
            if (m_air * 100 > 99)
            {
                SetVisible(m_airText, m_air1, m_air2, false);
            }
        }
        
        void SetAirBar(SpriteRenderer bar, float val)
        {
            bar.size = new Vector2(val * m_airWidth, Mathf.Lerp(this.m_barHeightMin, this.m_barHeightMax, val));
            bar.color = Color.Lerp(m_airLow, m_airHigh, val);
        }

        void SetAirText(TextMeshPro text, float val)
        {
            text.text = (val * 100f).ToString("N0") + "%";
            text.color = Color.Lerp(this.m_airLow, this.m_airHigh, val);
            text.ForceMeshUpdate(true);
        }

        void SetVisible(TextMeshPro text, RectTransform air1, RectTransform air2, bool vis)
        {
            air1.gameObject.SetActive(vis);
            air2.gameObject.SetActive(vis);
            text.gameObject.SetActive(vis);
        }
    }
}