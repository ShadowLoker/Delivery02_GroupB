// Player_Movement.cs
using UnityEngine;
public class Player_Movement : Movement
{
    public int speed = 2; // Speed of the player in pixels per frame
    public int crouchSpeed = 1; // Speed of the player when crouching
    private IMovementInput input;

    public bool isCrouching;
    public bool isMoving;

    private void Start()
    {
        input = new PlayerInput();
    }

    private void FixedUpdate()
    {
        Move(input);
    }

    public override void Move(IMovementInput input)
    {
        Vector2 movementInput = input.GetMovementInput();
        isCrouching = input.GetCrouchInput();


        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);

        if (movement != Vector3.zero)
        {
            isMoving = true;
            int currentSpeed = isCrouching ? crouchSpeed : speed;
            movement = movement.normalized * currentSpeed * Time.fixedDeltaTime;
            transform.position += movement;
        }
        else isMoving = false;
    }
}
