using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 _spawnPosition;

    private GameObject _spawnObject;

    private void Spawn ()
    {
        float x = Random.Range(_spawnPosition.x, _spawnPosition.y);
        float y = Random.Range(_spawnPosition.x, _spawnPosition.y);

        Instantiate(_spawnObject, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void SpawnerCycle(GameObject spawnObject, int spawnAmount)
    {
        _spawnObject = spawnObject;
        for (int i = 0; i < spawnAmount; i++)
        {
            Spawn();
        }
    }
}
