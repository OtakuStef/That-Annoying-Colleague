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
            powerUp.SetImage("ENERGY");
        }
        
    }

    void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}
