using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class ShieldPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    private float shieldDuration = 1.0f;

    private void Start()
    {
        shieldDuration = PowerupManager.Instance.shieldDuration;
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            PowerupManager.Instance.playPowerupPickupSound();
            StartCoroutine(setShield(player));
            this.transform.parent.gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }

    private IEnumerator setShield(Collider player)
    {
        player.GetComponent<UIManager>().PlayerUI.powerUp.SetImage("SHIELD");
        player.GetComponent<PlayerController>().setShield(true);
        yield return new WaitForSeconds(shieldDuration);
        player.GetComponent<PlayerController>().setShield(false);
        player.GetComponent<UIManager>().PlayerUI.powerUp.ResetImage();
        Destroy(this.transform.parent.gameObject);
    }

}
