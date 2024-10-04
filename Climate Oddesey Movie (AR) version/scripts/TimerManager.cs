using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public float timerDuration = 300f; // 5 minutes in seconds
    private float timeRemaining;
    private bool timerRunning = false;

    public Text timerText; // UI Text to display the timer
    public GameObject timeUpCanvas; // Canvas to show when time is up

    private void Start()
    {
        timeRemaining = timerDuration;
        timerRunning = true;
        timeUpCanvas.SetActive(false); // Ensure the canvas is hidden initially
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                // Time is up
                timerRunning = false;
                timeRemaining = 0;
                UpdateTimerUI();
                OnTimeUp();
            }
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update the timer UI
    }

    private void OnTimeUp()
    {
        // Show the canvas when time is up
        timeUpCanvas.SetActive(true);
    }
}
