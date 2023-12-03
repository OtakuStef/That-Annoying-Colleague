using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;
using static UnityEngine.Rendering.DebugUI;

public class HealthPowerup : MonoBehaviour
{
    private bool isTriggered = false;
    private float regenerationDuration = 1.0f;
    private float regeneration = 1.0f;

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
            PowerupManager.Instance.playPowerupPickupSound();
            StartCoroutine(regenerate(player));
            this.transform.parent.gameObject.transform.position = new Vector3(0, -100, 0);
        }
    }

    private IEnumerator regenerate(Collider player)
    {
        player.GetComponent<UIManager>().PlayerUI.powerUp.SetImage("HEALTH");
        player.GetComponent<PlayerDamage>().regenerateHealth(regeneration, regenerationDuration);
        yield return new WaitForSeconds(regenerationDuration);
        player.GetComponent<UIManager>().PlayerUI.powerUp.ResetImage();
        Destroy(this.transform.parent.gameObject);
    }

}
