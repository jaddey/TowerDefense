using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector3 spawnArea;
    public float spawnInterval = 5f;
    public List<EnemyWave> enemyWaves;
    private float nextSpawnTime;

    private int currentWave = 0;
private int enemiesSpawned = 0;

private void Update()
{
    if (Time.time >= nextSpawnTime && currentWave < enemyWaves.Count)
    {
        nextSpawnTime = Time.time + enemyWaves[currentWave].spawnDelay;

        if (enemiesSpawned < enemyWaves[currentWave].enemies.Count)
        {
            Vector3 spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                0f,
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(enemyWaves[currentWave].enemies[enemiesSpawned], spawnPosition, Quaternion.identity);
            enemiesSpawned++;
        }
        else
        {
            currentWave++;
            enemiesSpawned = 0;
        }
    }
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2f);
    }
}

