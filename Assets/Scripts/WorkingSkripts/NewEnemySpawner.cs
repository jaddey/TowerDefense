using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemySpawner : MonoBehaviour
{
    // Определение переменных для области появления, задержки между спауном врагов, списка волн врагов
    public Vector3 spawnArea;
    public float spawnDelay = 0.5f;
    public EnemyWaveData waveList;
    
    // Счетчики количества созданных и необходимых к созданию врагов
    private int enemiesSpawned = 0;
    private int totalEnemiesToSpawn = 0;

    // Вызывается при запуске скрипта и устанавливает полное количество врагов для этой волны
    private void Start()
    {
        SetTotalEnemies();
    }

    // Вызывается каждый кадр.
    private void Update()
    {
        
    }

    // Рисует границы области спауна для врагов
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnArea * 2f);
    }

    // Устанавливает общее количество врагов для этой волны и использует InvokeRepeating для повторного вызова метода SpawnEnemy() с интервалом времени spawnDelay
    public void SetTotalEnemies()
    {
        totalEnemiesToSpawn = waveList.enemies.Count;
        if (totalEnemiesToSpawn == 0)
        {
            return;
        }
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    // Создает врага на случайной позиции внутри зоны появления врагов, если уже не создано всех необходимых врагов
    private void SpawnEnemy()
    {
        if (enemiesSpawned >= totalEnemiesToSpawn) // Проверьте, все ли враги больше не нужны, чтобы отменить InvokeRepeating() и вернуться из метода SpawnEnemy()
        {
            CancelInvoke("SpawnEnemy");
            return; 
        }

        // создаем врага
        GameObject enemy = Instantiate(waveList.enemies[enemiesSpawned].EnemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        // Получает доступ к скрипту врага
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();

        // Устанавливает данные о враге через метод SetData() в скрипте врага.
        EnemyData enemyData = waveList.enemies[enemiesSpawned];
        enemyScript.SetData(ref enemyData);

        // увеличиваем счетчик созданных врагов
        enemiesSpawned++;
    }

    // Возвращает векторную координату в пределах области спауна для врагов
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), transform.position.y, Random.Range(-spawnArea.z, spawnArea.z));
        return transform.TransformPoint(randomPos);
    }
}



