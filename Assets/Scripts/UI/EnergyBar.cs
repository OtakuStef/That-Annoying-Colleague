using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;

    public void SetEnergy(int energy){
        slider.value = energy;
    }

    public void IncreaseEnergy(int energyIncrease){
        slider.value = slider.value + energyIncrease;
    }

    public void SetMaxEnergy(int energy){
        slider.maxValue = energy;
    }
}
