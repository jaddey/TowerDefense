using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public float turnSpeed = 10f;
    public Transform turretHeadY;
    public Transform turretHeadX;
    private Quaternion originalRotationY;
    private Quaternion originalRotationX;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        originalRotationY = turretHeadY.rotation;
        originalRotationX = turretHeadX.localRotation;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            // Если цели нет, возвращаем турель в изначальное положение
            turretHeadY.rotation = Quaternion.Lerp(turretHeadY.rotation, originalRotationY, Time.deltaTime * turnSpeed * 0.3f);
            turretHeadX.localRotation = Quaternion.Lerp(turretHeadX.localRotation, originalRotationX, Time.deltaTime * turnSpeed * 2f);
            return;
        }

    Vector3 dir = target.position - transform.position;

    // Повернуть объект только по оси Y в направлении цели
    Quaternion targetRotationY = Quaternion.LookRotation(dir);
    Quaternion yRotation = Quaternion.Euler(0f, targetRotationY.eulerAngles.y, 0f);
    turretHeadY.rotation = Quaternion.Lerp(turretHeadY.rotation, yRotation, Time.deltaTime * turnSpeed);

    // Повернуть объект только по оси X в сторону цели
    Quaternion targetRotationX = Quaternion.LookRotation(dir);
    Quaternion xRotation = Quaternion.Euler(targetRotationX.eulerAngles.x, 0f, 0f);
    turretHeadX.localRotation = Quaternion.Lerp(turretHeadX.localRotation, xRotation, Time.deltaTime * turnSpeed * 10f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}





