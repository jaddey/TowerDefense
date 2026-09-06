using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellRotator : MonoBehaviour
{
    public GameObject[] points;
    public GameObject closestPoint;

    void Start()
    {
        // Находим все объекты с тегом "point" и собираем их в массив
        points = GameObject.FindGameObjectsWithTag("Point");
        
        // Находим ближайший объект
        float shortestDistance = Mathf.Infinity;
        foreach (GameObject point in points)
        {
            float distance = Vector3.Distance(transform.position, point.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPoint = point;
            }
        }
        
        // Поворачиваем объект в сторону ближайшей точки с шагом в 60 градусов
        Vector3 direction = (closestPoint.transform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        targetAngle = Mathf.Round(targetAngle / 60) * 60; // Округляем до ближайшего угла, кратного 60 градусам
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }
}

