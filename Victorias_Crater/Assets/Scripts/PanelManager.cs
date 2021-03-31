using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] Image itemDragg;
    private ItemSlot draggedSlot;
    private void Awake() {
        //Right click event for equip/unequip items
        inventory.OnRightClickEvent += EquipItem;
        equipmentPanel.OnRightClickEvent += UnequipItem;
        //Pointer enter events ?params of gear
        //Pointer exit item -//-
        //Begin drag
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //Items Drag
        inventory.ItemDragEvent += ItemDrag;
        equipmentPanel.ItemDragEvent += ItemDrag;
        //Items Drop
        inventory.ItemDropEvent += ItemDrop;
        equipmentPanel.ItemDropEvent += ItemDrop;
        //End drag
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
    }


    private void EquipItem(ItemSlot itemSlot) {
        EquipableItem equipable = itemSlot.Item as EquipableItem;
        if (equipable != null) {
            Equiping(equipable);
        }
    }
    private void UnequipItem(ItemSlot itemSlot) {

        EquipableItem equipable = itemSlot.Item as EquipableItem;
        if (equipable != null) {
            Unequip(equipable);
        }
    }

    private void BeginDrag(ItemSlot itemSlot) {
        if (itemSlot.Item != null) {
            draggedSlot = itemSlot;
            itemDragg.sprite = itemSlot.Item.icon;
            itemDragg.transform.position = Input.mousePosition;
            itemDragg.enabled = true;
        }
    }
    private void ItemDrag(ItemSlot itemSlot) {
        if (itemDragg.enabled) {
            itemDragg.transform.position = Input.mousePosition;
        }
    }
    private void EndDrag(ItemSlot itemSlot) {
        draggedSlot = null;
        itemDragg.enabled = false;
    }
    private void ItemDrop(ItemSlot itemSlot) {
        Debug.Log("trying");
        if (itemSlot.CanGetItem(draggedSlot.Item) && draggedSlot.CanGetItem(itemSlot.Item)) {
             
        }
        EquipableItem dragItem = draggedSlot.Item as EquipableItem;
        EquipableItem dropItem = itemSlot.Item as EquipableItem;

        if (itemSlot is EquipmentSlot) {
            if (dragItem != null) dragItem.Unequip(this);
            if (dropItem != null) dropItem.Equip(this);
        }
        if (draggedSlot is EquipmentSlot) {
            if (dragItem != null) dragItem.Equip(this);
            if (dropItem != null) dropItem.Unequip(this);
        }

        MainItems draggedItem = draggedSlot.Item;
        draggedSlot.Item = itemSlot.Item;
        itemSlot.Item = draggedItem;
    }

    public void Equiping(EquipableItem itemSlot) {
        if (inventory.RemoveItem(itemSlot)) {
            EquipableItem itemInSlot;
            if (equipmentPanel.AddItem(itemSlot, out itemInSlot)) {
                if (itemInSlot != null) {
                    inventory.AddItem(itemInSlot);
                    itemInSlot.Unequip(this);
                }
                itemSlot.Equip(this);
            }
            else {
                Inventory.instance.AddItem(itemSlot);
            }
        }
    }

    public void Unequip(EquipableItem itemSlot) {
        if (!inventory.IsInvFull() && equipmentPanel.DeleteItem(itemSlot)) {
            itemSlot.Unequip(this);
            Inventory.instance.AddItem(itemSlot);
        }
    }
}
