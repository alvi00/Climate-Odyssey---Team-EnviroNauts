using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lab_timer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutes (300 seconds)
    public Text timerText;

    private float timeRemaining;
    private bool isTiming = true; // Start the timer automatically when the game starts
    private int completedTests = 0; // Counter for completed tests
    private const int totalTests = 6; // Total number of tests

    // Reference to the game over UI elements
    public GameObject gameOverUI; // The panel containing the image and exit button
    public GameObject completionUI; // The panel to show when all tests are completed

    // Reference to the Image Manager
    public last_image_manager imageManager; // Reference to last_image_manager

    void Start()
    {
        Time.timeScale = 1; // Ensure time scale is reset to 1 when the scene starts
        timeRemaining = timeLimit;
        UpdateTimerDisplay();
        gameOverUI.SetActive(false); // Ensure the game over UI is hidden at the start
        completionUI.SetActive(false); // Ensure the completion UI is hidden at the start
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

    public void CompleteTest()
    {
        completedTests++;
        if (completedTests >= totalTests)
        {
            isTiming = false; // Stop the timer
            Debug.Log("All tasks completed! Starting image sequence.");
            imageManager.StartImageSequence(); // Start images sequence when all tasks are completed
        }
    }

    public void ShowCompletionUI()
    {
        completionUI.SetActive(true);
        Time.timeScale = 0; // Pause the game
        Debug.Log("All tasks completed and MCQ finished. Showing completion UI.");
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        Debug.Log("Time's up! Game Over.");
    }

    void UpdateTimerDisplay()
    {
        timerText.text = $"Time Left: {Mathf.FloorToInt(timeRemaining / 60):00}:{Mathf.FloorToInt(timeRemaining % 60):00}";
    }
}
