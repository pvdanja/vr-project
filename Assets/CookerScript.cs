using UnityEngine;

public class CookerScript : MonoBehaviour
{
	[SerializeField]
	Transform OvenDoor;

	[SerializeField]
	float MoveSpeed;

	Vector3 OvenDoorInitialPos;

	Vector3 OvenDoorTargetPos;
	
	bool DoorMoving = false;
	
	bool forward = true;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		OvenDoorInitialPos = OvenDoor.localPosition;
		OvenDoorTargetPos = new(OvenDoorInitialPos.x, OvenDoorInitialPos.y, OvenDoorInitialPos.z - 0.21f);
	}

	// Update is called once per frame
	void Update()
	{
		if (DoorMoving)
		{
			
			float MoveDistance;

			if (forward) { 
				MoveDistance = OvenDoorTargetPos.z - OvenDoor.localPosition.z; 
			}
			else {
				MoveDistance = OvenDoorInitialPos.z - OvenDoor.localPosition.z;
			}
			

			float MoveStep = MoveSpeed * Time.deltaTime;
			float MoveAmount = Mathf.Min(MoveStep, Mathf.Abs(MoveDistance));

			//Debug.Log(MoveAmount);

			if (forward) {
				//OvenDoor.transform.Translate(0, 0, -MoveAmount);
				OvenDoor.transform.localPosition = new(OvenDoor.transform.localPosition.x, OvenDoor.transform.localPosition.y, OvenDoor.transform.localPosition.z - MoveAmount);

				if (MoveAmount != MoveStep)
				{
					OvenDoor.transform.localPosition = OvenDoorTargetPos;
					DoorMoving = false;
				}
			}
			else
			{
				//OvenDoor.transform.Translate(0, 0, MoveAmount);
				OvenDoor.transform.localPosition = new(OvenDoor.transform.localPosition.x, OvenDoor.transform.localPosition.y, OvenDoor.transform.localPosition.z + MoveAmount);
				if (MoveAmount != MoveStep)
				{
					OvenDoor.transform.localPosition = OvenDoorInitialPos;
					DoorMoving = false;
				}
			}


		}
	}

	public void DoorMove()
	{
		DoorMoving = true;
		if (OvenDoor.localPosition == OvenDoorInitialPos)
		{
			forward = true;
		}
		else { forward = false; }
	}
}
