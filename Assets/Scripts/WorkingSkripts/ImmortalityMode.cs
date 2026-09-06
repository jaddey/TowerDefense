using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImmortalityMode : MonoBehaviour
{
    public bool isImmortal = false; // Переменная, обозначающая включен или выключен режим бессмертия
    public UnityEvent<bool> onImmortalityModeChanged; // Событие, сообщающее состояние режима бессмертия

    private void Start()
    {
        Invoke("InvokeOnImmortalityModeChanged", 0.1f);
    }

    public void SetImmortalityMode(bool value)
    {
        isImmortal = value;
        onImmortalityModeChanged?.Invoke(isImmortal); // Вызываем событие, сообщающее об изменении состояния режима бессмертия
    }
    
    private void InvokeOnImmortalityModeChanged()
    {
        onImmortalityModeChanged?.Invoke(isImmortal);
    }
}


