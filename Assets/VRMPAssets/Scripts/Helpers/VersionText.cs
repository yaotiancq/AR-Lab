using TMPro;
using UnityEngine;

namespace XRMultiplayer
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField] TMP_Text[] m_VersionTextComponents;
        [SerializeField] string m_Prefix = "v";
        [SerializeField] string m_Suffix = "";

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnValidate()
        {
            
        }

    }
}
