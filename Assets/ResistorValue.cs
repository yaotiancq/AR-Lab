using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResisitorValue : MonoBehaviour
{
    public Slider minMaxSlider;
    public TextMeshProUGUI TextMeshProUGUI;

    void Start()
    {
        if (minMaxSlider == null || TextMeshProUGUI == null)
        {
            Debug.LogError("Slider or Value Text is not assigned.");
            return;
        }

        // ��ʼ���ı�
        UpdateValueText();

        // ��Ӽ�������������ֵ�ı�ʱ�����ı�
        minMaxSlider.onValueChanged.AddListener(delegate { UpdateValueText(); });
    }

    void UpdateValueText()
    {
        // ���� Value Text ��ʾ����
        float currentValue = minMaxSlider.value; // ��ȡ����ֵ
        TextMeshProUGUI.text = currentValue.ToString("F2");
    }

    void OnDestroy()
    {
        // �Ƴ�������������Ǳ�ڵ��ڴ�й©
        if (minMaxSlider != null)
        {
            minMaxSlider.onValueChanged.RemoveAllListeners();
        }
    }
}

