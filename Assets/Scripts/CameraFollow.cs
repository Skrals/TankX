using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerTank _playerTank;

    private void LateUpdate()
    {
        transform.position = new Vector3 (_playerTank.transform.position.x, _playerTank.transform.position.y, transform.position.z);
    }
}
