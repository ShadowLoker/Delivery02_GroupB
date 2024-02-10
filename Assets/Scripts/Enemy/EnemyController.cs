using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private List<Tile> path;
    private EnemyMovement enemyMovement;

    public Transform PointA;
    public Transform PointB;

    private void Awake()
    {   
        enemyMovement = GetComponent<EnemyMovement>();
        enemyMovement.OnPathComplete += SearchPath;
    }
    private void Start()
    {
        
    }

    public void SearchPath()
    {
        path = FindPath.FindPathAStar(GetCurrentTile(), GetNextPoint());
        enemyMovement.SetPatrolPoints(path);
    }

    private void OnDestroy()
    {
        enemyMovement.OnPathComplete -= SearchPath;
    }


    internal Tile GetCurrentTile()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        return gridManager.GetTileAt(transform.position);
    }

    internal Tile GetNextPoint()
    {
        GridManager gridManager = FindObjectOfType<GridManager>();
        if (gridManager.GetTileAt(transform.position) == gridManager.GetTileAt(PointA.position))
        {
            return gridManager.GetTileAt(PointB.position);
        }
        else
        {
            return gridManager.GetTileAt(PointA.position);
        }
    }

}
