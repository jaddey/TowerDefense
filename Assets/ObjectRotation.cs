using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f; // Скорость вращения объекта через инспектор

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Вращаем объект по оси z
    }
}