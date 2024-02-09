using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving => _isMoving;

    [SerializeField]
    private float Speed = 5.0f;

    private bool _isMoving;
    PlayerInput _input;
    Rigidbody2D _rigidbody;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = new Vector2(_input.MovementHorizontal, _input.MovementVertical) 
            * (_input.Sneak ? Speed/2 : Speed);
        _rigidbody.velocity = direction;
        _isMoving = direction.magnitude > 0.01f;

        if (_isMoving) LookAt((Vector2)transform.position + direction);
        else transform.rotation = Quaternion.identity;
    }

    void LookAt(Vector2 targetPosition)
    {
        float angle = 0.0f;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}
