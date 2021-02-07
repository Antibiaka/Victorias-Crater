using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
   
    [SerializeField] List<MainItems> items; //list of item in inv
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots; //list of slots
    Inventory inventory;
    public event Action<MainItems> ItemRightClickEvent;


    #region Singelton
    public static Inventory instance; //create a static variable shared by all instance of class 
    
    private void Awake() {
       
        if (instance != null) {
            Debug.Log("Inventory dublicate");
            return;
        }
        instance = this; // let to acess this invetory component
    }
    #endregion
    
    
    private void Start() {
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].OnRightClickEvent += ItemRightClickEvent;
        }
        inventory = Inventory.instance;
        inventory.onIvnentoryChangeCallback += UpdateInvUI;
    }

    private void OnValidate() {
        if (itemsParent != null) {
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();  // find all childs all our item slots
        }
        UpdateInvUI();
    }

  
    public delegate void OnIvnentoryChange(); //create a delegate on item changed
    public OnIvnentoryChange onIvnentoryChangeCallback;

    // public List<MainItems> items = new List<MainItems>(); //create an empty list of items

    void UpdateInvUI() { //
        Debug.Log("Succes Inv Update");
        int i = 0;
        for (; i < items.Count && i < itemSlots.Length; i++) { //check on full inventory or free slot 
            itemSlots[i].Item = items[i];
            itemSlots[i].AddItem(inventory.items[i]);
        }
        for (; i < itemSlots.Length; i++) {
            itemSlots[i].Item = null;
        }

    }




    public bool AddItem(MainItems item) { // add return value true false to check on full
        if (!item.isResoure) {
            if (InventorySpace()) {
                Debug.Log("Not Enough room");
                return false;
            }
            items.Add(item);

            if (onIvnentoryChangeCallback != null) {
                onIvnentoryChangeCallback.Invoke(); //calling update of inventory
                //cases*
            }
        }
        return true;
    }
    public bool RemoveItem(MainItems item) { //can add check true false on item remove
        if (items.Remove(item)) {
            UpdateInvUI();
            return true;
        }
        return false;               
    }

    public bool InventorySpace() {
        return items.Count >= itemSlots.Length;
    }

}
