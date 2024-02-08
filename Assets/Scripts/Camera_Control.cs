using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    public Transform target; // The target that the camera should follow
    public float smoothSpeed = 0.125f; // The speed at which the camera catches up with the target
    public int pixelsPerUnit = 16; // The desired pixels per unit

    private Vector3 desiredPosition;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        camera.orthographic = true;
    }

    private void Update()
    {
        desiredPosition = target.position + new Vector3(0.0f, 0.0f, -10.0f);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Adjust the camera's orthographic size based on the screen's height and the desired pixels per unit
        camera.orthographicSize = Screen.height / (2.0f * pixelsPerUnit);
    }
}