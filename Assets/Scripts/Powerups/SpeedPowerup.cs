using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class SpeedPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    public AudioSource pickupSound;
    private float speedTimeout = 1.0f;
    private float speedMultiplier = 1.0f;
    public PowerUpUI powerUp;

    private void Start()
    {
        speedTimeout = PowerupManager.Instance.speedDuration;
        speedMultiplier = PowerupManager.Instance.speedMultiplier;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            pickupSound.Play();
            StartCoroutine(speedUp(player));
            //Remove Powerup
        }
    }

    private IEnumerator speedUp(Collider player)
    {
        powerUp.SetImage("SPEED");
        player.GetComponent<PlayerController>().setSpeedMultiplier(speedMultiplier);
        yield return new WaitForSeconds(speedTimeout);
        player.GetComponent<PlayerController>().setSpeedMultiplier(1.0f);
        powerUp.ResetImage();
    }

}
