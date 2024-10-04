using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcq_manager_water_salinity_script : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What does conductivity in water primarily measure?", "What is indicated by low conductivity levels (<50 µS/cm) in water?", "Which of the following factors can cause high conductivity (>500 µS/cm) in water bodies?", "Why is it important to record the water temperature before conducting the conductivity test?", "How can you adjust the reading of the conductivity meter during calibration if it does not match the standard solution value?", "What should you do if one or more conductivity measurements are not within 40 µS/cm of the average?" };
    private string[][] answers = {
        new string[] { "Oxygen content", "The ability to conduct electrical current, influenced by dissolved ions", "The amount of dissolved oxygen", "The temperature of the water" },
        new string[] { "High levels of pollution", "Very pure water with few dissolved ions", "Excessive nutrient enrichment", "Water is very warm" },
        new string[] { "Low oxygen levels", "Natural mineral deposits, pollution, or runoff", "Very low pH", "Lack of nutrients in the ecosystem" },
        new string[] { "Conductivity readings are temperature-dependent, and water needs to be between 20˚ and 30˚ C", "The temperature affects the oxygen content in water", "Temperature affects the number of dissolved ions", "To determine the water's pH level" },
        new string[] { "Rinse the electrode again with distilled water", "Use a small screwdriver to adjust the calibration screw", "Submerge the probe deeper in the solution", "Wait for the water to cool down" },
        new string[] { "Ignore the readings", "Recalibrate the meter", "Take fresh samples and repeat the measurements", "Adjust the pH of the water" }
    };
    private int[] correctAnswers = { 1, 1,1,0,1,2 }; // Indices of correct answers
    private int currentQuestion = 0;

    public GameObject mcqPanel; // Reference to the MCQ panel

    public void StartMCQ()
    {
        currentQuestion = 0; // Reset the question index
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionText.text = questions[currentQuestion];
            for (int i = 0; i < answers[currentQuestion].Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = answers[currentQuestion][i];
                answerButtons[i].onClick.RemoveAllListeners();
                int answerIndex = i; // Capture the index
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
            }
        }
        else
        {
            // MCQ finished, hide everything
            CompleteMCQ();
        }
    }

    private void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == correctAnswers[currentQuestion])
        {
            answerButtons[selectedAnswerIndex].GetComponent<Image>().color = correctColor;
            ScoreManager.instance.AddPoints(pointsPerCorrectAnswer); // Add points for correct answer
        }
        else
        {
            answerButtons[selectedAnswerIndex].GetComponent<Image>().color = incorrectColor;
        }

        // Proceed to the next question after a short delay
        Invoke("NextQuestion", 1f);
    }

    private void NextQuestion()
    {
        // Reset button colors
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
        }

        currentQuestion++;
        DisplayQuestion(); // Show the next question or complete MCQ
    }

    private void CompleteMCQ()
    {
        Debug.Log("Water Salinity MCQ completed, hiding UI elements");
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `water_salinity_script` to hide images and reset
        FindObjectOfType<water_salinity_script>().CompleteMCQ();
    }

    public void ResetMCQ()
    {
        currentQuestion = 0;

        // Reset button colors and remove listeners
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.onClick.RemoveAllListeners();
        }

        mcqPanel.SetActive(false);
    }
}
