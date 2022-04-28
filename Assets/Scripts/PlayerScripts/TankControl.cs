using System.Collections;
using UnityEngine;


public class TankControl : MonoBehaviour
{
    [SerializeField] private DynamicJoystick _dynamicJoystick;
    [SerializeField] private FireCoolDown _fireCoolDown;
    [SerializeField] private FirePoint _firePoint;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private float _angle;

    private void Start()
    {
        _fireCoolDown = GetComponent<FireCoolDown>();
        _firePoint = GetComponentInChildren<FirePoint>();
    }

    private void Update()
    {
        if (_dynamicJoystick.isMoved)
        {
            TankRotation();
        }
    }

    private void Move()
    {
        transform.position += new Vector3(_dynamicJoystick.Direction.x, _dynamicJoystick.Direction.y, 0) * _speed * Time.deltaTime;
    }

    private void TankRotation()
    {
        _angle = Mathf.Atan2(_dynamicJoystick.Direction.y, _dynamicJoystick.Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, _angle - 90f), _rotationSpeed);

        Move();

    }

    public void FireButton()
    {
        if (!_fireCoolDown.GetCD())
        {
            Debug.Log("Fire");
            _firePoint.Shot();
            StartCoroutine(_fireCoolDown.FireCD());
        }
    }

}
