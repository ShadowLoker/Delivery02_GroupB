using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath
{

    public static List<Tile> FindPathAStar(Tile startTile, Tile targetTile)
    {
        List<Tile> openSet = new List<Tile>();
        HashSet<Tile> closedSet = new HashSet<Tile>();
        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            Tile currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentTile.fCost || openSet[i].fCost == currentTile.fCost && openSet[i].hCost < currentTile.hCost)
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == targetTile)
            {
                return RetracePath(startTile, targetTile);
            }

            foreach (Tile neighbour in Tile.GetNeighbours(currentTile))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.SetGCost(newMovementCostToNeighbour);
                    neighbour.SetHCost(GetDistance(neighbour, targetTile));
                    neighbour.parent = currentTile;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private static int GetDistance(Tile currentTile, Tile neighbour)
    {
        
        int dstX = Mathf.Abs((int)currentTile.code.x - (int)neighbour.code.x);
        int dstY = Mathf.Abs((int)currentTile.code.y - (int)neighbour.code.y);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

    private static List<Tile> RetracePath(Tile startTile, Tile targetTile)
    {
        
        List<Tile> path = new List<Tile>();
        Tile currentTile = targetTile;

        while (currentTile != startTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.parent;
        }
        path.Reverse();
        return path;
    }
}
