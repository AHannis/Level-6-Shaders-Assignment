using UnityEngine;

[ExecuteAlways]
public class CameraOverlayFollow : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        Transform cam = Camera.main.transform;
        transform.position = cam.position + cam.forward * 0.5f;
        transform.rotation = cam.rotation;
        transform.localScale = new Vector3(10, 10, 1);
    }
}
