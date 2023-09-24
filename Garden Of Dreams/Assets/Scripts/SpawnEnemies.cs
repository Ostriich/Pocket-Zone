using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();

    [SerializeField] private float _countPerSpawn;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private float _spawnCooldown;

    // Spawn enemies once in a while
    private void FixedUpdate()
    {
        _spawnCooldown += Time.deltaTime;

        if (_spawnCooldown >= _timeSpawn)
        {
            Spawn();
            _spawnCooldown = 0;
        }
    }

    // Spawn random enemies at one of the set points
    private void Spawn()
    {
        for (int i = 0; i < _countPerSpawn; i++)
        {
            GameObject spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            GameObject enemy = _enemies[Random.Range(0, _enemies.Count)];

            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
        }
    }
}
