using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpController : MonoBehaviour
{
    [SerializeField]
    private Camera camera; // to avoid camera.maim
    public LayerMask itemLayer; //layer of our items
    private float pickUpTime = 2f;
    [SerializeField]
    private Image pickProgress;
   
    //add item text and maybe image while pick up

    [SerializeField]
    private TextMeshProUGUI textname;


    private Item itemPicked;
    private float currentTime;// tracking time to pick up item 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        currentTime += Time.deltaTime;
        if (currentTime >= pickUpTime) {
            Destroy(itemPicked.gameObject);
        }
    }
    private void UpdatePickupProgressImage() {
        float pct = currentTime / pickUpTime;
        pickProgress.fillAmount = pct;
    }
    private bool HasItemTargetted() {
        return itemPicked != null;
    }
    void RayCheckedItem() {
        Ray ray = camera.ViewportPointToRay(Vector3.one / 2f); // vector dir 0.5 of all vaalues

        Debug.DrawRay(ray.origin, ray.direction * 11f, Color.red);

        RaycastHit hitInfo;
        
        if(Physics.Raycast(ray , out hitInfo , 11f, itemLayer)) {  // less  10f away , on itemLayer 

            var hitItem = hitInfo.collider.GetComponent<Item>(); //check if our item in ray is 

            
            if (hitItem == null) {
                itemPicked = null;
            }
            else if (hitItem != null && hitItem != itemPicked) {
                Debug.Log(hitItem);
                itemPicked = hitItem;
                textname.text = "PickUp " + itemPicked.gameObject.name;
            }
        }
        else {
            itemPicked = null;
            textname.text = " ";
        }
    }
}
