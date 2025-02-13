using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class CircuitManager : MonoBehaviour
{
    public XRSocketInteractor batteryPositive;
    public XRSocketInteractor batteryNegative;
    public XRSocketInteractor bulbPositive;
    public XRSocketInteractor bulbNegative;
    public XRSocketInteractor switchPositive;
    public XRSocketInteractor switchNegative;
    public XRSocketInteractor resistorPositive;
    public XRSocketInteractor resistorNegative;
    public XRSocketInteractor ammeterPositive;
    public XRSocketInteractor ammeterNegative;

    public Slider batterySlider;
    public Slider lampSlider;
    public Slider resistorSlider;
    public Button confirmButton;

    public string printText = "";

    public Light bulbLight;

    public XRLever xRLever;
    public XRKnob xRKnob;
    public Volume volume;

    public Material lightOffMaterial;
    public Material lightOnMaterial;
    public Renderer bulbRenderer;

    private Bloom bloom;
    private float originalLightIntensity;
    private float currentLightIntensity;
    private float originalBloomIntensity;
    private float currentBloomIntensity;
    private float originalBloomScatter;
    private float currentBloomScatter;

    private Color originalColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    public indicator indicator;
    public float bulbResistance = 1.0f;
    public float batteryVoltage = 10f;
    public float maxResistor = 9.0f;
    private float current = 0.0f;
    //private float maxCurrent = 0.0f;
    private float maxAmmeterCurrent = 10.0f;
    private Color adjustedColor;
    private float logTimer = 0.0f;
    private float logInterval = 1f;

    void Start()
    {
        bulbLight.enabled = false;
        bulbRenderer.material = lightOffMaterial;
        bulbResistance = lampSlider.value;
        batteryVoltage = batterySlider.value;
        maxResistor = resistorSlider.value;

        //maxCurrent = batteryVoltage / bulbResistance;
        indicator.current = current;

        if (volume == null)
        {
            Debug.LogError("Volume is not assigned!");
            return;
        }

        if (volume.profile == null)
        {
            Debug.LogError("Volume Profile is missing!");
            return;
        }

        if (!volume.profile.TryGet(out bloom))
        {
            Debug.LogError("Bloom is not found in the Volume Profile!");
            return;
        }

        if (bulbLight == null)
        {
            Debug.LogError("BulbLight is null at runtime!");
            return;
        }


        originalLightIntensity = bulbLight.intensity;
        originalBloomIntensity = bloom.intensity.value;
        originalBloomScatter = bloom.scatter.value;

        currentBloomIntensity = originalBloomIntensity;
        currentBloomScatter = originalBloomScatter;
        currentLightIntensity = originalLightIntensity;


        originalColor = lightOnMaterial.GetColor("_EmissionColor");
        adjustedColor = originalColor;
        //lightIntensity = emissionColor.maxColorComponent;

        //Debug.Log("emissionColor: " + originalColor + " lightIntensity: " + lightIntensity + " bloomIntensity: " + bloomIntensity + " bloomScatter: " + bloomScatter);

    }

    void Update()
    {
        //Debug.Log("circuitConnected: " + circuitConnected() + " xRLever.value: " + xRLever.value + " current: " + current);
        if (circuitConnected() && xRLever.value)
        {
            TurnOnBulb();
            culculateCurrent();
            AdjustIntensityOfLight();
            AdjustEmissionIntensity();
        }
        else
        {
            TurnOffBulb();
            current = 0;
        }

        UpdateAmmeter();

        logTimer += Time.deltaTime;
        if (logTimer >= logInterval)
        {
            Debug.Log("emissionColor: " + adjustedColor + " lightIntensity: " + currentLightIntensity + "\n"
                + " bloomIntensity: " + currentBloomIntensity + " bloomScatter: " + currentBloomScatter + "\n"
                + " batteryVoltage: " + batteryVoltage + " bulbResistance: " + bulbResistance + " maxResistor: " + maxResistor + "\n"
                + "current: " + current);
            logTimer = 0.0f;
        }

        //Debug.Log("emissionColor: " + adjustedColor + " lightIntensity: " + currentLightIntensity + "\n"
        //    + " bloomIntensity: " + currentBloomIntensity + " bloomScatter: " + currentBloomScatter + "\n"
        //    + " batteryVoltage: " + batteryVoltage + " bulbResistance: " + bulbResistance + " maxResistor: " + maxResistor + "\n"
        //    + "current: "+ current);

        //printText = "Circuit status: " + circuitConnected() + "\n" + "Switch status: " + xRLever.value + "\n";

    }

    private void UpdateAmmeter()
    {

        if (scaleFactor() > 1)
        {
            current = maxAmmeterCurrent;
        }

        indicator.current = current;
    }

    private void culculateCurrent()
    {
        float x = xRKnob.value;
        current = batteryVoltage / (bulbResistance + x * maxResistor);
    }

    private float scaleFactor()
    {
        //float var = (current - minCurrent) / (maxCurrent - minCurrent);
        float var = current / maxAmmeterCurrent;
        return var;
    }

    private void AdjustIntensityOfLight()
    {
        float var = scaleFactor();
        //Debug.Log("var: " + var);
        //Debug.Log("bulbLight.intensity: " + bulbLight.intensity);
        bulbLight.intensity = var * (originalLightIntensity - 0);
        //Debug.Log("bulbLight.intensity: " + bulbLight.intensity);
        currentLightIntensity = bulbLight.intensity;

        bloom.intensity.value = var * (originalBloomIntensity - 0);
        currentBloomIntensity = bloom.intensity.value;
        bloom.scatter.value = var * (originalBloomScatter - 0);
        currentBloomScatter = bloom.scatter.value;

    }

    private void AdjustEmissionIntensity()
    {
        float var = scaleFactor();
        if (lightOnMaterial == null) return;

        adjustedColor = originalColor * var;

        lightOnMaterial.SetColor("_EmissionColor", adjustedColor);

        lightOnMaterial.EnableKeyword("_EMISSION");
    }

    public void OnBatterSliderValueChanged()
    {
        batteryVoltage = batterySlider.value;
    }

    public void OnLampSliderValueChanged()
    {
        bulbResistance = lampSlider.value;
    }

    public void OnResistorSliderValueChanged()
    {
        maxResistor = resistorSlider.value;
    }

    private void UpdateParameters()
    {
        batteryVoltage = batterySlider.value;
        bulbResistance = lampSlider.value;
        maxResistor = resistorSlider.value;
        //Debug.Log("Confirmed:"+" batteryVoltage: " + batteryVoltage + " bulbResistance: " + bulbResistance + " maxResistor: " + maxResistor);
    }

    public void OnConfirmButtonClicked()
    {
        UpdateParameters();
    }

    private bool circuitConnected()
    {
        bool bool1 = IsSocketConnected(batteryPositive) &&
               IsSocketConnected(batteryNegative) &&
               IsSocketConnected(bulbPositive) &&
               IsSocketConnected(bulbNegative) &&
               IsSocketConnected(switchPositive) &&
               IsSocketConnected(switchNegative) &&
               IsSocketConnected(resistorPositive) &&
               IsSocketConnected(resistorNegative) &&
               IsSocketConnected(ammeterNegative) &&
               IsSocketConnected(ammeterPositive);


        return bool1;
    }


    private bool IsSocketConnected(XRSocketInteractor socket)
    {
        return socket.hasSelection;
    }


    private void TurnOnBulb()
    {
        //Debug.Log("TurnOnBulb");
        bulbLight.enabled = true;
        if (bulbRenderer != null && lightOnMaterial != null)
        {
            bulbRenderer.material = lightOnMaterial;
        }
    }


    private void TurnOffBulb()
    {
        //Debug.Log("TurnOffBulb");
        bulbLight.enabled = false;
        if (bulbRenderer != null && lightOffMaterial != null)
        {
            bulbRenderer.material = lightOffMaterial;
        }
    }

    void OnApplicationQuit()
    {
        // 游戏关闭时还原值
        lightOnMaterial.SetColor("_EmissionColor", originalColor);
    }
}


