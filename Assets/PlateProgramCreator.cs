using System.Collections.Generic;
using UnityEngine;

public class PlateProgramCreator : MonoBehaviour
{
	[SerializeField]
	List<Vector3> Program;

	[SerializeField]
	List<GameObject> ComponentsToInstallParents;

	[SerializeField]
	List<Vector3> ComponentsHoldersCords;

	[SerializeField]
	Transform StartCord;

	[SerializeField]
	bool start;


	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (start) {
			for (int i = 0; i < ComponentsToInstallParents.Count; i++) {
				foreach (var ChildTransform in ComponentsToInstallParents[i].GetComponentsInChildren<Renderer>())
				{
					if (ChildTransform.gameObject == ComponentsToInstallParents[i]) continue;
					Vector3 RelativeCords = ChildTransform.bounds.center - StartCord.position;
                    //Debug.Log(StartCord.position.ToString() + "_-_" + ChildTransform.position.ToString() + ChildTransform.gameObject.name);
                    //break;

                    RelativeCords = new(Mathf.Abs(RelativeCords.x), Mathf.Abs(RelativeCords.y), Mathf.Abs(RelativeCords.z));
					
                    Program.Add(ComponentsHoldersCords[i]);
                    Program.Add(RelativeCords);
                }
			}

            start = false;


        }
	}
}
