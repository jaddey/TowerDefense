using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base : MonoBehaviour
{
    //[SerializeField]
    //private int health = 1000;
    public int health = 1000;
    public ImmortalityMode immortalityMode;
    public HealthBar healthBar;
    public GameObject gameOverScreen;

    public void TakeDamage(int damage)
{
    if (!GetComponent<ImmortalityMode>().isImmortal) // Если режим бессмертия не включен
    {
        health -= damage;
        healthBar.UpdateHealthBar(health);

        if (health <= 0)
        {
            GameOver();
        }
    }
}
    private void GameOver()
    {
        Time.timeScale = 0f; // Ставим время на паузу
        gameOverScreen.SetActive(true); // Включаем объект, который нужно показать при завершении игры
    }
}




