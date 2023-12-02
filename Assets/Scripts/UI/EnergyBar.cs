using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetEnergy(float energy){
        slider.value = energy;
    }

    public void IncreaseEnergy(float energyIncrease){
        slider.value = slider.value + energyIncrease;
    }

    public void SetMaxEnergy(float energy){
        slider.maxValue = energy;
    }
}
