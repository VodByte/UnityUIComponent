using System.Text.RegularExpressions;

using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

namespace VodByte.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HyperlinkUI : MonoBehaviour, IPointerClickHandler
    {
        static readonly Regex m_linkRegex = new(@"<link=(.*?)>");

        TextMeshProUGUI m_comp;

        ///---------------------------------------------------------------
        private void Awake()
        {
            m_comp = GetComponent<TextMeshProUGUI>();
        }

        ///---------------------------------------------------------------
        public void SetLink(int _idx, string _url)
        {
            MatchCollection matches = m_linkRegex.Matches(m_comp.text);
            int count = 0;
            m_comp.text = m_linkRegex.Replace(m_comp.text, m =>
            {
                if (count == _idx)
                {
                    return @$"<link=""{_url}"">";
                }
                else
                {
                    count++;
                    return m.Value;
                }
            });
        }

        ///---------------------------------------------------------------
        void IPointerClickHandler.OnPointerClick(PointerEventData _eventData)
        {
            var canvas = m_comp.canvas;
            var camera = canvas.renderMode is RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            var index = TMP_TextUtilities.FindIntersectingLink(m_comp, _eventData.position, camera);

            if (index is -1) return;

            TMP_LinkInfo linkInfo = m_comp.textInfo.linkInfo[index];
            string url = linkInfo.GetLinkID();
            Application.OpenURL(url);
        }
    }
}