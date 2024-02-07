using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _input;
    private PlayerMovement _movement;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        _animator.SetBool("Walk", _movement.IsMoving);
        _animator.SetBool("Sneak", _input.Sneak);
    }
}
