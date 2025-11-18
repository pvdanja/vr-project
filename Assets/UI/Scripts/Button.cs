
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;

    private ParentMaterialChanger pMaterialChanger;
    private ChildMaterialChanger cMaterialChanger;

    private void Start()
    {
        if (parent != null)
            pMaterialChanger = parent.GetComponent<ParentMaterialChanger>();

        if (child != null)
            cMaterialChanger = child.GetComponent<ChildMaterialChanger>();
    }

    [ContextMenu("Change Materials")]
    public void ChildrenMaterials()
    {
        if (pMaterialChanger != null)
            pMaterialChanger.ChangeChildrenMaterials();
        else
            Debug.LogError("ParentMaterialChanger is null!");

        if (cMaterialChanger != null)
            cMaterialChanger.ChangeChildrenMaterials();
        else
            Debug.LogError("ChildMaterialChanger is null!");
    }
}

