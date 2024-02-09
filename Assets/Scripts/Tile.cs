using System.Collections;
using System.Collections.Generic;
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

    private int code;
    public bool isWalkable;

    public void SetCode(int code)
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
}
