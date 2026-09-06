using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMeshRenderer : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    private float minDelay = 0.01f;
    private float maxDelay = 0.20f;

    private void Start()
    {
        // Вычисляем случайный промежуток времени
        float delay = Random.Range(minDelay, maxDelay);
        Invoke("EnableRenderer", delay);
    }

    private void EnableRenderer()
    {
        meshRenderer.enabled = true;
    }
}

