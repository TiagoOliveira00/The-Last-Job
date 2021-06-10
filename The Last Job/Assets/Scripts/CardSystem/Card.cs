using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardType cardType;

    public enum CardType
    {
        card1,
        card2,
        card3,
    }

    public CardType GetCardType()
    {
        return cardType;
    }
}
