using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectDurability : MonoBehaviour
{
    public float maxDurabilty;
    public float currentDurability;
    private GameObject asset;
    public float damageCooldown = 2.0f;
    private float nextPossibleDamage = 0.0f;
    public bool spawnProtectionActive = true;
    public int spawnProtectionDuration = 4;


    private void Awake()
    {
        currentDurability = maxDurabilty;
    }

    // Start is called before the first frame update
    void Start()
    {
        asset = this.gameObject;
        StartCoroutine(removeSpawnProtection());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDurability <= 0) {
            StartCoroutine(objectDestruction());
        }
    }

    private void OnDestroy()
    {
        System.Console.WriteLine("Object destroyed");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time > nextPossibleDamage && !spawnProtectionActive)
        {
            nextPossibleDamage = Time.time + damageCooldown;

            if (collision.gameObject.tag == ObjectManager.Instance.throwableObjectTag)
            {
                float minMagnitude = ObjectManager.Instance.minDamageMagnitude;
                float collisionMagnitude = collision.relativeVelocity.magnitude;
                float duarbilityCollisionObject = collision.gameObject.GetComponent<ObjectDurability>().currentDurability;

                if (collisionMagnitude >= minMagnitude)
                {
                    float caluclatedDamage = calculateObjectDamage(collisionMagnitude, duarbilityCollisionObject);
                    Debug.Log("Dmg of coll between " + this.name + " and " + collision.gameObject.name + ": " + caluclatedDamage);
                    this.currentDurability -= caluclatedDamage;
                }
            }
            else if (collision.gameObject.tag == "Player")
            {
                this.currentDurability -= calculatePlayerDamage();
            }
        }
              
    }


    private float calculateObjectDamage(float collisionMagnitude, float durabilityCollisionObject)
    {
        float damageMultiplicator = ObjectManager.Instance.damageMultiplicator;
        float maxPossibleDamage = ObjectManager.Instance.maxPossibleDamage;
        float durabilityDamageFactor = currentDurability / durabilityCollisionObject;
        float calculatedDamage = durabilityDamageFactor * collisionMagnitude * damageMultiplicator;
        return Mathf.Clamp(calculatedDamage, 0.0f, maxPossibleDamage);
    }

    private float calculatePlayerDamage()
    {
        return maxDurabilty*0.35f; //Player-Collision should always reduce durability about half of max durability
    }

    private IEnumerator objectDestruction()
    {
        Debug.Log("Destroy: " + asset.name);
        Destroy(asset);
        //Play destruction sound
        yield return null;
    }

    private IEnumerator removeSpawnProtection()
    {
        yield return new WaitForSeconds(spawnProtectionDuration);
        this.spawnProtectionActive = false;
    }
}
