using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using ThatAnnoyyingColleagueInput.PlayerControl;

public class PlayerDamage : MonoBehaviour
{
    public float playerHealth = 100;
    public float maxHealth = 100;
    private float nextPossibleDamage = 0.0f;
    public float damageCooldown = 1.0f;
    private bool isHealing = false;
    private float regenerationDuration = 0.0f;
    private float regeneration = 0.0f;
    private int regenerationCounter = 0;
    private float regenerationSteps = 2.0f;
    private float regenerationCheckCounter = 0.0f;

    // add audio
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<UIManager>().PlayerUI.healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        regenerationCheckCounter += Time.deltaTime;
        if (isHealing && regenerationCheckCounter > regenerationSteps)
        {
            regenerationCheckCounter = 0;
            StartCoroutine(heal());
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerDamagePossible(collision.gameObject))
        {
            // add sound on getting hit
            AudioClip gotHitSound = audioManager.got_hit_01;

            float minMagnitude = PlayerManager.Instance.minPlayerDamageMagnitude;
            float collisionMagnitude = collision.relativeVelocity.magnitude;

            if (collisionMagnitude > minMagnitude)
            {
                audioManager.PlaySFX(gotHitSound, false);
                playerHealth -= calculatePlayerDamage(collision.gameObject, collisionMagnitude);
                
                Debug.Log("Player Health reduced to: " + playerHealth);
                this.gameObject.GetComponent<UIManager>().PlayerUI.healthBar.SetHealth(playerHealth);
            }
        }
    }

    private bool PlayerDamagePossible(GameObject collisionObject)
    {
        try
        {
            if (collisionObject.tag == ObjectManager.Instance.throwableObjectTag &&
                !collisionObject.GetComponent<ObjectDurability>().spawnProtectionActive &&
                !this.gameObject.GetComponent<PlayerController>().getShieldStatus() &&
                Time.time > nextPossibleDamage)
            {
                nextPossibleDamage = Time.time + damageCooldown;
                return true;
            }
        }
        catch
        {
            return false;
        }
        return false;
    }


    private float calculatePlayerDamage(GameObject collisionObject, float collisionMagnitude)
    {
        float damageMultiplicator = PlayerManager.Instance.playerDamageMultiplicator;
        float maxPossibleDamage = PlayerManager.Instance.maxPossiblePlayerDamage;
        float damageBooster = getDamageBooster();
        float durabilityDamage = collisionObject.GetComponent<ObjectDurability>().currentDurability;

        float calculatedDamage = durabilityDamage * collisionMagnitude * damageMultiplicator * damageBooster;
        return Mathf.Clamp(calculatedDamage, 0.0f, maxPossibleDamage);
    }

    private float getDamageBooster()
    {
        float booster = 1.0f;
        if (this.gameObject.name == "Player1")
        {
            booster = PowerupManager.Instance.getPlayer2DamageBooster();
        }
        else if (this.gameObject.name == "Player2")
        {
            booster = PowerupManager.Instance.getPlayer1DamageBooster();
        }
        return booster;
    }

    public void regenerateHealth(float regeneration, float regernerationDuration)
    {
        this.regeneration = regeneration;
        this.regenerationDuration = regernerationDuration;
        this.isHealing = true;
    }

    private IEnumerator heal()
    {
        Debug.Log("Healing true for: " + regenerationCounter + " / " + regenerationDuration);
        if(this.regenerationCounter < this.regenerationDuration)
        {
            Debug.Log("Healing. Current HP: " + playerHealth);
            playerHealth = Mathf.Clamp(playerHealth + this.regeneration, 0.0f, maxHealth);
            this.gameObject.GetComponent<UIManager>().PlayerUI.healthBar.SetHealth(playerHealth);
            this.regenerationCounter += 1;
            yield return new WaitForSeconds(1);
        }
        else
        {
            this.isHealing=false;
            yield return null;
        }
       
    }
}
