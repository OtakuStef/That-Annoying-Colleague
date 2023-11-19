using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{

    public string currentSprite;

    public Image HealthImg;
    public Image EnergyImg;

    public void SetImage(string powerUP){

        if(powerUP == "HEALTH"){
            HealthImg.enabled = true;
            EnergyImg.enabled = false;
        }
        if(powerUP == "ENERGY"){
            HealthImg.enabled = false;
            EnergyImg.enabled = true;
        }
        if(powerUP == "NONE"){
            EnergyImg.enabled = false;
            HealthImg.enabled = false;
        }

        currentSprite = powerUP;
        
    }


    void Start()
    {
        currentSprite = "NONE";
        HealthImg.enabled = false;
        EnergyImg.enabled = false;
    }

}
