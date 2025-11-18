using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BoardScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int mask;
    void Start()
    {
        mask = InteractionLayerMask.GetMask("Default", "Board");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComponentsPlaced()
    {
        gameObject.GetComponent<XRGrabInteractable>().interactionLayers = mask;
    }
}
