using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private List<Tile> neighbours = new List<Tile>();
    public int gCost { get; private set;}
    public int hCost { get; private set;}
    public int fCost { get { return gCost + hCost; } }

    public Tile parent;

    public Vector2 code { get; private set;}
    public bool isWalkable;

    public void SetCode(Vector2 code)
    {
        this.code = code;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Tile: " + code);
            Debug.Log("IsWalkable: " + isWalkable);
        }
    }
    
    public void AddNeighbourTile(Tile neighbour)
    {
        if(neighbour == null || neighbours.Contains(neighbour))
            return;
        neighbours.Add(neighbour);
        neighbour.AddNeighbourTile(this);

    }

    public IEnumerable<Tile> GetNeighbours()
    {
        return neighbours;
    }

    internal void SetGCost(int newMovementCostToNeighbour)
    {
        gCost = newMovementCostToNeighbour;
    }

    internal void SetHCost(int v)
    {
        hCost = v;
    }

}
