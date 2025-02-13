using UnityEngine;
using UnityEngine.UI;

namespace XRMultiplayer
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleManager : MonoBehaviour
    {
        [SerializeField] GameObject DebugInfoObject;
        [SerializeField] GameObject Toggle;
        [SerializeField] bool m_StartShowing = false;
        Toggle m_Toggle;

        void Awake()
        {
            if (!TryGetComponent(out m_Toggle) || DebugInfoObject == null)
            {
                Utils.Log($"{gameObject.name} Missing Setup Requirements! Disabling Now.", 2);
                gameObject.SetActive(false);
                enabled = false;
                return;
            }
            m_Toggle.onValueChanged.AddListener(OnToggle);
            ResetTooltip();
        }

        void OnDestroy()
        {
            m_Toggle.onValueChanged.RemoveListener(OnToggle);
        }

        void OnToggle(bool toggle)
        {
            if (toggle)
            {
                ShowDebugInfo();
            }
            else
            {
                HideDebugInfo();
            }
        }

        public void ShowDebugInfo()
        {
            DebugInfoObject.SetActive(true);
            Toggle.SetActive(true);
        }

        public void HideDebugInfo()
        {
            if (m_Toggle.isOn) return;
            DebugInfoObject.SetActive(false);
            Toggle.SetActive(true);
        }

        public void ResetTooltip()
        {
            m_Toggle.SetIsOnWithoutNotify(false);
            HideDebugInfo();
            if (m_StartShowing)
            {
                m_Toggle.isOn = true;
            }
        }
    }
}

