using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    

    private void Awake() {
        inventory.ItemRightClickEvent += EquipItem; //listener to activate equip method
        equipmentPanel.ItemRightClickEvent += UnequipItem;
       
    }

    private void EquipItem(MainItems item) {
        if (item is EquipableItem) {
            Equiping((EquipableItem)item);
        } 
    }
    private void UnequipItem(MainItems item) {
        if (item is EquipableItem) {
            Unequip((EquipableItem)item);
        }
    }
    public void Equiping(EquipableItem item) {
        if (inventory.RemoveItem(item)) {
            EquipableItem itemInSlot;
            if (equipmentPanel.AddItem(item,out itemInSlot)) {
                if (itemInSlot!=null) {
                    inventory.AddItem(itemInSlot);
                }
            }
            else {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquipableItem item) {
        if(!inventory.InventorySpace()&& equipmentPanel.DeleteItem(item)) {
            inventory.AddItem(item);
        }
    }
    #region Tools
    //public void Equiping(Tools item) {
    //    if (inventory.RemoveItem(item)) {
    //        Tools itemInSlot;
    //        if (toolPanel.AddItem(item, out itemInSlot)) {
    //            if (itemInSlot != null) {
    //                inventory.AddItem(itemInSlot);
    //            }
    //        }
    //        else {
    //            inventory.AddItem(item);
    //        }
    //    }
    //}
    //public void Unequip(Tools item) {
    //    if (!inventory.InventorySpace() && toolPanel.DeleteItem(item)) {
    //        inventory.AddItem(item);
    //    }
    //}

    #endregion
}
