using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        /*
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        */
    }

    public UIPlayer PlayerUI;

    //public UIManager Instance { get; private set; }
}
