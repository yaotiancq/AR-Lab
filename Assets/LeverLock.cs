using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LeverLock : MonoBehaviour
{
    public XRGrabInteractable leverInteractable; 
    private Rigidbody leverRigidbody;

    void Start()
    {
        leverRigidbody = GetComponent<Rigidbody>();

       
        leverInteractable.selectEntered.AddListener(OnLeverGrabbed);
        leverInteractable.selectExited.AddListener(OnLeverReleased);
    }

    private void OnLeverGrabbed(SelectEnterEventArgs args)
    {
       
        leverRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    private void OnLeverReleased(SelectExitEventArgs args)
    {
        
        leverRigidbody.constraints = RigidbodyConstraints.None;
        leverRigidbody.constraints = RigidbodyConstraints.FreezePosition; 
    }
}
