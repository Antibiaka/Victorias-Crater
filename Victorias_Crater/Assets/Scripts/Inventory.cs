using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

    [SerializeField] List<MainItems> items; //list of item in inv
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots; //list of slots
    Inventory inventory;

    public event Action<ItemSlot> OnRightClickEvent; //events that serving mouse activity
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> ItemDragEvent;
    public event Action<ItemSlot> ItemDropEvent;
    public event Action<ItemSlot> OnEndDragEvent;


    #region Singelton
    public static Inventory instance; //create a static variable shared by all instance of class 

    private void Awake() {
        UpdateInvUI();
        if (instance != null) {
            Debug.Log("Inventory dublicate");
            return;
        }
        instance = this; // let to acess this invetory component
    }
    #endregion


    private void Start() {
        for (int i = 0; i < itemSlots.Length; i++) {
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].ItemDragEvent += ItemDragEvent;
            itemSlots[i].ItemDropEvent += ItemDropEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
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

    void UpdateInvUI() { //Updating inventory on pickup/drop
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

    public bool AddItem(MainItems item) { //method that check inventory  for free slot where we will put picking item
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == null) {
                itemSlots[i].Item = item;
                return true;
            }
        }
        if (onIvnentoryChangeCallback != null) {
            onIvnentoryChangeCallback.Invoke(); //calling update of inventory
                                                //cases*
        }

        return false;
    }
    public bool RemoveItem(MainItems item) { //method that check inventory slot have an item that we need to  remove
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == item) {
                itemSlots[i].Item = null;
                return true;
            }
        }
        //add method that updating an inventory

        //if (items.Remove(item)) {
        //    UpdateInvUI();
        //    return true;
        //}
        return false;
    }

    public bool IsInvFull() { //method that cheking our inventory on empty space
        for (int i = 0; i < itemSlots.Length; i++) {
            if (itemSlots[i].Item == null) { //if even one empty slot are present
                return false;
            }
        }
        return true;
    }

}
