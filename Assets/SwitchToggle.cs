using Unity.Netcode;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public Transform lever; 
    public Vector3 onPosition;  
    public Vector3 offPosition;
    public NetworkVariable<bool> isOn = new(false, 
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);
    public float rotationSpeed = 5f; 

    private Quaternion targetRotation; 

    private void Start()
    {
        UpdateSwitchPosition(true); 
    }

    public void ToggleSwitch()
    {
        isOn.Value = !isOn.Value; 
        UpdateSwitchPosition(false);
    }

    private void UpdateSwitchPosition(bool instant)
    {
        
        targetRotation = Quaternion.Euler(isOn.Value ? onPosition : offPosition);

        if (instant)
        {
            
            lever.localRotation = targetRotation;
        }
    }

    private void Update()
    {
        
        lever.localRotation = Quaternion.Lerp(lever.localRotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}