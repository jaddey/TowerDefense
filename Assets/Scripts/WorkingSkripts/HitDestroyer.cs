using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDestroyer : MonoBehaviour
{
    public float time = 0.7f;
    void Start()
    {
        // Уничтожаем объект через 0,7 секунды после его создания
        Destroy(gameObject, time);
    }
}
