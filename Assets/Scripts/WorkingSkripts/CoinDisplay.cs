using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    public TextMeshProUGUI coinText; // компонент текста, в который будет отображаться количество монеток

    public void Refresh(int coins)
{
    // присваиваем значение переменной coins текстовому компоненту
    coinText.text = "Coins: " + coins.ToString();
}

}


