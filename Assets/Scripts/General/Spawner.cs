using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] protected Vector2 _spawnPosition;
    public void Spawn (GameObject spawnObject, int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            float x = Random.Range(_spawnPosition.x, _spawnPosition.y);
            float y = Random.Range(_spawnPosition.x, _spawnPosition.y);

            Instantiate(spawnObject, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
