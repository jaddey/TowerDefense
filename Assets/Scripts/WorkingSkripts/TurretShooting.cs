using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Transform firePoint; // Точка, из которой летят пули 
    public float fireRate = 600f; // Количество выстрелов в минуту
    public float bulletSpread = 1f; // Насколько разбросаны пули
    public float maxDistance = 100f; // Максимальная дистанция стрельбы
    public int damage = 10; // Урон, наносимый каждым выстрелом
    public GameObject hitBlood; // Эффект крови, когда пуля попадает во врага
    public GameObject hitDust; // Эффект песка, когда пуля попадает в нечто другое
    public GameObject bulletPrefab; // Префаб для эффекта полета снаряда
    public GameObject sleeve; // Префаб эффекта гильз
    public GameObject flash; // Префаб эффекта выстрела
    public int bulletFlightPrefabInterval = 5; // Интервал между выстрелами для отображения префаба полета снаряда
    public float bulletFlightAcceleration = 10f; // Скорость появления префаба полета снаряда

    private float nextFireTime = 0f; // Временной интервал между выстрелами
    private int shotCount = 0; //  Счетчик выстрелов

    void Update()
    {
        if (Time.time >= nextFireTime) // Если достигнут интервал времени
        {
            if (CanShoot()) // Проверяем, можно ли производить выстрел
            {
                Shoot(); // Выстреливаем
                float interval = 1f / (fireRate / 60f); // Расчет интервала времени между выстрелами
                nextFireTime = Time.time + interval; // Задание нового времени для следующего выстрела
            }
        }
    }

    bool CanShoot()
    {
        // Проверяем наличие врага в зоне обнаружения турели
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Vector3 direction = (collider.transform.position - transform.position).normalized; 
                // Вычисляем угол между направлением турели и направлением на противника
                float angle = Vector3.Angle(direction, transform.forward);

                // Если враг находится в пределах угла обзора, то можно стрелять
                if (angle < 45f)
                {
                    sleeve.SetActive(true);
                    flash.SetActive(true);
                    return true;
                }
            }
        }
        sleeve.SetActive(false);
        flash.SetActive(false);
        return false;
    }

    void Shoot()
    {
        shotCount++;

        int numOfRays = 5; // Количество снарядов за один выстрел (изменяемый аспект)

        // Создаем кучу лучей с разбросом
        Vector3[] directions = new Vector3[numOfRays];
        for (int i = 0; i < numOfRays; i++)
        {
            Vector3 direction = firePoint.forward;
            direction.x += Random.Range(-bulletSpread, bulletSpread);
            direction.y += Random.Range(-bulletSpread, bulletSpread);
            direction.z += Random.Range(-bulletSpread, bulletSpread);
            directions[i] = direction;
        }
        
        // Отображение префаба, если было осуществлено соответствующее количество выстрелов
        if (shotCount % bulletFlightPrefabInterval == 0) 
        {
            GameObject bulletFlightPrefabInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directions[0]));
            bulletFlightPrefabInstance.GetComponent<Rigidbody>().AddForce(directions[0] * bulletFlightAcceleration, ForceMode.Impulse);
        }

        foreach (Vector3 direction in directions)
        {
            RaycastHit hit;
            // Обнаружение столкновения луча с чем-либо
            if (Physics.Raycast(firePoint.position, direction, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                // Отобразить луч, если снаряд попал во врага
                Debug.DrawLine(firePoint.position, hit.point, Color.red, 0.1f);

                EnemyScript enemy = hit.transform.GetComponent<EnemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Instantiate(hitBlood, hit.point, Quaternion.identity);
                }
            }
            else
            {
                // Отобразить луч, если снаряд попал не во врага
                Debug.DrawLine(firePoint.position, hit.point, Color.yellow, 0.1f);
                Instantiate(hitDust, hit.point, Quaternion.identity);
            }
        }
        else
        {
            // Отобразить луч если снаярд никуда не попал
            Debug.DrawLine(firePoint.position, firePoint.position + direction * maxDistance, Color.white, 0.1f);
        }
    }
}
}

