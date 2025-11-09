using UnityEngine;

[ExecuteAlways] // Works in edit mode too
public class InspectorTransparency : MonoBehaviour
{
    [Header("Assign the object whose material you want to fade")]
    public Renderer targetRenderer;

    [Range(0f, 1f)]
    public float transparency = 1f; // Slider in Inspector

    void Update()
    {
        if (targetRenderer == null) return;

        // Get the shared material so it updates in scene view
        Material mat = targetRenderer.sharedMaterial;
        if (mat == null) return;

        if (mat.HasProperty("_BaseColor"))
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = transparency;
            mat.SetColor("_BaseColor", c);
        }
        else
        {
            // fallback for older shaders
            Color c = mat.color;
            c.a = transparency;
            mat.color = c;
        }
    }
}
