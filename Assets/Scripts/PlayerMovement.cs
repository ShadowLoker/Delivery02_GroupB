// Player_Movement.cs
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public int speed = 5; // Speed of the player in pixels per frame
    public int crouchSpeed = 1; // Speed of the player when crouching
    private IMovementInput input;

    public bool isCrouching;
    public bool isMoving;
    private int currentGems = 0;
    private Rigidbody2D rb;


    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetInput(IMovementInput inputB) // Change this to IMovementInput
    {
        input = inputB;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector2 movementInput = input.GetMovementInput();
        isCrouching = input.GetCrouchInput();

        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);

        if (movement != Vector3.zero)
        {
            isMoving = true;
            int currentSpeed = isCrouching ? crouchSpeed : speed-currentGems;
            movement = movement.normalized * (currentSpeed) * Time.fixedDeltaTime;
            transform.position += movement;
        }
        else
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
    }

    internal Tile GetCurrentTile()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        return gridManager.GetTileAt(transform.position);
    }

    public void Collect()
    {
        currentGems++;
        Debug.Log("Collected");
    }
}
