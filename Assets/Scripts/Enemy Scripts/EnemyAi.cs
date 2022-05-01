using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [Header("Player target")]
    [SerializeField] private PlayerTank _playerTank;
    [Header("Enemy move speed")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    [Header("Roaming patrol attributes")]
    [SerializeField] private float _minRoamDistance;
    [SerializeField] private float _maxRoamDistance;

    [SerializeField] private Vector2 _newPosition;
    [SerializeField] private float _roamDistance;

    [SerializeField] private float _timeBetweenActions;

    [Header("Player searching attributes")]
    [SerializeField] private float _searchingDistance;
    [SerializeField] private float _maxChasingDistance;
    [SerializeField] private float _stopDistance;
    [SerializeField] private bool _stopPlayerChasing = false;

    [Header("Attack attributes")]
    [SerializeField] private FireCoolDown _coolDown;
    [SerializeField] private bool _hasTarget = false;

    [Header("")]
    [SerializeField] private FirePoint _firePoint;

    [SerializeField] DebugPoint _debugPoint;

    private bool _newPointReady = false;

    void Start()
    {
        _playerTank = FindObjectOfType<PlayerTank>();
        _firePoint = GetComponentInChildren<FirePoint>();
        _coolDown = GetComponent<FireCoolDown>();
        StartCoroutine(PatrolCD());
    }

    private void Update()
    {
        if(_playerTank == null)
        {
            return;
        }

        Move();
        Search();

        if(_hasTarget)
        {
            Attack();
        }
    }


    private void NewPatrolPoint()
    {
        float newX = Random.Range(_minRoamDistance, _maxRoamDistance);
        float newY = Random.Range(_minRoamDistance, _maxRoamDistance);

        _newPosition = new Vector2(newX, newY);

        Instantiate(_debugPoint.gameObject, _newPosition, Quaternion.identity);

        StopCoroutine(PatrolCD());

        _newPointReady = false;
    }

    private void Move()
    {
        _roamDistance = Vector2.Distance(_newPosition, transform.position);

        if (_roamDistance >= 1)
        {
            if (!_stopPlayerChasing)
            {
                transform.position += ((Vector3)_newPosition - transform.position).normalized * _speed * Time.deltaTime;
            }
            Rotation();
        }
        else
        {
            if (!_newPointReady)
            {
                _newPointReady = true;
                StartCoroutine(PatrolCD());
            }
        }
    }

    private void Rotation()
    {
        var angle = Vector2.Angle(Vector2.up, (Vector3)_newPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, transform.position.x > _newPosition.x ? angle : -angle), _rotationSpeed);
    }

    private void Search()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, _playerTank.transform.position);
        if (distanceToPlayer <= _searchingDistance)
        {
            _newPosition = _playerTank.transform.position;
            _hasTarget = true;
        }
    }

    private void Attack()
    {
        AttackMovement();

        if (!_coolDown.GetCD())
        {
            _firePoint.Shot();
            StartCoroutine(_coolDown.FireCD());
        }
    }

    private void AttackMovement()
    {
        var playerTank = _playerTank.transform.position;

        if (Vector2.Distance(transform.position, playerTank) <= _stopDistance)
        {
            _stopPlayerChasing = true;
        }
        else
        {
            _stopPlayerChasing = false;
        }

        if (Vector2.Distance(transform.position, playerTank) >= _maxChasingDistance && _hasTarget)
        {
            _hasTarget = false;
            StartCoroutine(PatrolCD());
        }
    }

    private IEnumerator PatrolCD()
    {
        yield return new WaitForSeconds(_timeBetweenActions);
        NewPatrolPoint();
    }
}
