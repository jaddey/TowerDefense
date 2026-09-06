using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill; // Ссылка на компонент заполнения прогрессбара
    public Base baseScript; // Ссылка на скрипт, отвечающий за здоровье персонажа

    private float maxHealth; // Полное здоровье персонажа

    private void Start()
    {
        maxHealth = baseScript.health; // Получаем полное здоровье из другого скрипта в старте
    }

    public void UpdateHealthBar(float currentHealth)
    {
        float fillAmount = currentHealth / maxHealth; // Вычисляем процентное отношение текущего здоровья к полному
        healthBarFill.fillAmount = fillAmount; // Устанавливаем fillAmount компонента заполнения прогрессбара
    }
}
