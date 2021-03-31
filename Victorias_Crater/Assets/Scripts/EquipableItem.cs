using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    Helmet,
    AstronautSuit,
    Backpack,
    Boots,
    Accessory1,
    Accessory2,
}
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/EquipableItem")]
public class EquipableItem : MainItems {
    public int AirBonus;
    public int EnergyBonus;
    public int HealthBonus;
    public int SpeedBonus;
    public int JumpBonus;
    public int InventoryBonus;
    [Space]
    public float AirSpendReduction;
    public float EnergySpendReduction;
    public float HealthLosedReduction;
    public float AirRegeneration;
    [Space]
    public EquipmentType EquipmentType;
    public void Equip(PanelManager p) {

    }
    public void Unequip(PanelManager p) {

    }
}
