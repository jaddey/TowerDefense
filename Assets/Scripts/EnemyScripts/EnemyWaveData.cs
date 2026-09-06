using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyWaveData", menuName = "Enemy/WaveData", order = 1)]
public class EnemyWaveData : ScriptableObject
{
    public List<EnemyData> enemies;
    public float spawnDelay;
}

[System.Serializable]
public struct EnemyData
{
    public GameObject EnemyPrefab;
    public int Health;
    public float LifeTime;

    public EnemyData(GameObject enemyPrefab, int health, float lifeTime)
    {
        EnemyPrefab = enemyPrefab ?? throw new System.ArgumentNullException(nameof(enemyPrefab), "Enemy prefab cannot be null.");
        Health = health;
        LifeTime = lifeTime;
    }
}



