using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class water_pollution_timer : MonoBehaviour
{
    public float timeLimit = 180f; // 3 minutes in seconds (3 * 60)
    public GameObject popupCanvas; // The canvas that will pop up after the timer ends
    public Text timeText; // UI Text object to display the timer
    private float currentTime; // Keeps track of the current time
    private bool timerRunning = false; // Keeps track if the timer is running

    void Start()
    {
        // Initialize the current time and start the timer
        currentTime = timeLimit;
        popupCanvas.SetActive(false); // Make sure the popup canvas is hidden initially
        timerRunning = true; // Set the timer to start running
    }

    void Update()
    {
        if (timerRunning)
        {
            // Decrease the time and update the timer
            currentTime -= Time.deltaTime;

            // Update the time display
            DisplayTime(currentTime);

            // Check if the time is up
            if (currentTime <= 0)
            {
                TimerEnd();
            }
        }
    }

    // Function called when the timer ends
    void TimerEnd()
    {
        timerRunning = false; // Stop the timer
        popupCanvas.SetActive(true); // Show the popup canvas
        DisplayTime(0); // Ensure the time displays 0:00
    }

    // Function to format and display the time in the UI
    void DisplayTime(float timeToDisplay)
    {
        // Clamp the time to ensure it doesn't go below 0
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // Update the text UI
        timeText.text = "Time :"+string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


//