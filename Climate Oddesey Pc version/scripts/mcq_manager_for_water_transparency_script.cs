using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcq_manager_for_water_transparency_script : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What does water transparency measure?", "What is one of the possible causes of low transparency in water?", "According to GLOBE values, what does a transparency reading of less than 1 meter suggest?", "What is the importance of shading the Secchi disk during transparency measurement?", "When marking the rope during transparency measurement, why is it important to ensure the marks are consistent?", "What would be the most appropriate course of action if the transparency reading is consistently below 1 meter in a lake?" };
    private string[][] answers = {
        new string[] { "The amount of oxygen in the water", "The clarity of water, indicating the presence of suspended particles", "The temperature of water", "The level of acidity in water" },
        new string[] { "High water temperature", "Increased sunlight penetration", "High levels of suspended particles, such as sediment or algae", "Increased dissolved oxygen" },
        new string[] { "Excellent water quality", "High levels of dissolved oxygen", "Significant pollution or issues affecting water clarity", "Water is too cold for measurement" },
        new string[] { "To prevent the Secchi disk from being submerged", "To reduce glare and ensure accurate readings", "To cool down the water sample", "To prevent pollution from affecting the measurement" },
        new string[] { "To standardize the distance from the water surface to the Secchi disk", "To measure the depth of the water", "To compare the distance between the observer and the water", "To avoid touching the rope with your hands" },
        new string[] { "Add more Secchi disks to increase accuracy", "Consider possible sources of pollution or sedimentation affecting water clarity.", "Move to a different location in the lake to take new measurements", "Calibrate the Secchi disk before remeasuring." }
    };
    private int[] correctAnswers = { 1,2,2,1,0,1 }; // Indices of correct answers
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
        Debug.Log("Water Transparency MCQ completed, hiding UI elements");
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `water_transparency_script` to hide images and reset
        FindObjectOfType<water_transparency_script>().CompleteMCQ();
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
