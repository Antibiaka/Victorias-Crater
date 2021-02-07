using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class EnergyBar : MonoBehaviour {

    private Energy energy;
    public Image image;

    private void Awake() {
        image.fillAmount = 0.842f;
        energy = new Energy();
    }

    private void Update() {
        energy.Update();
        image.fillAmount = energy.GetEnergyNormalized();
        if (image.fillAmount < 0.174) {
            image.fillAmount = 0f;
        }
    }

}

public class Energy {
    public const int energyMaxAmount = 1000;
    public static float energyAmount;
    public static float newEnergyAmount ;
    

    public Energy() {
        energyAmount = 842;
        newEnergyAmount = 842;
       
    }

    public void Update() {
        energyAmount = newEnergyAmount;
    }
    public static void UsedEnergy(int amount) { // in future restore amount of air using items etc
        if (energyAmount <= energyMaxAmount) {
            energyAmount -= amount;
            newEnergyAmount = energyAmount;
        }
    }


    public void RestoreEnergy(int amount) { // in future restore amount of air using items etc
        if (energyAmount < energyMaxAmount) {
            energyAmount += amount;
        }
    }

    public float GetEnergyNormalized() {
        return energyAmount / energyMaxAmount;
    }
}
