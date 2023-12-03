using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public RoundManager roundManager;
    public TextMeshProUGUI timerText;
    private TimeSpan timeRemaining;
    private bool timerIsRunning = false;
    private float startTime = 90;

    // Start is called before the first frame update
    void Start()
    {
        /*
        timerIsRunning = true;
        timeRemaining = TimeSpan.FromSeconds(startTime);
        UpdateTimerText();
        */

    }

    private void Awake()
    {
        roundManager = GameObject.FindGameObjectWithTag("GameRound").GetComponent<RoundManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Office Stage 1")
        {
            roundManager.StartNewRound();
            StartCountdown();
        }
    }

    public void StartCountdown()
    {
        if (!timerIsRunning)
        {
            timerIsRunning = true;
            timeRemaining = TimeSpan.FromSeconds(startTime);
            UpdateTimerText();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining.TotalSeconds > 0)
            {
                // subtract the time since last Update call
                timeRemaining -= TimeSpan.FromSeconds(Time.deltaTime);
                UpdateTimerText();
            }
            else
            {
                // TODO: add game stop when timer is finished
                timeRemaining = TimeSpan.Zero;
                timerIsRunning = false;
                UpdateTimerText();
                OnTimerFinished();
            }
        }
    }

    private void UpdateTimerText()
    {
        // update timer
        timerText.text = timeRemaining.ToString(@"mm\:ss");
    }

    private void OnTimerFinished()
    {
        RoundManager.Instance.EndRound();
        Debug.Log("Timer has finished!");
    }
}
