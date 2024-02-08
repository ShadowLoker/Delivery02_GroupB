using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FoV;

public class EnemyAI : MonoBehaviour
{
    public FoV fov;

    public Transform player; // The player's transform

    public float speed = 1f; // Speed of the enemy in pixels per frame
    public float speedx;

    public Transform[] patrolPoints; // The patrol points
    private int currentPatrolIndex = 0; // The current patrol point index
    public float patrolSpeed = 0.5f; // Speed of the enemy while patrolling
    public float waitTime = 2f; // Time to wait at each patrol point

    private bool isPatrolling = true; // Is the enemy currently patrolling?



    private float directionChangeCooldown = 0.2f; // Cooldown period in seconds
    private float timeSinceLastDirectionChange = 0f; // Time since last direction change


    private Rigidbody2D rb; // The enemy's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        StartCoroutine(Patrol());
    }

    void Update()
    {
        PlayerDetectionState detectionState = fov.IsPlayerInFieldOfView();

        switch (detectionState)
        {
            case PlayerDetectionState.FullyDetected:
                isPatrolling = false;
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                ChasePlayer(directionToPlayer);
                break;

            case PlayerDetectionState.PartiallyDetected:
                isPatrolling = false;
                rb.velocity = Vector2.zero; // Stop moving
                break;

            case PlayerDetectionState.NotDetected:
                if (!isPatrolling)
                {
                    rb.velocity = Vector2.zero;
                    StartCoroutine(ReturnToPatrolAfterDelay());
                }
                break;
        }
    }


    IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.2f)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Vector2 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
                rb.velocity = direction * patrolSpeed;
                Vector2 roundedDirection = RoundDirectionToEightWay(direction);
                RotateToDirection(roundedDirection);
                yield return null;
            }
        }
    }

    IEnumerator ReturnToPatrolAfterDelay()
    {
        yield return new WaitForSeconds(waitTime);
        isPatrolling = true;
        StartCoroutine(Patrol());
    }

    void ChasePlayer(Vector2 direction)
    {
        Vector2 roundedDirection = RoundDirectionToEightWay(direction);
        RotateToDirection(roundedDirection);

        //move enemy to player

        rb.velocity = roundedDirection * speed;

    }

    void RotateToDirection(Vector2 direction)
    {
        // Rotate the enemy to face the direction
        if (timeSinceLastDirectionChange >= directionChangeCooldown)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 to align the enemy's up direction with the direction
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
