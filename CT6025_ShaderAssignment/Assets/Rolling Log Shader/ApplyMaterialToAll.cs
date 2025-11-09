using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ApplyMaterialToAll : MonoBehaviour
{
    public Material newMaterial;

    [ContextMenu("Apply Material To All (Scene)")]
    public void ApplyNow()
    {
        if (newMaterial == null)
        {
            Debug.LogWarning("No material assigned!");
            return;
        }

        // Find all renderers in the entire open scene
        Renderer[] renderers = FindObjectsOfType<Renderer>(true);
        int count = 0;

        foreach (Renderer rend in renderers)
        {
            if (rend.sharedMaterial != newMaterial)
            {
                Undo.RecordObject(rend, "Apply Material");
                rend.sharedMaterial = newMaterial;
                count++;
            }
        }

        Debug.Log($"✅ Applied '{newMaterial.name}' to {count} renderers in scene (Editor mode).");
    }
}
