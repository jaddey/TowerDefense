using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private ResourceManager resourceManager;
    public int Coins;

    private void Start()
    {
        // Ищем объект с компонентом ResourceManager в сцене
        GameObject managerObject = GameObject.FindWithTag("Target");
        if (managerObject != null)
        {
            // Получаем ссылку на компонент ResourceManager на найденном объекте
            resourceManager = managerObject.GetComponent<ResourceManager>();
        }
        else
        {
            Debug.LogError("Unable to find an object with the tag 'ResourceManager'");
        }
    }

    public void AddCoins()
    {
        // Вызываем метод AddCoins у ResourceManager
        if (resourceManager != null)
        {
            resourceManager.AddCoins(Coins);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Unable to add coins: ResourceManager is not set");
        }
    }
}


