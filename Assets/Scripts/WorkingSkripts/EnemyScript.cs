using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyScript : MonoBehaviour
{
    // Настройки врага
    public int health = 100;
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float waypointReachRadius = 3f; // Радиус срабатывания waypoint

    // Урон по базе
    public int damageToBase = 10; // Урон, который наносит враг при достижении цели

    // Выпадение монет
    public GameObject coinPrefab;
    [Range(0, 100)]
    public float coinDropChance = 50f; // Вероятность выпадения монеты (%)

    // Waypoints и цель
    private List<List<Transform>> waypointRows;
    private Transform target;
    private NavMeshAgent agent;
    private int currentRowIndex = 0;
    private int currentWaypointIndex = 0;
    private bool hasDealtDamage = false; // Флаг, чтобы наносить урон только один раз

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(minSpeed, maxSpeed);
        agent.stoppingDistance = waypointReachRadius;
        SetNextWaypoint();
    }

    void Update()
    {
        // Проверяем, дошли ли до текущей цели
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            // Если цель — конечная (target), то наносим урон и взрываемся
            if (currentRowIndex >= waypointRows.Count && !hasDealtDamage)
            {
                DealDamageToBase();
                hasDealtDamage = true;
                Die();
            }
            else
            {
                SetNextWaypoint();
            }
        }
    }

    // Установка waypoints и цели (вызывается из NewEnemySpawner)
    public void SetWaypoints(List<List<Transform>> rows, Transform finalTarget)
    {
        waypointRows = rows;
        target = finalTarget;
    }

    // Установка следующей цели движения
    void SetNextWaypoint()
    {
        // Если рядов нет или мы уже прошли все ряды — идем к цели
        if (waypointRows == null || waypointRows.Count == 0 || currentRowIndex >= waypointRows.Count)
        {
            agent.SetDestination(target.position);
            return;
        }

        // Если текущий ряд — последний, идем к цели
        if (currentRowIndex == waypointRows.Count - 1)
        {
            agent.SetDestination(target.position);
            currentRowIndex++;
            return;
        }

        // Переходим к следующему ряду
        currentRowIndex++;
        currentWaypointIndex = Random.Range(0, waypointRows[currentRowIndex].Count);

        // Добавляем случайное смещение от waypoint
        Vector3 randomOffset = new Vector3(
            Random.Range(-1.5f, 1.5f),
            0,
            Random.Range(-1.5f, 1.5f)
        );

        // Проверяем, что новая позиция находится на NavMesh
        NavMeshHit hit;
        Transform targetWaypoint = waypointRows[currentRowIndex][currentWaypointIndex];
        if (NavMesh.SamplePosition(targetWaypoint.position + randomOffset, out hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        else
        {
            agent.SetDestination(targetWaypoint.position);
        }
    }

    // Нанесение урона по базе (использует твой Base.cs)
    void DealDamageToBase()
    {
        Base baseComponent = target.GetComponent<Base>();
        if (baseComponent != null)
        {
            baseComponent.TakeDamage(damageToBase);
        }
        else
        {
            Debug.LogWarning("Base component not found on target!");
        }
    }

    // Получение урона
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Смерть врага (с выпадением монет)
    void Die()
    {
        // Выпадение монеты с заданной вероятностью
        if (coinPrefab != null && Random.Range(0f, 100f) < coinDropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        // Уведомляем спавнер о смерти
        NewEnemySpawner spawner = FindObjectOfType<NewEnemySpawner>();
        if (spawner != null)
        {
            spawner.OnEnemyDied();
        }

        Destroy(gameObject);
    }
}