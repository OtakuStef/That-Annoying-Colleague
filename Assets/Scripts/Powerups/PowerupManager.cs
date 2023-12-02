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
    public float spawnRate = 1.0f;
    public float shieldDuration = 4.0f;
    public float speedMultiplier = 1.4f;
    public float speedDuration = 10.0f;
    public float regerneration = 5.0f;
    public float regernerationDuration = 10.0f;
    public float damageMultiplier = 2.0f;
    public float damageDuration = 10.0f;
}
