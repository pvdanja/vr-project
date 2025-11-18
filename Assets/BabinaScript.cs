using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BabinaScript : MonoBehaviour
{

    [SerializeField]
    MeshRenderer RopeRenderer;

    [SerializeField]
    MeshRenderer WireCapRenderer;

    [SerializeField]
    GameObject EndPos;

    InteractionLayerMask InitInteractionLayers;

    bool SyncingWithSocket = false;

    Transform SocketAttach;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitInteractionLayers = gameObject.GetComponent<XRGrabInteractable>().interactionLayers;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (SyncingWithSocket)
        {
            if(gameObject.transform.position == SocketAttach.position)
            {
                EndPos.GetComponent<XRGrabInteractable>().interactionLayers = InitInteractionLayers;
                EndPos.GetComponent<MaterialRopeDetachAction>().reset();
                EndPos.GetComponent<Collider>().enabled = true;
                SyncingWithSocket = false;
            }
        }
    }

    public void SwithToRope(InteractionLayerMask SocketMask, Transform v_SocketAttach)
    {
        gameObject.GetComponent<XRGrabInteractable>().interactionLayers = SocketMask;

        SocketAttach = v_SocketAttach;
        SyncingWithSocket = true;
        RopeRenderer.enabled = true;
        WireCapRenderer.enabled = true;

    }
}
