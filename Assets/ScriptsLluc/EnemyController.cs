using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    private List<Tile> path;

    private EnemyAI enemyAI;

    public Transform pointA;
    public Transform pointB;
    private Transform targetPoint;

    private static GridManager grid;

    private void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridManager>();
        enemyAI = GetComponent<EnemyAI>();
        targetPoint = pointA;
        enemyAI.OnSearchPath += SearchPath;
        enemyAI.OnPatrolEnd += NextPoint;
    }
    private void Update()
    {
        if(grid==null)
            grid = GetComponent<GridManager>();
    }


    public void SearchPath()
    {
        path = FindPath.FindPathAStar(GetCurrentTile(), GetNextPoint());
        enemyAI.SetPathPoints(path);
    }

    private void OnDestroy()
    {
        enemyAI.OnSearchPath -= SearchPath;
        enemyAI.OnPatrolEnd -= NextPoint;
    }


    internal Tile GetCurrentTile()
    {      
        return grid.GetTileAt(transform.position);
    }

    internal Tile GetNextPoint()
    {
        return grid.GetTileAt(targetPoint.position);
    }

    internal void NextPoint()
    {
        if (targetPoint == pointA || Vector2.Distance(transform.position,pointA.transform.position) < 0.3f)
        {
            targetPoint = pointB;
        }
        else
        {
            targetPoint = pointA;
        }
        SearchPath();
    }

}
