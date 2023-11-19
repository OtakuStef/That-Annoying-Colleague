using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public int MaxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public PowerUpUI powerUp;


    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
    }

 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            powerUp.SetImage("HEALTH");
        }
        if(Input.GetKeyDown(KeyCode.S)){
            powerUp.SetImage("DMG");
        }
        if(Input.GetKeyDown(KeyCode.D)){
            powerUp.SetImage("SPEED");
        }
        if(Input.GetKeyDown(KeyCode.F)){
            powerUp.SetImage("SHIELD");
        }
        if(Input.GetKeyDown(KeyCode.G)){
            powerUp.SetImage("NONE");
        }
        
    }

    void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}
