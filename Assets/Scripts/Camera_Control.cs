// Camera_Control.cs
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_Control : MonoBehaviour
{
    public Transform target; // The target that the camera should follow

    public int pixelsPerUnit = 16; // Set this to match your sprites' pixels per unit
    private Vector3 desiredPosition;

    private void Awake()
    {
        desiredPosition = target.position + new Vector3(0.0f, 0.0f, -10.0f);
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            desiredPosition.x = Mathf.Round(target.position.x * pixelsPerUnit) / pixelsPerUnit;
            desiredPosition.y = Mathf.Round(target.position.y * pixelsPerUnit) / pixelsPerUnit;

            transform.position = desiredPosition;
        }
    }
}