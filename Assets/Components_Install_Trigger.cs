using UnityEngine;

public class Components_Install_Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Touched: " + other.gameObject.name);
        if (other.gameObject.tag == "SMD_Plate_component")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            //Debug.Log("Activated: " + other.gameObject.name);
        }
    }

}
