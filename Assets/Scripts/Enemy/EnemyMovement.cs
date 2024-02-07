using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float CooldownTime;

    public List<Transform> _patrolPoints;
    private int _currentPatrolPoint = 0;
    [SerializeField]
    private float _speed = 2;
    [SerializeField]
    private float _detectionRadius = 5;
    private float _cooldown = 0;
    private Vector2 _direction;

    private Transform _player;
    private EnemyAlarm _alarm;
    private Rigidbody2D _rigidbody;

    public enum State
    {
        Patrolling,
        Chasing,
        Cooldown,
        Checking
    }
    public State CurrentState { get => _currentState; private set => _currentState = value; }
    private State _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _alarm = GetComponentInChildren<EnemyAlarm>();

    }

    // Update is called once per frame
    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_currentState == State.Chasing)

            _direction = (_player.position - transform.position).normalized;

        else if (_currentState == State.Patrolling)

            _direction = (_patrolPoints[_currentPatrolPoint].position - transform.position).normalized;

        if(_currentState==State.Cooldown) 
        {
            _rigidbody.velocity = Vector2.zero;
            _cooldown -= Time.deltaTime;

            if(_cooldown <= 0)
                _currentState = State.Patrolling;
        }
        else
        {
            Move(_direction);
            CheckPosition();
        }
        
        
    }

    void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _speed;
        LookAt((Vector2)transform.position + direction);
    }
    void LookAt(Vector2 targetPosition)
    {
        float angle;
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
    void CheckPosition()
    {
        if(Vector2.Distance(transform.position, _patrolPoints[_currentPatrolPoint].position) < 0.1f)
        {
            if (_currentState == State.Patrolling)
            {
                _currentPatrolPoint++;
                _cooldown = CooldownTime;
                _currentState = State.Cooldown;

                if (_currentPatrolPoint >= _patrolPoints.Count)
                {
                    _currentPatrolPoint = 1;
                    _patrolPoints.Reverse();
                }
            }
        }
    }

}
