using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public CardsList cardsList; // ссылка на ScriptableObject, содержащий список карт
    public List<Card> cardList = new List<Card>(); // список объектов Card
    
    void Start()
    {
        if (cardsList != null)
        {
            cardList = new List<Card>(cardsList.cardList); // копирование списка из ScriptableObject в список деки
        }
        
        ShuffleCards();  // передаем список в метод перемешивания
    }
    
    void ShuffleCards()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            Card temp = cardList[i];
            int randomIndex = Random.Range(i, cardList.Count);
            cardList[i] = cardList[randomIndex];
            cardList[randomIndex] = temp;
        }
    }
}



