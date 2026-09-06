using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform target;
    public int health = 100;
    public int coinsOnDeath = 10; // количество монеток, которые получит игрок при убийстве врага
    public float lifeTime = 5.0f;
    public float noiseFrequency = 0.1f; // частота шума
    public float noiseAmplitude = 1f; // амплитуда шума
    public float minSpeed = 1f; // минимальная скорость
    public float maxSpeed = 5f; // максимальная скорость
    private float speed;
    NavMeshAgent agent;

    void Start()
    {
        target = GameObject.FindWithTag("Target").transform;
        agent = GetComponent<NavMeshAgent>();
        Invoke("DestroyAfterTime", lifeTime);
        speed = Random.Range(minSpeed, maxSpeed); // задаем случайную скорость в диапазоне между minSpeed и maxSpeed
        agent.speed = speed; // устанавливаем скорость для NavMeshAgent
    }

    void Update()
    {
        // Генерируем случайное направление на основе карты шума
        Vector3 noiseDirection = new Vector3(
            Mathf.PerlinNoise(Time.time * noiseFrequency, 0) * noiseAmplitude * 2 - noiseAmplitude,
            0,
            Mathf.PerlinNoise(0, Time.time * noiseFrequency) * noiseAmplitude * 2 - noiseAmplitude
        );

        // Нормализуем направление и устанавливаем его в качестве пункта назначения для NavMeshAgent
        agent.destination = target.position + noiseDirection.normalized;

        // Обновляем позицию агента вручную
        agent.nextPosition = transform.position;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // увеличиваем количество монеток в ResourceManager
        ResourceManager.GetResourceManager().AddCoins(coinsOnDeath);

        // уничтожаем объект
        Destroy(gameObject);
    }

    void DestroyAfterTime()
    {
        // уничтожаем объект
        Destroy(gameObject);
    }
}



