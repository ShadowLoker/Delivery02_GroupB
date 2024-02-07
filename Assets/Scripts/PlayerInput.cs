using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool Sneak;

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Sneak = Input.GetKey(KeyCode.LeftShift);
    }
}
