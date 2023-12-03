using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
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

    //Global Player Settings
    public float minPlayerDamageMagnitude = 2.0f;
    public float playerDamageMultiplicator = 1.0f;
    public float maxPossiblePlayerDamage = 10.0f;
}
