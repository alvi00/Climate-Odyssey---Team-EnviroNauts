using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterSampleTimer : MonoBehaviour
{
    public float timeLimit = 180f; // 3 minutes (180 seconds)
    public Text timerText;
    private bool isTiming = false;
    public bool sampleCollected = false; // Flag to track if the sample has been collected
    private float timeRemaining;

    // Reference to the game over UI elements
    public GameObject gameOverUI; // The panel containing the image and exit button

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerDisplay();
        gameOverUI.SetActive(false); // Ensure the game over UI is hidden at the start
    }

    void Update()
    {
        if (isTiming)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                isTiming = false;
                GameOver();
            }
        }
    }

    public void StartTimer()
    {
        // Only start the timer if it is not already running and the sample hasn't been collected
        if (!isTiming && !sampleCollected)
        {
            Debug.Log("Timer started.");
            isTiming = true;
        }
    }

    public void CollectSample()
    {
        Debug.Log("Sample Collected! Timer is still running.");
        sampleCollected = true; // Set the flag to true when the sample is collected
        // Trigger the success scenario if needed
        Debug.Log("Sample Collected!");
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        // Handle the game over scenario (e.g., show game over screen, restart level)
        Debug.Log("Time's up! Game Over.");
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"Time : {Mathf.FloorToInt(timeRemaining / 60):00}:{Mathf.FloorToInt(timeRemaining % 60):00}";
    }

    private void OnDisable()
    {
        // Stop the timer if the object is disabled (e.g., when transitioning to a new scene)
        if (isTiming)
        {
            isTiming = false;
            Debug.Log("Timer stopped due to scene change.");
        }
    }
}
