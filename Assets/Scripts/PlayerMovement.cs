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

    private Animator animator;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetInput(IMovementInput inputB) // Change this to IMovementInput
    {
        input = inputB;
    }

    private void FixedUpdate()
    {
        Move();
        if(isMoving)
        {
            animator.Play("PlayerWalkAnimation");
        }
        else
        {
            animator.Play("PlayerIdleAnimation");
        }
    }

    public void Move()
    {
        Vector2 movementInput = input.GetMovementInput();
        isCrouching = input.GetCrouchInput();

        Vector2 movement = new Vector3(movementInput.x, movementInput.y, 0.0f);

        if (movement != Vector2.zero)
        {
            isMoving = true;
            int currentSpeed = isCrouching ? crouchSpeed : speed-currentGems;
            movement = movement.normalized * (currentSpeed) * Time.fixedDeltaTime;
            rb.position += movement;
            SetSpriteRotation(RoundDirectionToEightWay(movement));
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

    private void SetSpriteRotation(Vector2 movement)
    {
        GetComponent<SpriteRenderer>().transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
    }

    Vector2 RoundDirectionToEightWay(Vector2 direction)
    {
        float step = 45.0f;
        float angle = Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Round(angle / (step * Mathf.Deg2Rad)) * (step * Mathf.Deg2Rad);

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
