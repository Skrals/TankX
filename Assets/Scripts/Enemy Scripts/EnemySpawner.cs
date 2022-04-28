using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Spawner _spawner;
    [SerializeField] EnemyTank _enemyTankTemplate;
    [SerializeField] private int _enemyAmont;
    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _spawner.SpawnerCycle(_enemyTankTemplate.gameObject,_enemyAmont);
    }
}
