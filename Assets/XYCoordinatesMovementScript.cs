using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Rendering;

public class XYCoordinatesMovementScript : MonoBehaviour
{

	[SerializeField]
	[Tooltip("XAxis Transform")]
	Transform XAxis;

	[SerializeField]
	[Tooltip("YAxis Transform")]
	Transform YAxis;

	[SerializeField]
	[Tooltip("ZAxis Transform")]
	Transform ZAxis;

	[SerializeField]
	Vector3 CurrentCords;

	[SerializeField]
	float CartXSpeed = 0.1f;

	[SerializeField]
	float CartYSpeed = 0.1f;

	[SerializeField]
	float CartZSpeed = 0.1f;

	[SerializeField]
	int MaterialsToInsert = 0;

	[SerializeField]
	bool TestProgramStart;

    [SerializeField]
    bool DebugStart = false;

    [SerializeField]
	bool Restart = false;

	// Флаг работы, по нему проверяется, выполняется ли сейчас какая-либо программа
	bool WorkInProgress = false;

    [SerializeField]
    List<bool> InsertedMaterials;
	
	[SerializeField]
    List<bool> InsertedReels;

	bool ReadyToWork = false;
	
	bool ReelsReady = false;

	// Номер объекта в CurrentProgram, к которому двигаться
	int CurrentStep = 0;

	//float test = float.Parse("0.11", CultureInfo.InvariantCulture);

	List<Vector3> CurrentProgram;

	// Границы по координатам: (0.435, 0.02, 0.4125)

	[SerializeField]
	GameObject Board;

	void Start()
	{
		//DiagonalMoveDbg = false;
		for (int i = 0; i < MaterialsToInsert; i++)
		{
			InsertedMaterials.Add(false);
			InsertedReels.Add(false);
		}
	} 
	void FixedUpdate()
	{
		CurrentCords = new(XAxis.localPosition.x, YAxis.localPosition.y, ZAxis.localPosition.z);

		/*		if (DiagonalMoveDbg == true)
				{
					XAxis.Translate(CartXSpeed * Time.deltaTime, 0, 0);
					ZAxis.Translate(0, 0, CartZSpeed * Time.deltaTime);
				}*/
		if (DebugStart)
		{
			DebugStart = false;
			StartWork(PalmTreeProg);
		}

		if (TestProgramStart == true)
		{
			TestProgramStart = false;
			//Debug.Log(testProgr);

			if (ReadyToWork)
			{
				StartWork(PalmTreeProg);
			}
			else
			{
				Debug.Log(InsertedMaterials);
			}
		}

		if (Restart == true)
		{
			if (MoveTo(Vector3.zero))
			{
				Restart = false;
			}
		}
		else if (WorkInProgress == true)
		{
			if(CurrentStep <= CurrentProgram.Count - 1) 
			{
				//Debug.Log(1);
				if (MoveTo(CurrentProgram[CurrentStep]))
				{
					//Debug.Log(2 + " " + YAxis.localPosition.y);
					if(YAxis.localPosition.y > 0) {
						CurrentProgram[CurrentStep] = new(CurrentProgram[CurrentStep].x, 0, CurrentProgram[CurrentStep].z);
						//Debug.Log(3);

					}
					else
					{
						//Debug.Log(4);
						CurrentStep++;
					}
					
				}
			}
			else
			{
				WorkInProgress = false;
				Restart = true;
				Board.GetComponent<BoardScript>().ComponentsPlaced();
			}
		}
		

		
	}

	// Функция начала работы, принимает программу для работы
	public void StartWork(List<Vector3> program)
	{
		WorkInProgress = true;
		Restart = true;
		CurrentProgram = new(program);
		CurrentStep = 0;

		//TargetCords = new(CurrentProgram[CurrentStep]);
	}

	public void MaterialInserted(int index)
	{ 
		InsertedMaterials[index] = true;

		foreach (bool mat in InsertedMaterials) {
			if (!mat) {
				return;
			}
		}
		
		if(ReelsReady){
			ReadyToWork = true;
		}
	}
	
