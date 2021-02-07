using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AirBar : MonoBehaviour 
{
    private Air air;
    public Image image;

    private void Awake() {
       // image = transform.Find("bartest").GetComponent<Image>();

        image.fillAmount = 1f;
        air = new Air();
    }

    private void Update() {
        air.Update();
        image.fillAmount = air.GetAirNormalized();
    }
   
}

public class Air {
    public const int airMaxAmount  = 100;

    public static float airAmount;
    private float airUsing;

    public Air() {
        airAmount = 100;
        airUsing = 10f;
    }

    public void Update() {
        if (airAmount>0) {
            airAmount -= airUsing * Time.deltaTime/4;
        }
        else {
           //dieis
        }
    }

    public void RestoreAir(int amount) { // in future restore amount of air using items etc
        if (airAmount < airMaxAmount) {
            airAmount += amount;
        }
    }

    public float GetAirNormalized() {
        return airAmount / airMaxAmount;
    }
}