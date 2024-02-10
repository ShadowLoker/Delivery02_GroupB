using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private List<Tile> path;

    private EnemyAI enemyAI;

    public Transform PointA;
    public Transform PointB;

    private void Awake()
    {   
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.OnPathComplete += SearchPath;
    }
    private void Start()
    {
        
    }

    public void SearchPath()
    {
        path = FindPath.FindPathAStar(GetCurrentTile(), GetNextPoint());
        enemyAI.SetPathPoints(path);
    }

    private void OnDestroy()
    {
        enemyAI.OnPathComplete -= SearchPath;
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
