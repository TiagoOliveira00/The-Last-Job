using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    private List<Card.CardType> cardList;

    private void Awake()
    {
        cardList = new List<Card.CardType>();
    }
    public void AddCard(Card.CardType cardType)
    {
        cardList.Add(cardType);
    }

    public void RemoveCard(Card.CardType cardType)
    {
        cardList.Remove(cardType);
    }

    public bool ContainsCard(Card.CardType cardType)
    {
        return cardList.Contains(cardType);
    }

    private void OnTriggerEnter(Collider col)
    {
        Card card = col.GetComponent<Card>();

        if (card != null)
        {
            AddCard(card.GetCardType());
        }

        CardDoor cardDoor = col.GetComponent<CardDoor>();

        if (cardDoor != null)
        {
            if (ContainsCard(cardDoor.GetCardType()))
            {
                RemoveCard(cardDoor.GetCardType());
                cardDoor.OpenDoor();
            }
        }
    }
}
