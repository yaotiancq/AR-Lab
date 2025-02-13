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

        // 初始化文本
        UpdateValueText();

        // 添加监听器，当滑块值改变时更新文本
        minMaxSlider.onValueChanged.AddListener(delegate { UpdateValueText(); });
    }

    void UpdateValueText()
    {
        // 更新 Value Text 显示内容
        float currentValue = minMaxSlider.value; // 获取滑块值
        TextMeshProUGUI.text = currentValue.ToString("F2");
    }

    void OnDestroy()
    {
        // 移除监听器，避免潜在的内存泄漏
        if (minMaxSlider != null)
        {
            minMaxSlider.onValueChanged.RemoveAllListeners();
        }
    }
}

