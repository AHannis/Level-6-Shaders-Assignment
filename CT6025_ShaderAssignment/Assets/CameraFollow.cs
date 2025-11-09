using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Camera Offsets")]
    [Tooltip("Horizontal distance behind the player")]
    public float distance = 8f;
    [Tooltip("Height above the player")]
    public float height = 6f;
    [Tooltip("Tilt angle downward (degrees)")]
    [Range(0, 80)] public float tiltAngle = 45f;

    [Header("Follow Settings")]
    public float smoothSpeed = 5f;

    private Vector3 desiredPosition;

    void LateUpdate()
    {
        if (!target) return;

        // --- CAMERA OFFSET ---
        // Rotate backward around the player (like ACNH’s diagonal camera)
        Vector3 direction = Quaternion.Euler(tiltAngle, 0, 0) * Vector3.back;
        Vector3 offset = direction.normalized * distance;
        offset.y += height;

        // Desired position farther behind and slightly above the player
        Vector3 desiredPosition = target.position + offset;

        // Smooth camera motion
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look slightly ahead of the player instead of directly down
        Vector3 lookPoint = target.position + target.forward * 2f + Vector3.up * 1.5f;
        transform.LookAt(lookPoint);
    }
}
