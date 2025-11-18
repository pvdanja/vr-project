using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class FilteredSocket : MonoBehaviour
{
	//[SerializeField] private string allowedTag = "SocketCompatible";

	[SerializeField] private Transform SocketVisual;

    [SerializeField] private MeshRenderer SocketToHolderRope;

    [SerializeField] private XYCoordinatesMovementScript MachineController;

    [SerializeField] private float SocketVisualRotationAngle = 5f;

    private Vector3 InitialRotation;

	private int SocketTagNumber;

    private XRSocketInteractor socketInteractor;


	private void Awake()
	{
		socketInteractor = GetComponent<XRSocketInteractor>();
		socketInteractor.selectEntered.AddListener(OnObjectPlaced);
		socketInteractor.selectExited.AddListener(OnObjectRemoved);
        SocketToHolderRope.enabled = false;

		SocketTagNumber = int.Parse(this.gameObject.tag.Split("Material")[1]) - 1;
		

		InitialRotation = SocketVisual.localEulerAngles;
    }

	private void OnDestroy()
	{
		socketInteractor.selectEntered.RemoveListener(OnObjectPlaced);
		socketInteractor.selectExited.RemoveListener(OnObjectRemoved);
	}

	private void OnObjectPlaced(SelectEnterEventArgs args)
	{
		// Если у объекта не тот тег, отменяем его размещение
		if (!args.interactableObject.transform.CompareTag(this.gameObject.tag))
		{
			//Debug.Log(socketInteractor.name + " " + args.interactableObject.transform.gameObject.name);
			socketInteractor.interactionManager.SelectExit(socketInteractor, args.interactableObject);
			MachineController.MaterialRemoved(SocketTagNumber);
		}
		else {
			//gameObject.
			//SocketVisual.Rotate(SocketVisualRotationAngle, 0, 0, Space.Self);
			SocketVisual.localRotation = Quaternion.Euler(InitialRotation.x - SocketVisualRotationAngle, InitialRotation.y, InitialRotation.z);
            SocketToHolderRope.enabled = true;

            MachineController.MaterialInserted(SocketTagNumber);


        }
	}

	private void OnObjectRemoved(SelectExitEventArgs args)
	{
        // Можно добавить логику при извлечении объекта

        /*if (!args.interactableObject.isSelected)
		{
			args.interactableObject.transform.gameObject.GetComponent<MaterialRopeDetachAction>().ResetCheck();
		}*/
        //args.interactableObject.transform.gameObject.GetComponent<MaterialRopeDetachAction>().reset();
        SocketVisual.localRotation = Quaternion.Euler(InitialRotation.x, InitialRotation.y, InitialRotation.z);
        MachineController.MaterialRemoved(SocketTagNumber);
        SocketToHolderRope.enabled = false;
    }
}