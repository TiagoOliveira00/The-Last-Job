using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Item card1, taser;
    private Player p;

    public Transform interactionZone;
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public GameObject CardZone;

    public TypeOfCameras typeOfCameras;
    public Taser pickedTaser;

    public List<GameObject> intZoneList;

    public bool isPickable = true;

    private void Start()
    {
        p = GetComponent<Player>();
        typeOfCameras = FindObjectOfType<TypeOfCameras>();
    }

    // Update is called once per frame
    void Update()

    {
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<Colliders>().isPickable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<Colliders>().isPickable = false;
                PickedObject.transform.SetParent(interactionZone);
                intZoneList.Add(PickedObject);

                if (PickedObject.name == "Card1")
                {
                    card1 = new Item { itemType = Item.ItemType.Card1 };
                    p.inventory.AddItem(card1);
                    PickedObject.transform.position = CardZone.transform.position;

                    if (p.inventory.GetItemList().Count > 1)
                    {
                        PickedObject.gameObject.SetActive(false);
                    }
                }

                if (PickedObject.name == "Taser")
                {
                    taser = new Item { itemType = Item.ItemType.Taser };
                    p.inventory.AddItem(taser);
                    PickedObject.gameObject.SetActive(false);
                 
                    if (p.inventory.GetItemList().Count > 1)
                    {
                        PickedObject.gameObject.SetActive(false);
                    }

                    typeOfCameras.taserOnHand = true;
                }

                PickedObject.transform.forward = this.transform.forward;
                PickedObject.transform.rotation = this.transform.rotation;

                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                for (int c = 0; c < intZoneList.Count; c++)
                {
                    if (intZoneList[c].activeSelf)
                    {
                        intZoneList[c].GetComponent<Colliders>().isPickable = true;
                        intZoneList[c].transform.SetParent(null);

                        if (intZoneList[c].name == "Card1")
                         {
                             p.inventory.RemoveItem(card1);
                             intZoneList[c].transform.position = CardZone.transform.position;

                         }

                        if (intZoneList[c].name == "Taser")
                        {
                            p.inventory.RemoveItem(taser);
                            typeOfCameras.taserOnHand = false;
                        }

                        intZoneList[c].GetComponent<Rigidbody>().useGravity = true;
                        intZoneList[c].GetComponent<Rigidbody>().isKinematic = false;
                        intZoneList.Remove(intZoneList[c]);
                    }
                    else
                    {
                        intZoneList[c].SetActive(true);
                    }
                }
            }
        }

        foreach (GameObject g in intZoneList)
        {
            if (g.activeSelf)
            {
                if (g.CompareTag("Card"))
                {

                }
                else if (g.CompareTag("Taser"))
                {
    
                }
            }
        }
    }
}
