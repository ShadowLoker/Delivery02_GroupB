using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using static FoV;

public class EnemyAI : MonoBehaviour
{
    public FoV fov;
    public AudioManager audioManager; // Reference to the AudioManager

    public Transform player; // The player's transform

    public float speed = 1f; // Speed of the enemy in pixels per frame

    public delegate void pathComplete();
    public pathComplete OnSearchPath;

    public delegate void patrolEnd();
    public patrolEnd OnPatrolEnd;

    public List<Tile> pathPoints; // The patrol points
    private int currentPathPoint = 0; // The current patrol point index
    public float patrolSpeed = 0.5f; // Speed of the enemy while patrolling
    public float waitTime = 2f; // Time to wait at each patrol point
    public float rotationSpeed = 200f; // Speed at which the enemy rotates

    public bool isPatrolling = true; // Is the enemy currently patrolling?
    public bool isMoving;

    public PlayerDetectionState detectionState { get; private set; }


    private Rigidbody2D rb; // The enemy's Rigidbody2D component

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        OnSearchPath?.Invoke();
        StartCoroutine(Patrol());
        isMoving = true;
        AudioManager.AddEnemy(this); // Add the enemy to the list of enemies

    }

    void Update()
    {
        detectionState = fov.IsPlayerInFieldOfView();
        isMoving = rb.velocity != Vector2.zero;

        switch (detectionState)
        {
            case PlayerDetectionState.FullyDetected:
                
                fov.StartChangingFieldOfView(true);
                StopAllCoroutines();
                isPatrolling = false;
                rb.velocity = Vector2.zero;
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                
                ChasePlayer(directionToPlayer);
                break;



            case PlayerDetectionState.PartiallyDetected:
                
                StopAllCoroutines();
                isPatrolling = false;
                rb.velocity = Vector2.zero;
                directionToPlayer = (player.position - transform.position).normalized;
                StartCoroutine(RotateToDirection(directionToPlayer));
                break;

            case PlayerDetectionState.NotDetected:
                fov.StartChangingFieldOfView(false);
                rb.velocity = Vector2.zero;
                if (!isPatrolling)
                {
                    rb.velocity = Vector2.zero;
                    currentPathPoint = 0;
                    OnSearchPath?.Invoke();
                    StartCoroutine(ReturnToPatrol());
                }
                break;
        }
        
    }


    IEnumerator Patrol()
    {
        
        isPatrolling = true;
        while (isPatrolling)
        {
            if (Vector2.Distance(transform.position, pathPoints[currentPathPoint].transform.position) < 0.1f)
            {
                currentPathPoint++;
                if (currentPathPoint >= pathPoints.Count)
                {
                    currentPathPoint = 0;
                    OnPatrolEnd?.Invoke();
                    isPatrolling = false;
                    StartCoroutine(ReturnToPatrol());
                    yield break;
                }
                StartCoroutine(Patrol());

            }
            else
            {
                Vector2 direction = (pathPoints[currentPathPoint].transform.position - transform.position).normalized;
                rb.velocity = direction * patrolSpeed;
                Vector2 roundedDirection = RoundDirectionToEightWay(direction);
                
                yield return RotateToDirection(roundedDirection);
                yield return null;
            }
        }
    }

    IEnumerator ReturnToPatrol()
    {
        
        rb.velocity = Vector2.zero; // Stop the enemy
        yield return new WaitForSeconds(waitTime); // Wait at the patrol point
        Vector2 directionToNextPatrolPoint = (pathPoints[currentPathPoint].transform.position - transform.position).normalized;
        Vector2 roundedDirection = RoundDirectionToEightWay(directionToNextPatrolPoint);
        // Start the rotation coroutine
        yield return StartCoroutine(RotateToDirection(roundedDirection)); // Rotate over 1 second
        StartCoroutine(Patrol());

    }



    void ChasePlayer(Vector2 direction)
    {
        
        Vector2 roundedDirection = RoundDirectionToEightWay(direction);
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Subtract 90 to align the enemy's up direction with the direction


        rb.velocity = roundedDirection * speed;
    }

    IEnumerator RotateToDirection(Vector2 direction)
    {

        
        // Calculate the target angle
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        // Rotate the enemy to face the direction
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // Interpolate between the current rotation and the target rotation
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            yield return null;
        }
    }




    Vector2 RoundDirectionToEightWay(Vector2 direction)
    {
        float step = 45.0f;
        float angle = Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Round(angle / (step * Mathf.Deg2Rad)) * (step * Mathf.Deg2Rad);

        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    internal void SetPathPoints(List<Tile> path)
    {
        pathPoints = path;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(pathPoints != null)
        {
            Tile previous = null;
            foreach (Tile point in pathPoints)
            {
                if(previous != null)
                {
                    Gizmos.DrawLine(previous.transform.position, point.transform.position);
                }
                previous = point;
            }
        }
    }
}
