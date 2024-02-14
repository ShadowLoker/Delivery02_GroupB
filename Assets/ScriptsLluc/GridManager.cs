using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject walkTilePrefab;
    public GameObject wallTilePrefab;
    public int columns = 10;
    public int rows = 10;
    private bool[,] roomLayout;
    private GameObject[,] grid;

    void Start()
    {
        roomLayout = Seed.GenerateRoomLayout();
        columns = roomLayout.GetLength(0);
        rows = roomLayout.GetLength(1);
        Debug.Log(columns+" "+rows);
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 position = new Vector3(transform.position.x+x, transform.position.y+y, 0);
                GameObject tile = Instantiate(roomLayout[x,y]?walkTilePrefab:wallTilePrefab, position, Quaternion.identity);
                tile.transform.parent = transform;
                grid[x, y] = tile;
                try
                {
                    tile.GetComponent<Tile>().SetCode(new Vector2(x,y));
                    tile.GetComponent<Tile>().isWalkable = roomLayout[x, y];
                    if (x > 0)
                    {
                        tile.GetComponent<Tile>().AddNeighbourTile(grid[x - 1, y].GetComponent<Tile>());
                    }
                    if(y>0)
                    {
                        tile.GetComponent<Tile>().AddNeighbourTile(grid[x, y - 1].GetComponent<Tile>());
                    }

                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }

    internal Tile GetTileAt(Vector3 position)
    {
        float x = position.x - transform.position.x;
        float y = position.y - transform.position.y;
        x/=walkTilePrefab.transform.localScale.x;
        y/=walkTilePrefab.transform.localScale.y;
        int xIndex = Mathf.RoundToInt(x);
        int yIndex = Mathf.RoundToInt(y);
        if (xIndex < 0 || xIndex >= columns || yIndex < 0 || yIndex >= rows)
        {
            return null;
        }
        return grid[xIndex, yIndex].GetComponent<Tile>();
    }
}
