using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThatAnnoyyingColleagueInput.PlayerControl;

public class SpeedPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    private float speedTimeout = 1.0f;
    private float speedMultiplier = 1.0f;

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
            PowerupManager.Instance.playPowerupPickupSound();
            StartCoroutine(speedUp(player));
            this.transform.parent.gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }

    private IEnumerator speedUp(Collider player)
    {
        player.GetComponent<UIManager>().PlayerUI.powerUp.SetImage("SPEED");
        player.GetComponent<PlayerController>().setSpeedMultiplier(speedMultiplier);
        yield return new WaitForSeconds(speedTimeout);
        player.GetComponent<PlayerController>().setSpeedMultiplier(1.0f);
        player.GetComponent<UIManager>().PlayerUI.powerUp.ResetImage();
        Destroy(this.transform.parent.gameObject);
    }

}
