using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRSocketInteractor))]
public class FilteredSocketReel : MonoBehaviour
{
	//[SerializeField] private string allowedTag = "SocketCompatible";

	[SerializeField] private XYCoordinatesMovementScript MachineController;

	private int SocketTagNumber;

	private XRSocketInteractor socketInteractor;
	

	private void Awake()
	{
		socketInteractor = GetComponent<XRSocketInteractor>();
		socketInteractor.selectEntered.AddListener(OnObjectPlaced);
		socketInteractor.selectExited.AddListener(OnObjectRemoved);


		SocketTagNumber = int.Parse(this.gameObject.tag.Split("Material")[1]) - 1;
		


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
			MachineController.ReelRemoved(SocketTagNumber);
		}
		else {
			//gameObject.
			
			MachineController.ReelInserted(SocketTagNumber);
			args.interactableObject.transform.GetComponent<BabinaScript>().SwithToRope(socketInteractor.interactionLayers, socketInteractor.attachTransform);



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

		MachineController.ReelRemoved(SocketTagNumber);
	}
}