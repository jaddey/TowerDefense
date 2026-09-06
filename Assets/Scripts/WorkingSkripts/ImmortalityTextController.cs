using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ImmortalityTextController : MonoBehaviour
{
    public TextMeshProUGUI text;
    public ImmortalityMode immortalityMode;

    void Start()
    {
        // Подписываемся на событие изменения состояния режима бессмертия
        immortalityMode.onImmortalityModeChanged.AddListener(OnImmortalityModeChanged);
    }

    void OnImmortalityModeChanged(bool isImmortal)
    {
        // Обновляем текст и цвет в зависимости от состояния режима бессмертия
        text.text = isImmortal ? "Бессмертен" : "Смертен";
        text.color = isImmortal ? Color.green : Color.red;
    }

    void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        immortalityMode.onImmortalityModeChanged.RemoveListener(OnImmortalityModeChanged);
    }
}


