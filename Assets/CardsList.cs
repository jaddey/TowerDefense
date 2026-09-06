using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewCardsList", menuName = "Custom/Cards List")]
public class CardsList : ScriptableObject
{
    public List<Card> cardList = new List<Card>();
}

