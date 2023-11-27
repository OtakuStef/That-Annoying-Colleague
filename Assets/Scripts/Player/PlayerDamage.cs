using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float playerHealth = 100;
    private float nextPossibleDamage = 0.0f;
    public float damageCooldown = 1.0f;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerDamagePossible(collision.gameObject))
        {

            float minMagnitude = PlayerManager.Instance.minPlayerDamageMagnitude;
            float collisionMagnitude = collision.relativeVelocity.magnitude;
            Debug.Log("Collision Velocity: " + collision.relativeVelocity);
            Debug.Log("Collision Magnitude: " + collisionMagnitude);

            if (collisionMagnitude > minMagnitude)
            {
                playerHealth -= calculatePlayerDamage(collision.gameObject, collisionMagnitude);
                
                Debug.Log("Player Health reduced to: " + playerHealth);
                healthBar.SetHealth(playerHealth);
            }
        }
    }

    private bool PlayerDamagePossible(GameObject collisionObject)
    {
        if (collisionObject.tag == ObjectManager.Instance.throwableObjectTag && 
            collisionObject.GetComponent<ObjectDurability>().spawnProtectionActive &&
            Time.time > nextPossibleDamage) 
        {
            nextPossibleDamage = Time.time + damageCooldown;
            return true; 
        }

        return false;
    }

    private float calculatePlayerDamage(GameObject collisionObject, float collisionMagnitude)
    {
        float damageMultiplicator = PlayerManager.Instance.playerDamageMultiplicator;
        float maxPossibleDamage = PlayerManager.Instance.maxPossiblePlayerDamage;
        float durabilityDamage = collisionObject.GetComponent<ObjectDurability>().currentDurability;

        float calculatedDamage = durabilityDamage * collisionMagnitude * damageMultiplicator;
        return Mathf.Clamp(calculatedDamage, 0.0f, maxPossibleDamage);
    }
}
