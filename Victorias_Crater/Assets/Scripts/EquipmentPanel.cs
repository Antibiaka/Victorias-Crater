using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour {
    [SerializeField] Transform equipmentParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<ItemSlot> OnRightClickEvent; //events that serving mouse activity
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> ItemDragEvent;
    public event Action<ItemSlot> ItemDropEvent;
    public event Action<ItemSlot> OnEndDragEvent;

  
    private void Start() {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            equipmentSlots[i].OnRightClickEvent += itemslot => OnRightClickEvent(itemslot);
            equipmentSlots[i].OnBeginDragEvent += itemslot => OnBeginDragEvent(itemslot);
            equipmentSlots[i].OnEndDragEvent += itemslot => OnEndDragEvent(itemslot);
            equipmentSlots[i].ItemDragEvent += itemslot => ItemDragEvent(itemslot);
            equipmentSlots[i].ItemDropEvent += itemslot => ItemDropEvent(itemslot);
        }
    }
    private void OnValidate() {
        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>(); // find all childs all our item slots
    }

    public bool AddItem(EquipableItem item, out EquipableItem itemInSlot) {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].EquipmentType == item.EquipmentType) { //find a slot with tipy of our item
                itemInSlot = (EquipableItem)equipmentSlots[i].Item;//previous item if it was at slot
                equipmentSlots[i].Item = item;
                return true;
                
            }
        }
        itemInSlot = null;
        return false;
    }
    public bool DeleteItem(EquipableItem item) {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            if (equipmentSlots[i].Item == item) { //find non empty  spot
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
