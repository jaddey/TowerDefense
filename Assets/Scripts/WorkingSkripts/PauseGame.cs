using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    private float timeScaleBeforePause = 1f; // устанавливаем значение по умолчанию
    public ImmortalityMode immortalityMode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (Time.timeScale == 1f) // проверяем, что время не ускорено
            {
                timeScaleBeforePause = Time.timeScale; // сохраняем предыдущее значение
                Time.timeScale *= 3f; // ускоряем время в 3 раза
            }
            else // если время уже ускорено, то возвращаем его в нормальное состояние
            {
                Time.timeScale = timeScaleBeforePause;
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            immortalityMode.SetImmortalityMode(!immortalityMode.isImmortal);
        }
    }

    private void Pause()
    {
        timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void Unpause()
    {
        Time.timeScale = timeScaleBeforePause;
        isPaused = false;
    }
}


