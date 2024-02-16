using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetValues : MonoBehaviour
{
    private void Start()
    {
        PlayTimer.ResetTime();
        PlayerDistanceTotal.ResetDistance();

    }
    
}
