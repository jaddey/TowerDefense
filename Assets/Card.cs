using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Объявление нового класса Card, отнаследованного от MonoBehaviour
public class Card : MonoBehaviour {

    // Поле типа Sprite для хранения изображения карты
    public Sprite cardImage;

    // Метод, который устанавливает изображение карты
    public void SetCardImage (Sprite newImage) {
        cardImage = newImage;
    }
}
