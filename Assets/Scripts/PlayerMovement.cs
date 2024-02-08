// Player_Movement.cs
using UnityEngine;

public class Player_Movement : Movement
{
    public int speed = 1; // Speed of the player in pixels per frame
    private int pixelsPerUnit = 16; // This should match with your sprite's pixels per unit
    private IMovementInput input;


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
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);

        if (movement != Vector3.zero)
        {
            movement = movement.normalized * speed * Time.fixedDeltaTime;
            transform.position += movement;
        }
    }
}