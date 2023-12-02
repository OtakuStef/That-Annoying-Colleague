using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{

    public string currentSprite;

    public Image HealthImg;
    public Image StrongImg;
    public Image ShieldImg;
    public Image SpeedImg;

    public void SetImage(string powerUP){

        if(powerUP == "HEALTH"){
            HealthImg.enabled = true;
            StrongImg.enabled = false;
            ShieldImg.enabled = false;
            SpeedImg.enabled = false;
        }
        if(powerUP == "DMG"){
            HealthImg.enabled = false;
            StrongImg.enabled = true;
            ShieldImg.enabled = false;
            SpeedImg.enabled = false;
        }
        if(powerUP == "SPEED"){
            HealthImg.enabled = false;
            StrongImg.enabled = false;
            ShieldImg.enabled = false;
            SpeedImg.enabled = true;
        }
        if(powerUP == "SHIELD"){
            HealthImg.enabled = false;
            StrongImg.enabled = false;
            ShieldImg.enabled = true;
            SpeedImg.enabled = false;
        }
        if(powerUP == "NONE"){
            StrongImg.enabled = false;
            HealthImg.enabled = false;
            ShieldImg.enabled = false;
            SpeedImg.enabled = false;
        }

        currentSprite = powerUP;
        
    }

    public void ResetImage()
    {
        SetImage("NONE");
    }


    void Start()
    {
        SetImage("NONE");
    }

}
