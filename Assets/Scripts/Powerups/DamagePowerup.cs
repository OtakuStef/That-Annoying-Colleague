using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class DamagePowerup : MonoBehaviour
{
    private bool isTriggered = false;
    private float damageTimeout = 1.0f;
    private float damageMultiplier = 1.0f;

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
            PowerupManager.Instance.powerUpAudio.Play();
            StartCoroutine(damageUp(player));
            this.transform.parent.gameObject.transform.position = new Vector3(0, -100, 0);

        }
    }

    private IEnumerator damageUp(Collider player)
    {
        player.GetComponent<UIManager>().PlayerUI.powerUp.SetImage("DMG");
        if (player.name == "Player1")
        {
            PowerupManager.Instance.setPlayer1DamageBooster(damageMultiplier);
        }
        else if(player.name == "Player2")
        {
            PowerupManager.Instance.setPlayer2DamageBooster(damageMultiplier);
        }

        yield return new WaitForSeconds(damageTimeout);

        if (player.name == "Player1")
        {
            PowerupManager.Instance.setPlayer1DamageBooster(1.0f);
        }
        else if(player.name == "Player2")
        {
            PowerupManager.Instance.setPlayer2DamageBooster(1.0f);
        }
        player.GetComponent<UIManager>().PlayerUI.powerUp.ResetImage();
        Destroy(this.transform.parent.gameObject);
    }

}
