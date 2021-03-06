using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    //TODO ????????? ?????? 

    [Header("Player target")]
    [SerializeField] private PlayerTank _playerTank;

    [Header("Enemy move speed")]
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    [Header("Roaming patrol attributes")]
    [SerializeField] private float _minRoamDistance;
    [SerializeField] private float _maxRoamDistance;
    [SerializeField] private Vector2 _newTargetPosition;
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
    [SerializeField] private HealthContainer _healthContainer;

    private bool _newPointReady = false;
    private Vector3 _player;

    private void Awake() => _healthContainer = GetComponent<HealthContainer>();
    private void OnEnable() => _healthContainer.OnHealthChanged += OnDamageReceived;
    private void OnDisable() => _healthContainer.OnHealthChanged -= OnDamageReceived;

    private void Start()
    {
        _playerTank = FindObjectOfType<PlayerTank>();
        _firePoint = GetComponentInChildren<FirePoint>();
        _coolDown = GetComponent<FireCoolDown>();
        StartCoroutine(PatrolCD());
    }

    private void FixedUpdate()
    {

        if(_playerTank == null)
        {
            return;
        }

        _player = _playerTank.transform.position;

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

        _newTargetPosition = new Vector2(newX, newY);

        StopCoroutine(PatrolCD());

        _newPointReady = false;
    }

    private void Move()
    {
        _roamDistance = Vector2.Distance(_newTargetPosition, transform.position);

        if (_roamDistance >= 1)
        {
            if (!_stopPlayerChasing)
            {
                transform.position += ((Vector3)_newTargetPosition - transform.position).normalized * _speed * Time.deltaTime;
            }
            Rotation();
        }
        else
        {
           ChangePatrolPoint();
        }
    }

    private void ChangePatrolPoint()
    {
        if (!_newPointReady)
        {
            _newPointReady = true;
            StartCoroutine(PatrolCD());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_hasTarget && (collision.gameObject.TryGetComponent(out Wall wall) || collision.gameObject.TryGetComponent(out EnemyTank tank)))
        {
            ChangePatrolPoint();
        }
    }

    private void Rotation()
    {
        var angle = Vector2.Angle(Vector2.up, (Vector3)_newTargetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, transform.position.x > _newTargetPosition.x ? angle : -angle), _rotationSpeed);
    }

    private void Search()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, _player);
        if (distanceToPlayer <= _searchingDistance)
        {
            AttackOn();
        }
    }

    private void AttackOn()
    {
        StopCoroutine(PatrolCD());
        _newTargetPosition = _player;
        _hasTarget = true;
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
        if (Vector2.Distance(transform.position, _player) <= _stopDistance)
        {
            _stopPlayerChasing = true;
        }
        else
        {
            _stopPlayerChasing = false;
        }

        if (Vector2.Distance(transform.position, _player) >= _maxChasingDistance && _hasTarget)
        {
            _hasTarget = false;
            StartCoroutine(PatrolCD());
        }
    }

    private void OnDamageReceived(float hp)
    {
        AttackOn();
    }

    private IEnumerator PatrolCD()
    {
        yield return new WaitForSeconds(_timeBetweenActions);
        NewPatrolPoint();
    }
}
