using UnityEngine;

public class PlayerInput : IMovementInput
{
    public Vector2 GetMovementInput()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        return new Vector2(moveHorizontal, moveVertical);
    }

    public bool GetCrouchInput()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}