using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Correct namespace for TextMeshPro
using UnityEngine.Events;

public class WaterPollutionScoreGetter : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputScore; // Use TMP_InputField for input
    [SerializeField]
    private TMP_InputField inputName;
    public UnityEvent<string, int> submitScoreEvent;

    private bool hasSubmitted = false; // Flag to track if the score has been submitted

    private void Start()
    {
        // Populate the score input field with the current score from ScoreManager
        int currentScore = ScoreManager.instance.GetScore();
        inputScore.text = currentScore.ToString();
        inputScore.interactable = false; // Make the input field non-editable
    }

    public void Submitscore()
    {
        if (hasSubmitted) // Check if the score has already been submitted
        {
            Debug.LogWarning("You can only submit your score once.");
            return; // Exit if the score has already been submitted
        }

        // Validate the score input before parsing
        if (int.TryParse(inputScore.text, out int score))
        {
            submitScoreEvent.Invoke(inputName.text, score);
            hasSubmitted = true; // Set the flag to true after submission
        }
        else
        {
            Debug.LogError("Score input is not a valid integer.");
            // You may want to add additional feedback to the user here
        }
    }

    // Public method to disable input fields
    public void DisableInputFields()
    {
        inputName.interactable = false; // Disable name input
        inputScore.interactable = false; // Disable score input
    }
}