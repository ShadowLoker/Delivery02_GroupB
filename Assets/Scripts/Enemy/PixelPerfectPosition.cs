using UnityEngine;

public class PixelPerfectPosition : MonoBehaviour
{
    public float pixelsPerUnit = 100.0f;
    private Vector3 fractionalPosition = Vector3.zero;

    void LateUpdate()
    {
        Vector3 position = transform.position * pixelsPerUnit + fractionalPosition;
        Vector3 roundedPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z));
        fractionalPosition = position - roundedPosition;
        transform.position = roundedPosition / pixelsPerUnit;
    }
}
