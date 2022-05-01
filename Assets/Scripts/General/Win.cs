using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private EnemyTank[] _enemies;
    [SerializeField] private List<EnemyTank> _enemyTanksList;

    private void Start()
    {
        _enemies = FindObjectsOfType<EnemyTank>();
        for (int i = 0; i < _enemies.Length; i++)
        {
            _enemyTanksList.Add(_enemies[i]);
        }
    }

    public void RemoveEnemy (EnemyTank enemy)
    {
        _enemyTanksList.Remove(enemy);

        WinCheck();
    }

    private void WinCheck()
    {
        if(_enemyTanksList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
