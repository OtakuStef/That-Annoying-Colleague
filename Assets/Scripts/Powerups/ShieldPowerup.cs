using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class ShieldPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    public AudioSource pickupSound;
    private float shieldDuration = 1.0f;
    public PowerUpUI powerUp;

    private void Start()
    {
        shieldDuration = PowerupManager.Instance.shieldDuration;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            pickupSound.Play();
            StartCoroutine(setShield(player));
            //Remove Powerup
        }
    }

    private IEnumerator setShield(Collider player)
    {
        powerUp.SetImage("SHIELD");
        player.GetComponent<PlayerController>().setShield(true);
        yield return new WaitForSeconds(shieldDuration);
        player.GetComponent<PlayerController>().setShield(false);
        powerUp.ResetImage();
    }

}
