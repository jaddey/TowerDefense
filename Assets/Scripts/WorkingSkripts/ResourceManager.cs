using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private static ResourceManager resourceManager;

    private int coins = 0;

public int Coins
{
    get { return coins; }
    set
    {
        coins = value;
        CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();
        if (coinDisplay != null)
        {
            coinDisplay.Refresh(coins);
        }
    }
}

void Awake()
{
    // сохраняем ссылку на экземпляр класса ResourceManager
    resourceManager = this;

    // обновляем текстовый компонент в начале игры
    CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();
    if (coinDisplay != null)
    {
        coinDisplay.Refresh(coins);
    }
}

// метод для получения монеток
public void AddCoins(int amount)
{
    coins += amount;

    // обновляем текстовый компонент
    CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();
    if (coinDisplay != null)
    {
        coinDisplay.Refresh(coins);
    }
}

// метод для траты монеток
public bool SpendCoins(int amount)
{
    if (coins >= amount)
    {
        coins -= amount;

        // обновляем текстовый компонент
        CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();
        if (coinDisplay != null)
        {
            coinDisplay.Refresh(coins);
        }

        return true;
    }
    else
    {
        return false;
    }
}

// статический метод для доступа к ResourceManager из другого скрипта
public static ResourceManager GetResourceManager()
{
    return resourceManager;
}

}

