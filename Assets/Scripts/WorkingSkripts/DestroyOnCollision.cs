using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Проверяем столкновение с любым другим объектом
        Destroy(gameObject);
    }
}

