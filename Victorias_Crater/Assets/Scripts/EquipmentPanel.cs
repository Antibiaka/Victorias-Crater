using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour {
    [SerializeField] Transform equipmentParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<MainItems> ItemRightClickEvent;
    private void OnValidate() {
        equipmentSlots = equipmentParent.GetComponentsInChildren<EquipmentSlot>(); // find all childs all our item slots
    }
    private void Awake() {
        for (int i = 0; i < equipmentSlots.Length; i++) {
            equipmentSlots[i].OnRightClickEvent += ItemRightClickEvent;
        }
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
