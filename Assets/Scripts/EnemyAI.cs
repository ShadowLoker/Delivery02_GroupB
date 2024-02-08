using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public FoV fov;

    public Transform player; // The player's transform

    public float speed = 1f; // Speed of the enemy in pixels per frame
    public float speedx;

    
    private float directionChangeCooldown = 0.2f; // Cooldown period in seconds
    private float timeSinceLastDirectionChange = 0f; // Time since last direction change


    private Rigidbody2D rb; // The enemy's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (fov.IsPlayerInFieldOfView())
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            ChasePlayer(directionToPlayer);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


    void ChasePlayer(Vector2 direction)
    {
        Vector2 roundedDirection = RoundDirectionToEightWay(direction);
        RotateToPlayer(roundedDirection);

        //move enemy to player

        rb.velocity = roundedDirection * speed;

    }

    void RotateToPlayer(Vector2 roundedDirection)
    {
        // Rotate the enemy to face the player
        if (timeSinceLastDirectionChange >= directionChangeCooldown)
    {
        float angle = Mathf.Atan2(roundedDirection.y, roundedDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 to align the enemy's up direction with the direction to the player
        timeSinceLastDirectionChange = 0f; // Reset the timer
    }

// Update the timer
    timeSinceLastDirectionChange += Time.fixedDeltaTime;
    }
    Vector2 RoundDirectionToEightWay(Vector2 direction)
    {
        float step = 45.0f;
        float angle = Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Round(angle / (step * Mathf.Deg2Rad)) * (step * Mathf.Deg2Rad);

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
