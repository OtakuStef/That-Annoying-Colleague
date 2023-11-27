using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetHealth(float health){
        slider.value = health;
    }

    public void IncreaseHealth(float healthIncrease){
        slider.value = slider.value + healthIncrease;
    }

    public void SetMaxHealth(float health){
        slider.maxValue = health;
    }
}
