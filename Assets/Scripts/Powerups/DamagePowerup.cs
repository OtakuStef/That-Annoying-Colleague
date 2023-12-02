using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class DamagePowerup : MonoBehaviour
{
    private bool isTriggered = false;
    public AudioSource pickupSound;
    private float damageTimeout = 1.0f;
    private float damageMultiplier = 1.0f;
    public PowerUpUI powerUp;

    private void Start()
    {
        damageTimeout = PowerupManager.Instance.damageDuration;
        damageMultiplier = PowerupManager.Instance.damageMultiplier;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            pickupSound.Play();
            StartCoroutine(damageUp(player));
            //Remove Powerup
        }
    }

    private IEnumerator damageUp(Collider player)
    {
        powerUp.SetImage("DMG");
        player.GetComponent<PlayerController>().setDamageMultiplier(damageMultiplier);
        yield return new WaitForSeconds(damageTimeout);
        player.GetComponent<PlayerController>().setDamageMultiplier(1.0f);
        powerUp.ResetImage();
    }

}
