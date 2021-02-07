using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour {
    [SerializeField]
    private Camera camera; // to avoid camera.maim
    public LayerMask itemLayer; //layer of our items
    public LayerMask usingLayer; //layer of our items
    private float pickUpTime = 2f;
    [SerializeField]
    private Image pickProgress;

    //add item text and maybe image while pick up

    [SerializeField]
    private TextMeshProUGUI textname;

    [HideInInspector]
    public Item itemPicked;
    public Craft itemUsed;



    private float currentTime;// tracking time to pick up item 

    
    void Update() {
        RayCheckedItem();
        if (HasItemTargetted()) {
            pickProgress.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E)) {
                PickupProgress();
            }
            else {
                currentTime = 0f;
            }

            UpdatePickupProgressImage();
        }
        else {
            pickProgress.gameObject.SetActive(false);
            currentTime = 0f;
        }
    }
    private void PickupProgress() {
        currentTime += Time.deltaTime; //time to pick up item
        if (currentTime >= pickUpTime) {

            bool isFreeSlots = Inventory.instance.AddItem(itemPicked.items); //check if we have free space
            if (isFreeSlots) { 
                Debug.Log("Item Taken : " + itemPicked.name);
                Destroy(itemPicked.gameObject);
                Energy.UsedEnergy(76);
                currentTime = 0f;
            }
            
        }
    }
   
    private void UpdatePickupProgressImage() {
        float pct = currentTime / pickUpTime;
        pickProgress.fillAmount = pct; //filling image by time last to destroy obj
    }
    private bool HasItemTargetted() {
        return itemPicked != null;
    }
    void RayCheckedItem() {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f); // vector dir 0.5 of all vaalues

        Debug.DrawRay(ray.origin, ray.direction * 11f, Color.red);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 11f, itemLayer)) {  // less  10f away , on itemLayer 

            var hitItem = hitInfo.collider.GetComponent<Item>(); //check if our item is in ray  
            if (hitItem == null) {
                itemPicked = null;
            }
            else if (hitItem != null && hitItem != itemPicked) {
                itemPicked = hitItem;
                textname.text = "Pick Up " + itemPicked.gameObject.name;
            }
        }
        else {
            itemPicked = null;
            textname.text = " ";
        }
        //if (Physics.Raycast(ray, out hitInfo, 11f, usingLayer)) {  // less  10f away , on itemLayer 

        //    var hitItem = hitInfo.collider.GetCSomponent<Craft>(); //check if our item is in ray  
        //    if (hitItem == null) {
        //        itemUsed = null;
        //    }
        //    else if (hitItem != null && hitItem != itemUsed) {
        //        itemUsed = hitItem;
        //        textname.text = "Using " + itemUsed.gameObject.name;
        //    }
        //}
        //else {
        //    itemUsed = null;
        //    textname.text = " ";
        //}
    }
}
