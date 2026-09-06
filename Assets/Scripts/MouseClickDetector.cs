using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetector : MonoBehaviour
{
    public Camera mainCamera;


    private void Update()
    {
        // Проверяем клик мыши каждый кадр
        if (Input.GetMouseButtonDown(0))
        {
            // Создаем луч из камеры в точку клика мыши
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Проверяем, что луч попал на объект с тегом Coin
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.CompareTag("Coin"))
            {
                // Получаем скрипт Coin на объекте
                Coin coin = hit.collider.GetComponent<Coin>();

                // Если скрипт найден, вызываем метод AddCoins
                if (coin != null)
                {
                    coin.AddCoins();
                }
            }
        }
    }
}

