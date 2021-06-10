using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Card1,
        Taser,
    }

    public ItemType itemType;

    // To get items sprites.
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case  ItemType.Card1: return ItemAssets.Instance.cardSprite;
            case  ItemType.Taser: return ItemAssets.Instance.taserSprite;
        }
    }

}
