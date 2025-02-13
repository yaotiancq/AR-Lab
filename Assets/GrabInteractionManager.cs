using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabInteractionManager : MonoBehaviour
{
    public XRGrabInteractable parentInteractable; 
    public XRGrabInteractable childInteractable; 

    private void Start()
    {
        parentInteractable.selectEntered.AddListener(OnParentGrabbed);
        parentInteractable.selectExited.AddListener(OnParentReleased);
    }

    private void OnParentGrabbed(SelectEnterEventArgs args)
    {
        
        childInteractable.enabled = false;
    }

    private void OnParentReleased(SelectExitEventArgs args)
    {
        
        childInteractable.enabled = true;
    }
}

