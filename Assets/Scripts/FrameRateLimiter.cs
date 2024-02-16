// FrameRateLimiter.cs
using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    public int targetFrameRate = 144; // The desired frame rate

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
