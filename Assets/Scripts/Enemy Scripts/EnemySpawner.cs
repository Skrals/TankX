using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private EnemyTank _enemyTankTemplate;
    [SerializeField] private int _enemyAmont;

    private void Awake()
    {
        Spawn(_enemyTankTemplate.gameObject, _enemyAmont);
    }
}
