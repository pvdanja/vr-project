using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MaterialSocketChecker : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void test(XRSocketInteractor interactor) {
		Debug.Log(interactor.interactablesHovered[0].transform.gameObject.name + " " + (interactor.interactablesHovered[0].transform.gameObject.name == "1").ToString());
		if (
		interactor.interactablesHovered[0].transform.gameObject.name == "1")
		{
			interactor.socketActive = true;
		}
		else {
			interactor.interactionManager.SelectExit(interactor, interactor.interactablesSelected[0]);
			interactor.socketActive = false;
		}
	}
}
