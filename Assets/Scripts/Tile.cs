using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Tile top;
    private Tile bottom;
    private Tile left;
    private Tile right;

    private Tile topLeft;
    private Tile topRight;
    private Tile bottomLeft;
    private Tile bottomRight;

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
            Debug.Log("Top: " + (top == null ? "null" : top.name));
            Debug.Log("Bottom: " + (bottom == null ? "null" : bottom.name));
            Debug.Log("Left: " + (left == null ? "null" : left.name));
            Debug.Log("Right: " + (right == null ? "null" : right.name));
            Debug.Log("TopLeft: " + (topLeft == null ? "null" : topLeft.name));
            Debug.Log("TopRight: " + (topRight == null ? "null" : topRight.name));
            Debug.Log("BottomLeft: " + (bottomLeft == null ? "null" : bottomLeft.name));
            Debug.Log("BottomRight: " + (bottomRight == null ? "null" : bottomRight.name));
        }
    }
    
    public void SetLeft (Tile left)
    {
        if(left == null)
            return;
        this.left = left;
        left.right = this;
        if(left.top != null)
        {
            topLeft = left.top;
            topLeft.bottomRight = this;
        }

        if(left.bottom != null)
        {
            bottomLeft = left.bottom;
            bottomLeft.topRight = this;
        }
            
            
    }
    public void SetBottom(Tile bottom)
    {
        if(bottom == null)
            return;
        this.bottom = bottom;
        bottom.top = this;
        if(bottom.right != null)
        {
            bottomRight = bottom.right;
            bottomRight.topLeft = this;
        }

    }

    public static IEnumerable<Tile> GetNeighbours(Tile currentTile)
    {

        List<Tile> neighbours = new List<Tile>();

        if (currentTile.top != null)
        {
            neighbours.Add(currentTile.top);
        }
        if (currentTile.bottom != null)
        {
            neighbours.Add(currentTile.bottom);
        }
        if (currentTile.left != null)
        {
            neighbours.Add(currentTile.left);
        }
        if (currentTile.right != null)
        {
            neighbours.Add(currentTile.right);
        }
        if (currentTile.topLeft != null)
        {
            neighbours.Add(currentTile.topLeft);
        }
        if (currentTile.topRight != null)
        {
            neighbours.Add(currentTile.topRight);
        }
        if (currentTile.bottomLeft != null)
        {
            neighbours.Add(currentTile.bottomLeft);
        }
        if (currentTile.bottomRight != null)
        {
            neighbours.Add(currentTile.bottomRight);
        }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInput>().SetCurrentTile(this);
        }else if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMovement>().SetCurrentTile(this);
        }
    }
}
