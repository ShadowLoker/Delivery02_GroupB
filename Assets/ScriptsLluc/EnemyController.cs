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
        enemyAI.OnSearchPath += SearchPath;
    }
    private void Update()
    {
        if(grid==null)
            grid = GetComponent<GridManager>();
    }
    private void Start()
    {
        
        targetPoint = pointA;
    }

    public void SearchPath()
    {
        path = FindPath.FindPathAStar(GetCurrentTile(), GetNextPoint());
        enemyAI.SetPathPoints(path);
    }

    private void OnDestroy()
    {
        enemyAI.OnSearchPath -= SearchPath;
    }


    internal Tile GetCurrentTile()
    {
        Debug.Log(grid.ToString());
        return grid.GetTileAt(transform.position);
    }

    internal Tile GetNextPoint()
    {
        if (grid.GetTileAt(transform.position) == grid.GetTileAt(pointA.position))
        {
            return grid.GetTileAt(pointB.position);
        }
        else
        {
            return grid.GetTileAt(pointA.position);
        }
    }

}
