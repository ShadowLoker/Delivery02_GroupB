using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f; // The range of detection
    public float detectionAngle = 60f; // The angle of detection (in degrees)
    public Transform player; // The player's transform

    public float speed = 1f; // Speed of the enemy in pixels per frame
    private Vector3 accumulatedMovement = Vector3.zero;
    private int pixelsPerUnit = 16; // This should match with your sprite's pixels per unit
    private float directionChangeCooldown = 0.2f; // Cooldown period in seconds
    private float timeSinceLastDirectionChange = 0f; // Time since last direction change


    private Rigidbody2D rb; // The enemy's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Calculate the start and end angles for the detection cone
        float startAngle = Vector2.SignedAngle(Vector2.right, directionToPlayer) - detectionAngle / 2;
        float endAngle = startAngle + detectionAngle;

        // Define the number of rays to cast
        int rayCount = 10;

        // Calculate the angle between each ray
        float angleBetweenRays = (endAngle - startAngle) / (rayCount - 1);

        // Initialize a flag to indicate whether the player has been detected
        bool playerDetected = false;

        // Cast the rays
        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the direction of the current ray
            float currentAngle = startAngle + i * angleBetweenRays;
            Vector2 rayDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // Cast the ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRange);

            // If the ray hit something...
            if (hit.collider != null)
            {
                // If the ray hit a wall, stop casting further rays in this direction
                if (hit.collider.gameObject.CompareTag("Wall"))
                {
                    continue;
                }

                // If the ray hit the player, set the flag to true and break the loop
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    playerDetected = true;
                    break;
                }
            }
        }

        // If the player was detected, chase the player
        if (playerDetected)
        {
            ChasePlayer(directionToPlayer);
        }
        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the direction of the current ray
            float currentAngle = startAngle + i * angleBetweenRays;
            Vector2 rayDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // Cast the ray
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, detectionRange);

            // Draw the ray in the Scene view
            Debug.DrawRay(transform.position, rayDirection * detectionRange, Color.red);

            // If the ray hit something...
            if (hit.collider != null)
            {
                // ... (same as before)
            }
        }
    }


    void ChasePlayer(Vector2 direction)
    {
        Vector2 roundedDirection = RoundDirectionToEightWay(direction);

        Vector3 movement = new Vector3(roundedDirection.x, roundedDirection.y, 0.0f).normalized;

        if (movement != Vector3.zero)
        {
            accumulatedMovement += movement * speed / pixelsPerUnit * Time.fixedDeltaTime;

            if (accumulatedMovement.magnitude >= 1.0f / pixelsPerUnit)
            {
                Vector3 newPosition = transform.position + accumulatedMovement;
                newPosition.x = Mathf.Round(newPosition.x * pixelsPerUnit) / pixelsPerUnit;
                newPosition.y = Mathf.Round(newPosition.y * pixelsPerUnit) / pixelsPerUnit;
                transform.position = newPosition;

                accumulatedMovement = Vector3.zero; // Reset the accumulated movement
            }
        }

        // Rotate the enemy to face the player
        float angle = Mathf.Atan2(roundedDirection.y, roundedDirection.x) * Mathf.Rad2Deg;
        float currentAngle = transform.rotation.eulerAngles.z;
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, angle));

        
      
        if (timeSinceLastDirectionChange >= directionChangeCooldown)
        {
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
