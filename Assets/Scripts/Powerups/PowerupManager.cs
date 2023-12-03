using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Global Variables
    public float shieldDuration = 4.0f;
    public float speedMultiplier = 1.4f;
    public float speedDuration = 8.0f;
    public float regerneration = 5.0f;
    public float regernerationDuration = 8.0f;
    public float damageMultiplier = 2.0f;
    public float damageDuration = 8.0f;
    private float player1DamageBooster = 1.0f;
    private float player2DamageBooster = 1.0f;
    public AudioSource powerUpAudio;

    public float getPlayer1DamageBooster() {  return player1DamageBooster; }

    public float getPlayer2DamageBooster() {  return player2DamageBooster; }

    public void setPlayer1DamageBooster(float damageBooster) { this.player1DamageBooster = damageBooster; }

    public void setPlayer2DamageBooster(float damageBooster) { this.player2DamageBooster = damageBooster; }
}
