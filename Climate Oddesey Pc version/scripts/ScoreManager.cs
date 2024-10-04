using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For handling scene loading

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public Text scoreText; // Reference to UI Text component

    private int score = 0;

    private void Awake()
    {
        // Ensure there is only one instance of the score manager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the ScoreManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void OnEnable()
    {
        // Subscribe to the sceneLoaded event to refresh the scoreText when a new scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Method that gets called whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindScoreText(); // Re-assign the scoreText when a new scene is loaded
        UpdateScoreUI(); // Update the score in the UI immediately

        // Reset the score when entering the lab scene
        if (scene.name == "water_polution_lab")
        {
            ResetScore(); // Reset score in the lab
        }
        else if (scene.name == "water_pollution_fix_things")
        {
            // Carry over the score to "water_pollution_fix_things"
            UpdateScoreUI();
        }
    }


    // Method to find the Score Text UI component by tag
    private void FindScoreText()
    {
        if (scoreText == null)
        {
            GameObject scoreTextObject = GameObject.FindWithTag("score_text");
            if (scoreTextObject != null)
            {
                scoreText = scoreTextObject.GetComponent<Text>();
            }
            else
            {
                Debug.LogWarning("Score Text UI not found! Ensure it is tagged as 'score_text'.");
            }
        }
    }

    // Method to add points and update the UI
    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    // Update the score UI text
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    // Method to reset the score if needed
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    // Method to get the current score (if you need it elsewhere)
    public int GetScore()
    {
        return score;
    }
}
