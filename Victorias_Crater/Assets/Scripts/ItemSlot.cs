using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler {


    [SerializeField] Image icon;
    MainItems _item;

    public event Action<MainItems> OnRightClickEvent; //event must ne triggered on right click
    public MainItems Item {
        get { return _item; }
        set {
            _item = value;
            if (_item == null) {
                icon.enabled = false;
            }
            else {
                icon.sprite = _item.icon;
                icon.enabled = true;
            }
        }
    }


    public void OnPointerClick(PointerEventData eventData) {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) { //test mechanich on equip by right button down
            
            if (Item != null && OnRightClickEvent != null) {
                Debug.Log("test");
                OnRightClickEvent(Item); //item that pressed
            }
        }
    }
    protected virtual void OnValidate() {
        if (icon == null) {
            icon = GetComponent<Image>();
        }
    }
    public void AddItem(MainItems newItem) {
        _item = newItem;
        icon.sprite = _item.icon;
        icon.enabled = true;
    }

    public void DropItem() {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;

        //check on itemtype and intantiate obj S
    }
    

}
