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
        roomLayout = Seed.GenerateRoomLayout(columns, rows);
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
                    tile.GetComponent<Tile>().SetCode(x * 100 + y);
                    tile.GetComponent<Tile>().isWalkable = roomLayout[x, y];
                    if (x > 0)
                    {
                        tile.GetComponent<Tile>().SetLeft(grid[x - 1, y].GetComponent<Tile>());
                    }
                    if(y>0)
                    {
                        tile.GetComponent<Tile>().SetBottom(grid[x, y - 1].GetComponent<Tile>());
                    }

                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }
}
