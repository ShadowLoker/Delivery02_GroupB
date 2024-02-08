using UnityEngine;

public interface IMovementInput
{
    Vector2 GetMovementInput();
    bool GetCrouchInput();
}
