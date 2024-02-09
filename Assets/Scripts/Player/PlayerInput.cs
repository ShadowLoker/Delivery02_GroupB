using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void ResetSceneInputDelegate();
    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }
    public bool Sneak;
    public static ResetSceneInputDelegate OnResetSceneInput;

    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        MovementVertical = Input.GetAxis("Vertical");
        Sneak = Input.GetKey(KeyCode.LeftShift);
    }
}
