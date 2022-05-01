using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerTank _playerTank;

    private void Start()
    {
        _playerTank = FindObjectOfType<PlayerTank>();
    }

    private void LateUpdate()
    {
        if (_playerTank != null)
        {
            transform.position = new Vector3(_playerTank.transform.position.x, _playerTank.transform.position.y, transform.position.z);
        }
    }
}
