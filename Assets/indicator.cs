using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class indicator : MonoBehaviour
{

    [SerializeField] private Transform indicatorTransform;
    [Range(0, 10)] public float current = 0;
    public TextMeshProUGUI TextMeshProUGUI;

    private float angle = -60;  
    // Start is called before the first frame update
    void Start()
    {
        angle = -60 + current * 120 / 10;
        indicatorTransform.localRotation=Quaternion.Euler(0, angle, 0);
        TextMeshProUGUI.text = current.ToString("F2") + "A";
    }

    // Update is called once per frame
    void Update()
    {
        angle = -60 + current * 120 / 10;
        indicatorTransform.localRotation = Quaternion.Euler(0, angle, 0);
        TextMeshProUGUI.text = current.ToString("F2") + "A";
    }
}
