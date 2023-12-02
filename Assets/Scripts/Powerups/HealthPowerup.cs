using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class HealthPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    public AudioSource pickupSound;
    private float regenerationDuration = 1.0f;
    private float regeneration = 1.0f;
    public PowerUpUI powerUp;

    private void Start()
    {
        regenerationDuration = PowerupManager.Instance.regernerationDuration;
        regeneration = PowerupManager.Instance.regerneration;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            pickupSound.Play();
            StartCoroutine(regenerate(player));
            //Remove Powerup
        }
    }

    private IEnumerator regenerate(Collider player)
    {
        powerUp.SetImage("HEALTH");
        player.GetComponent<PlayerDamage>().regenerateHealth(regeneration, regenerationDuration);
        yield return new WaitForSeconds(regenerationDuration);
        powerUp.ResetImage();
    }

}
