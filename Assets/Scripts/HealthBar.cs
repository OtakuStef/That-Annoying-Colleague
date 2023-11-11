using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetHealth(int health){
        slider.value = health;
    }

    public void IncreaseHealth(int healthIncrease){
        slider.value = slider.value + healthIncrease;
    }

    public void SetMaxHealth(int health){
        slider.maxValue = health;
    }
}
