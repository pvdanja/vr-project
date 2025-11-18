using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MaterialRopeDetachAction : MonoBehaviour
{
	[SerializeField]
	Vector3 InitialCords;
	[SerializeField]
	Quaternion InitialRotation;
	//Rope Rope;

	bool resetAsked = false;

	XRGrabInteractable grabComponent;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		grabComponent = gameObject.GetComponent<XRGrabInteractable>();
		//InitialCords = transform.localPosition;
		//InitialRotation = gameObject.transform.rotation;
		//Rope = gameObject.GetComponentInChildren<Rope>();
		//InvokeRepeating("DebugSender", 0f, 1f);
	}

	// Update is called once per frame
	void LateUpdate()
	{
		//Debug.Log(grabComponent.interactorsSelecting);
		if (resetAsked)
		{
			Invoke("InvokedReset", 0.5f);
			resetAsked = false;
		}
	}

	void DebugSender()
	{
		Debug.Log("Функция вызвана в: " + Time.time + "\n" + grabComponent.interactorsSelecting.Count);
	}

	void OnDestroy()
	{
		// Остановить вызовы при уничтожении объекта
		CancelInvoke("DebugSender");
	}

	public void reset()
	{
		//transform.localPosition = InitialCords;
		//transform.rotation = InitialRotation;
		//Rope.RecalculateRope();
		// gameObject.GetComponent<XRGrabInteractable>().interactionManager.
		//Invoke("InvokedReset", 0.1f);
		resetAsked = true;
		
	}

	private void InvokedReset() {
		transform.localPosition = InitialCords;
		transform.rotation = InitialRotation;
	}

}
