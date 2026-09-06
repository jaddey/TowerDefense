using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float damageInterval = 1f;
    public Base targetBase;

    public float raycastDistance = 5f;

    private float lastDamageTime = 0f;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                targetBase = hit.collider.gameObject.GetComponent<Base>();
            }
            else
            {
                targetBase = null;
            }
        }
        else
        {
            targetBase = null;
        }

        if (targetBase != null)
        {
            if (Time.time > lastDamageTime + damageInterval)
            {
                targetBase.TakeDamage(damageAmount);
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * raycastDistance);
    }
}




