using UnityEngine;

public class RollingCamera : MonoBehaviour
{
    [Header("Camera Movement")]
    public Transform target;           // The world center (pivot)
    public float radius = 50f;         // Distance from center
    public float moveSpeed = 10f;      // How fast to roll
    public float minRadius = 40f;      // Closest zoom
    public float maxRadius = 60f;      // Furthest zoom

    [Header("Camera Angle")]
    public float height = 20f;         // Fixed height above ground
    public float tilt = 20f;           // Downward tilt angle
    public float rotationOffset = 0f;  // Starting rotation angle

    private float currentAngle = 0f;

    void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("No target assigned! Please assign the world center.");
            return;
        }

        // Set initial position
        UpdateCameraPosition();
    }

    void Update()
    {
        if (target == null) return;

        // Move forward/back (W/S)
        float input = Input.GetAxis("Vertical");
        currentAngle += input * moveSpeed * Time.deltaTime;

        // Optionally adjust radius for zoom with Q/E
        if (Input.GetKey(KeyCode.Q)) radius -= moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) radius += moveSpeed * Time.deltaTime;
        radius = Mathf.Clamp(radius, minRadius, maxRadius);

        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // Convert angle around center to position (rolling-pin effect)
        float x = Mathf.Sin(currentAngle + rotationOffset) * radius;
        float z = Mathf.Cos(currentAngle + rotationOffset) * radius;
        Vector3 position = new Vector3(x, height, z);
        transform.position = position + target.position;

        // Look at world center and tilt down a bit
        transform.LookAt(target);
        transform.rotation *= Quaternion.Euler(tilt, 0f, 0f);
    }
}
