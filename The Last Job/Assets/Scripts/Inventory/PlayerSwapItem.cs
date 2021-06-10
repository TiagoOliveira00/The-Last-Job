using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapItem : MonoBehaviour
{
    public PickUp obj;
    public void SetItemType(string item)
    {
        foreach (GameObject i in obj.intZoneList)
        {
            if (i.name == item)
            {
                i.SetActive(true);
            }
            else i.SetActive(false);
        }
    }
}
