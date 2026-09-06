using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
    {
        public float speed;
        public int damage;
        public Transform target;
         public float lifeTime = 5.0f;

        void Start()
    {
            Invoke("DestroyAfterTime", lifeTime);
    }

        private void Update()
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<EnemyScript>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else{   
                Destroy(gameObject);
            }
                
        }
        public void Seek()
        {
        if (target == null)
        {
            return;
        }
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            //TakeDamage();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
        }

        void DestroyAfterTime()
    {
        // Уничтожите объект
        Destroy(gameObject);
    }
    }
