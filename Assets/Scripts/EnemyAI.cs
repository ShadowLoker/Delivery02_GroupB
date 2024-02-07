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
        Vector2 directionToPlayer = player.position - transform.position; // The direction from the enemy to the player
        float distanceToPlayer = directionToPlayer.magnitude; // The distance from the enemy to the player

        // If the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Calculate the angle between the enemy's forward direction and the direction to the player
            float angle = Vector2.Angle(transform.up, directionToPlayer);

            // If the player is within the detection angle
            if (angle <= detectionAngle * 0.5f) // We multiply by 0.5 because the angle is the total angle of the cone, not from the middle to the side
            {
                Debug.Log("Player detected");
                ChasePlayer(directionToPlayer.normalized); // Chase the player

                // Draw a raycast pointing towards the player
                Debug.DrawRay(transform.position, directionToPlayer, Color.green);
                return; // Exit the function here to prevent the enemy's velocity from being set to zero below
            }
        }

        // If the player is not detected, stop the enemy
        rb.velocity = Vector2.zero;

        // Draw the detection cone
        float coneResolution = 10; // The number of rays to draw for the cone
        for (int i = 0; i <= coneResolution; i++)
        {
            float interpolation = (float)i / coneResolution; // The interpolation factor
            float angle = Mathf.Lerp(-detectionAngle * 0.5f, detectionAngle * 0.5f, interpolation);
            Vector2 coneDirection = Quaternion.Euler(0, 0, angle) * transform.up;
            Debug.DrawRay(transform.position, coneDirection * detectionRange, Color.red);
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
