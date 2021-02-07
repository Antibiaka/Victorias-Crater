using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour{
    public GameObject inventoryUI;
    public GameObject equipmentUI;

    [HideInInspector]
    public static bool isInventoryUse;


    void Update()
    {
        if ( Input.GetButtonDown("Inventory")) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            isInventoryUse = inventoryUI.activeSelf; //using inbentory proccess


        }
    }
   
}






   