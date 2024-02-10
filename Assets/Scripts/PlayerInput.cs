using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public bool Sneak;
    [SerializeField]
    private Tile _currentTile;

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        Sneak = Input.GetKey(KeyCode.LeftShift);
    }

    public void SetCurrentTile(Tile current)
    {
        _currentTile = current;
    }
    internal Tile GetCurrentTile()
    {
        return _currentTile;
    }
}
