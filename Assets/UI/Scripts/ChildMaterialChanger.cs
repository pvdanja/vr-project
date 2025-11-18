using UnityEngine;

public class ChildMaterialChanger : MonoBehaviour
{
    public Material secondMaterial;
    [ContextMenu("Change Children Materials")]
    public void ChangeChildrenMaterials()
    {
        Material parentMaterial = secondMaterial;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] newMaterials = new Material[renderer.sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = parentMaterial;
            }
            renderer.sharedMaterials = newMaterials;
        }
    }
}