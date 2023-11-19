using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //Gloabal Object Settings
    public float minDamageMagnitude = 2.0f;
    public float damageMultiplicator = 1.0f;
    public float maxPossibleDamage = 4.0f;
    public string throwableObjectTag = "Throwable";
}
