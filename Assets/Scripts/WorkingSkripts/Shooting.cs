using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float viewRadius = 10f;
    public float spreadFactor = 0;
    private float nextFireTime;
    public Transform targetEnemy;

    void Update()
    {
        if (targetEnemy == null)
        {
            targetEnemy = FindVisibleEnemy();
        }
        else
        {
            // Проверяем, видима ли цель перед нами
            Vector3 direction = targetEnemy.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, viewRadius))
            {
                if (hit.transform == targetEnemy)
                {
                    // Цель всё ещё видна, стреляем
                    if (Time.time >= nextFireTime)
                    {
                        Shoot();
                        nextFireTime = Time.time + 1f / fireRate;
                    }
                }
                else
                {
                    // Цель не видна, начинаем поиск заново
                    targetEnemy = null;
                }
            }
            else
            {
                // Цель не видна, начинаем поиск заново
                targetEnemy = null;
            }
        }
    }

    Transform FindVisibleEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Transform enemy = collider.transform;
                Vector3 direction = enemy.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < 45f)
                {
                    Ray ray = new Ray(transform.position, direction);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, viewRadius))
                    {
                        if (hit.transform.CompareTag("Enemy"))
                        {
                            return enemy;
                        }
                    }
                }
            }
        }

        return null;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 spread = new Vector3(Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor), 0);
        rb.AddForce((firePoint.forward + spread) * 100f);
    }
}
