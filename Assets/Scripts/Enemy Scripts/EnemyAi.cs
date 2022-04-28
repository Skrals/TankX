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

    [Header("Passive actions cooldown")]
    [SerializeField] private float _timeBetweenActions;

    [SerializeField] DebugPoint _debugPoint;

    private bool _newPointReady = false;

    void Start()
    {
        _playerTank = FindObjectOfType<PlayerTank>();
        StartCoroutine(Actions());
    }

    private void Update()
    {
        Move();
    }


    private void NewPatrolPoint()
    {
        float newX = Random.Range(_minRoamDistance, _maxRoamDistance);
        float newY = Random.Range(_minRoamDistance, _maxRoamDistance);

        _newPosition = new Vector2(newX, newY);

        Instantiate(_debugPoint.gameObject, _newPosition, Quaternion.identity);

        StopCoroutine(Actions());

        _newPointReady = false;
    }

    private void Move()
    {
        _roamDistance = Vector2.Distance(_newPosition, transform.position);

        if(_roamDistance>=1)
        {
            transform.position += ((Vector3)_newPosition - transform.position).normalized * _speed * Time.deltaTime;
            Rotation();
        }
        else
        {
            if (!_newPointReady)
            {
                _newPointReady = true;
                StartCoroutine(Actions());
            }
        }
    }

    private void Rotation ()
    {
        var angle = Vector2.Angle(Vector2.up, (Vector3)_newPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, transform.position.x > _newPosition.x ? angle : -angle), _rotationSpeed);
    }

    private void Search()
    {

    }

    private void Attack()
    {

    }

    private IEnumerator Actions()
    {
        yield return new WaitForSeconds(_timeBetweenActions);
        NewPatrolPoint();
    }
}
