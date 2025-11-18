using System.Collections.Generic;
using UnityEngine;

public class MovingBelt : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created

	[SerializeField]
	Transform AxisTransform;

	float MonitoredAxisCord;

	[SerializeField]
	bool reversed = false;

	enum UsedAxis
	{
		X,
		Y,
		Z,
	}

	[SerializeField]
	[Tooltip("Determines which axis will be monitored.")]
	UsedAxis m_UsedAxis = UsedAxis.Z;
	UsedAxis usedAxis
	{
		get => m_UsedAxis;
		set => m_UsedAxis = value;
	}

	[SerializeField]
	Transform MovingPartTransform;

	[SerializeField]
	Transform StaticPartTransform;

	[SerializeField]
	Transform RadiusTransform;

	[SerializeField]
	List<Transform> MovingPartSegments;

	[SerializeField]
	List<Transform> StaticPartSegments;

	[SerializeField]
	float SegmentSize;

	Transform CurrentMoveExtra;
	Transform CurrentMoveLastPart;

	float CurrentExtraStartCord;

	[SerializeField]
	float MoveDelta;

	GameObject extraSection;




	void Start()
	{
		//gameObject.transform.SetParent(transform);

		// Отличие в нахождении дополнительной секции: reversed == false - секция в moving части и наоборот
		switch (reversed) {
			case false:
				extraSection = Instantiate(MovingPartSegments[MovingPartSegments.Count - 1].gameObject, MovingPartTransform);

				MovingPartSegments.Add(extraSection.transform);

				CurrentMoveExtra = MovingPartSegments[MovingPartSegments.Count - 1];
				CurrentMoveLastPart = StaticPartSegments[StaticPartSegments.Count - 1];
				break;

			case true:
				extraSection = Instantiate(StaticPartSegments[StaticPartSegments.Count - 1].gameObject, StaticPartTransform);

				StaticPartSegments.Add(extraSection.transform);

				CurrentMoveExtra = StaticPartSegments[StaticPartSegments.Count - 1];
				CurrentMoveLastPart = MovingPartSegments[MovingPartSegments.Count - 1];
				break;
		}

		CurrentExtraStartCord = CurrentMoveExtra.localPosition.z;
		//UpperPartSegments[0].transform.SetParent(UpperPartTransform);
		//Debug.Log(usedAxis + " " + (usedAxis == UsedAxis.X).ToString() + " " + (usedAxis == UsedAxis.Z).ToString() + " ");
	}



	// Update is called once per frame
	void FixedUpdate()
	{

		switch (usedAxis)
		{
			case UsedAxis.X:
				MonitoredAxisCord = AxisTransform.localPosition.x;
				break;
			case UsedAxis.Y:
				MonitoredAxisCord = AxisTransform.localPosition.y;
				break;
			 case UsedAxis.Z:
				MonitoredAxisCord = AxisTransform.localPosition.z;
				break;
		}

		//Debug.Log(MoveDelta);

		FollowAxis(reversed);

		

		if (MoveDelta >= 1)
		{
			MoveForward(reversed);

		}
		else if (MoveDelta < 0)
		{
			MoveBackward(reversed);
		}
	}


	void MoveForward(bool reversed)
	{
		if (!reversed)
		{
			//Debug.Log("1" + CurrentMoveUpper.name + " " + CurrentMoveLower.name + " " + CurrentMoveUpper.localPosition.z + " " + CurrentMoveUpperStartCord);
			CurrentMoveLastPart.SetParent(MovingPartTransform);
			CurrentMoveLastPart.localPosition = CurrentMoveExtra.localPosition;

			MovingPartSegments.Add(CurrentMoveLastPart);
            CurrentMoveLastPart.rotation = Quaternion.Euler(CurrentMoveLastPart.rotation.eulerAngles.x, CurrentMoveLastPart.rotation.eulerAngles.y + 180f, CurrentMoveLastPart.rotation.eulerAngles.z);

            StaticPartSegments.RemoveAt(StaticPartSegments.Count - 1);

			CurrentMoveExtra = CurrentMoveLastPart;
			CurrentExtraStartCord = CurrentMoveExtra.localPosition.z;

			//Debug.Log("2" + CurrentMoveUpper.name + " " + CurrentMoveLower.name + " " + CurrentMoveUpper.localPosition.z + " " + CurrentMoveUpperStartCord);

			CurrentMoveLastPart = StaticPartSegments[StaticPartSegments.Count - 1];
		}
		else {

            CurrentMoveLastPart.SetParent(StaticPartTransform);
            CurrentMoveLastPart.localPosition = CurrentMoveExtra.localPosition;

			StaticPartSegments.Add(CurrentMoveLastPart);

            CurrentMoveLastPart.rotation = Quaternion.Euler(CurrentMoveLastPart.rotation.eulerAngles.x, CurrentMoveLastPart.rotation.eulerAngles.y + 180f, CurrentMoveLastPart.rotation.eulerAngles.z);

            MovingPartSegments.RemoveAt(MovingPartSegments.Count - 1);

            CurrentMoveExtra = CurrentMoveLastPart;
            CurrentExtraStartCord = CurrentMoveExtra.localPosition.z;

            CurrentMoveLastPart = MovingPartSegments[MovingPartSegments.Count - 1];

        }
	}

	void MoveBackward(bool reversed) {

		if (!reversed)
		{

			//Debug.Log("1" + CurrentMoveUpper.name + " " + CurrentMoveLower.name + " " + CurrentMoveUpper.localPosition.z + " " + CurrentMoveUpperStartCord);
			CurrentMoveExtra.SetParent(StaticPartTransform);
			CurrentMoveExtra.localPosition = CurrentMoveLastPart.localPosition;

			StaticPartSegments.Add(CurrentMoveExtra);

            CurrentMoveExtra.rotation = Quaternion.Euler(CurrentMoveExtra.rotation.eulerAngles.x, CurrentMoveExtra.rotation.eulerAngles.y + 180f, CurrentMoveExtra.rotation.eulerAngles.z);

            MovingPartSegments.RemoveAt(MovingPartSegments.Count - 1);

			CurrentMoveLastPart = CurrentMoveExtra;
			CurrentMoveExtra = MovingPartSegments[MovingPartSegments.Count - 1];

			CurrentExtraStartCord = MovingPartSegments[MovingPartSegments.Count - 2].localPosition.z;
			//Debug.Log("2" + CurrentMoveUpper.name + " " + CurrentMoveLower.name + " " + CurrentMoveUpper.localPosition.z + " " + CurrentMoveUpperStartCord);
		}
		else
		{
            CurrentMoveExtra.SetParent(MovingPartTransform);
            CurrentMoveExtra.localPosition = CurrentMoveLastPart.localPosition;

			MovingPartSegments.Add(CurrentMoveExtra);
            CurrentMoveExtra.rotation = Quaternion.Euler(CurrentMoveExtra.rotation.eulerAngles.x, CurrentMoveExtra.rotation.eulerAngles.y + 180f, CurrentMoveExtra.rotation.eulerAngles.z);
            StaticPartSegments.RemoveAt(StaticPartSegments.Count - 1);

			CurrentMoveLastPart = CurrentMoveExtra;
        
            CurrentMoveExtra = StaticPartSegments[StaticPartSegments.Count - 1];

            CurrentExtraStartCord = StaticPartSegments[StaticPartSegments.Count - 2].localPosition.z;
        }
	}

	void FollowAxis(bool reversed)
	{
		switch (reversed) {
			case false:
				CurrentMoveExtra.localPosition = new(0, 0, MonitoredAxisCord / 2);      // z		= 0.5 z
				RadiusTransform.localPosition = new(0, 0, MonitoredAxisCord / 2);       // z		= 0.5 z
				MovingPartTransform.localPosition = new(0, 0, MonitoredAxisCord / 2);   // z		= 0.5 z

                MoveDelta = (CurrentMoveExtra.localPosition.z - CurrentExtraStartCord) / SegmentSize;

                break;

			case true:
				CurrentMoveExtra.localPosition = new(0, 0, -MonitoredAxisCord / 2);		// -z		= 0.5 z
				RadiusTransform.localPosition = new(0, 0, -MonitoredAxisCord / 2);		// -z		= 0.5 z
				MovingPartTransform.localPosition = new(0, 0, -MonitoredAxisCord);      // -z		= z

                MoveDelta = - (CurrentMoveExtra.localPosition.z - CurrentExtraStartCord) / SegmentSize;

                break;
		}

		CurrentMoveLastPart.localPosition = new(0, 0, MonitoredAxisCord / 2);   // +z		= 0.5 z
	}
	
}