	public void ReelInserted(int index)
	{ 
		InsertedReels[index] = true;

		foreach (bool reel in InsertedReels) {
			if (!reel) {
				return;
			}
		}
		ReelsReady = true;
	}

    public void MaterialRemoved(int index)
    {
        InsertedMaterials[index] = false;

        ReadyToWork = false;
    }
	
	public void ReelRemoved(int index)
    {
        InsertedReels[index] = false;

        ReelsReady = false;
    }


    // Возвращает true, когда находится в координате, которую сообщили
    bool MoveTo(Vector3 cords)
	{
		float XMoveDistance = cords.x - XAxis.localPosition.x;
		float YMoveDistance = cords.y - YAxis.localPosition.y;
		float ZMoveDistance = cords.z - ZAxis.localPosition.z;

		if (Mathf.Abs(XMoveDistance) < 0.0001f)
		{
			XMoveDistance = 0;
		}
		if (Mathf.Abs(YMoveDistance) < 0.0001f)
		{
			YMoveDistance = 0;
		}
		if (Mathf.Abs(ZMoveDistance) < 0.0001f)
		{
			ZMoveDistance = 0;
		}

		if (Mathf.Abs(XMoveDistance) > 0 || Mathf.Abs(ZMoveDistance) > 0)
		{
			float XStep = CartXSpeed * Time.deltaTime;
			float XMoveAmount = Mathf.Min(XStep, Mathf.Abs(XMoveDistance));

			float ZStep = CartZSpeed * Time.deltaTime;
			float ZMoveAmount = Mathf.Min(ZStep, Mathf.Abs(ZMoveDistance));

			//Debug.Log("Текущий угол: " + Cover.localEulerAngles.y + "\nНа сколько повернём: " + rotationAmount + "\nШаг расчитанный через скорость: " + step + "\nРазница между целью и текущим: " + AngleDifference + "\nВремя: " + Time.deltaTime);

			// текущий - заданный = + (120 - 0) -> закрытие -> -rotationAmount

			if (XMoveDistance > 0) {
				XAxis.Translate(XMoveAmount, 0, 0);
			}
			else {
				XAxis.Translate(-XMoveAmount, 0, 0);
			}

			if (ZMoveDistance > 0)
			{
				ZAxis.Translate(0, 0, ZMoveAmount);
			}
			else
			{
				ZAxis.Translate(0, 0, -ZMoveAmount);
			}

			return false;
			//Debug.Log("Текущий угол: " + transform.localEulerAngles.z + "\nНа сколько повернём: " + rotationAmount + "\nШаг расчитанный через скорость: " + step + "\nРазница между целью и текущим: " + Mathf.Abs(transform.localEulerAngles.z - target_angle) + "\nВремя: " + delttm);
		}
		else if(Mathf.Abs(YMoveDistance) > 0){
			float YStep = CartYSpeed * Time.deltaTime;
			float YMoveAmount = Mathf.Min(YStep, Mathf.Abs(YMoveDistance));

			if (YMoveDistance > 0)
			{
				YAxis.Translate(0, -YMoveAmount, 0);
			}
			else
			{
				YAxis.Translate(0, YMoveAmount, 0);
			}

			return false;
		}
		else
		{
			XAxis.localPosition = new(cords.x, XAxis.localPosition.y, XAxis.localPosition.z);
			YAxis.localPosition = new(YAxis.localPosition.x, cords.y, YAxis.localPosition.z);
			ZAxis.localPosition = new(ZAxis.localPosition.x, ZAxis.localPosition.y, cords.z);

			return true;
		}
	}

	// Границы по координатам: (0.435, 0.02, 0.4125)
	[SerializeField]
	List<Vector3> testProgr;

    [SerializeField]
    List<Vector3> PalmTreeProg;

	public void StartWorkFromButton()
	{
		if (!WorkInProgress)
		{
			TestProgramStart = true;
		}
	}



}
