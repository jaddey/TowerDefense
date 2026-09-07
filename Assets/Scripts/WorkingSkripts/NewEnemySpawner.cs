using UnityEngine;
using System.Collections.Generic;
using System;

public class NewEnemySpawner : MonoBehaviour
{
    // Префаб врага
    public GameObject enemyPrefab;

    // Массив рядов waypoints (отображается в инспекторе!)
    public WaypointRow[] waypointRows;

    // Финальная цель (база игрока)
    public Transform target;

    // Настройки спавна
    public float spawnInterval = 2f;
    public int maxEnemies = 20;

    private int currentEnemies = 0;
    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Создаём нового врага
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Получаем компонент EnemyScript
        EnemyScript enemyScript = newEnemy.GetComponent<EnemyScript>();

        if (enemyScript != null)
        {
            // Преобразуем WaypointRow[] в List<List<Transform>> для EnemyScript
            List<List<Transform>> rowsAsLists = new List<List<Transform>>();
            foreach (WaypointRow row in waypointRows)
            {
                rowsAsLists.Add(new List<Transform>(row.waypoints));
            }

            // Передаём waypoints и цель врагу
            enemyScript.SetWaypoints(rowsAsLists, target);
        }

        currentEnemies++;
    }

    // Метод для уменьшения счетчика врагов
    public void OnEnemyDied()
    {
        currentEnemies--;
    }
}


[Serializable] // Важно! Чтобы Unity мог отображать этот класс в инспекторе
public class WaypointRow
{
    public Transform[] waypoints; // Массив точек для этого ряда
}
