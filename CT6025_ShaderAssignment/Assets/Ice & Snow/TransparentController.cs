using UnityEngine;
using UnityEngine.UI;

public class TransparencyController : MonoBehaviour
{
    [Header("Assign your target object here")]
    public Renderer targetRenderer;

    private Material materialInstance;
    private Color originalColor;
    private Slider slider;

    void Start()
    {
        // Make sure we have a target
        if (targetRenderer == null)
        {
            Debug.LogError("No renderer assigned!");
            return;
        }

        // Duplicate material so we don’t affect others using the same one
        materialInstance = targetRenderer.material;
        originalColor = materialInstance.color;

        // Create a Canvas
        GameObject canvasGO = new GameObject("TransparencyCanvas");
        canvasGO.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create a Slider under it
        GameObject sliderGO = new GameObject("TransparencySlider");
        sliderGO.transform.SetParent(canvasGO.transform);

        slider = sliderGO.AddComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = originalColor.a;

        // Add background + fill visuals
        GameObject background = new GameObject("Background");
        background.transform.SetParent(sliderGO.transform, false);
        Image bgImage = background.AddComponent<Image>();
        bgImage.color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
        slider.targetGraphic = bgImage;

        GameObject fillArea = new GameObject("Fill");
        fillArea.transform.SetParent(sliderGO.transform, false);
        Image fillImage = fillArea.AddComponent<Image>();
        fillImage.color = new Color(0.3f, 0.7f, 1f, 1f);
        slider.fillRect = fillImage.rectTransform;

        // Add OnValueChanged listener
        slider.onValueChanged.AddListener(SetTransparency);

        // Position on screen
        RectTransform rt = sliderGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.5f, 0f);
        rt.anchorMax = new Vector2(0.5f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.anchoredPosition = new Vector2(0f, 50f);
        rt.sizeDelta = new Vector2(200f, 20f);
    }

    void SetTransparency(float value)
    {
        Color c = materialInstance.color;
        c.a = Mathf.Clamp01(value);
        materialInstance.color = c;
    }
}
