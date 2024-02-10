using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private List<Tile> path;
    private EnemyMovement enemyMovement;
    private PlayerInput playerInput;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        SearchPath();
    }

    public void SearchPath()
    {
        path = FindPath.FindPathAStar(enemyMovement.GetCurrentTile(), playerInput.GetCurrentTile());
        enemyMovement.SetPatrolPoints(path);
    }


}
