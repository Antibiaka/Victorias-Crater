using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler {


    [SerializeField] Image icon;
    MainItems _item;
    public event Action<ItemSlot> OnRightClickEvent; //events that serving mouse activity
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> ItemDragEvent;
    public event Action<ItemSlot> ItemDropEvent;
    public event Action<ItemSlot> OnEndDragEvent;

    Vector2 prevItemPos; //value  of position of item icon

    private Color baseColor = Color.white;
    private Color transparentColor = new Color(1, 1, 1, 0);
    public MainItems Item {
        get { return _item; }
        set {
            _item = value;
            if (_item == null) {
                icon.color = transparentColor;
            }
            else {
                icon.sprite = _item.icon;
                icon.color = baseColor;
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
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right) { //method of using items of type usable 
            //usable items events
            if (OnRightClickEvent != null) {
                OnRightClickEvent(this); //item that pressed
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
       // prevItemPos = icon.transform.position; //save the position of icon before move it
        if (OnBeginDragEvent != null) {
            OnBeginDragEvent(this);
        }
    }

    public void OnDrag(PointerEventData eventData) {
       // icon.transform.position = Input.mousePosition; //item icon follow mouse
        if (ItemDragEvent != null) {
            ItemDragEvent(this);
        }
        if (Item != null)
            icon.color = baseColor;
    }


    public void OnEndDrag(PointerEventData eventData) {
        //icon.transform.position = prevItemPos; //reset the position   
        if (OnEndDragEvent != null) {
            OnEndDragEvent(this);
        }
    }
    public void OnDrop(PointerEventData eventData) {
        if (ItemDropEvent != null) {
            ItemDropEvent(this);
        }
    }

    public virtual bool CanGetItem(MainItems item) {
        return true;
    }
}
