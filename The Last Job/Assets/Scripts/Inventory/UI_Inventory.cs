using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TreeEditor;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Transform itemSlotCont;
    private Transform itemSlotTemp;

    private void Awake()
    {
        itemSlotCont = transform.Find("Slot");
        itemSlotTemp = itemSlotCont.Find("SlotTemp");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInv();
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        RefreshInv();
    }

    private void RefreshInv()
    {
        //To erase dropped items on inv
        foreach (Transform child in itemSlotCont)
        {
            if (child == itemSlotTemp) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotSize = 70f;

        // To add items sprites to the inv and refresh it
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRT = Instantiate(itemSlotTemp, itemSlotCont).GetComponent<RectTransform>();
            itemSlotRT.gameObject.SetActive(true);
            itemSlotRT.anchoredPosition = new Vector2(x * itemSlotSize * 3, y * itemSlotSize);

            Image image = itemSlotRT.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;

            // Might add something here
        }
    }
}
