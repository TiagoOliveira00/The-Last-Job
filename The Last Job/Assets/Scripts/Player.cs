using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Inventory inventory;

    [SerializeField]
    private UI_Inventory uiInventory;

    public void Update()
    {
        //ansform.Translate(velocity);
        uiInventory = GetComponent<UI_Inventory>();
    }
    void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);


    }
}
