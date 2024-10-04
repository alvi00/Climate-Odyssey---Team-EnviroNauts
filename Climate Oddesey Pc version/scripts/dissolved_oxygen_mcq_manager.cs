using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dissolved_oxygen_mcq_manager : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What is Dissolved Oxygen (DO)?", "What is a normal range for Dissolved Oxygen levels in most freshwater ecosystems?", "What happens to aquatic life when DO levels drop below 2 mg/L?", "Why might DO levels above 9 mg/L occur in water?", "Which reagent is used first when conducting a Dissolved Oxygen test with a chemical kit?", "What is the relationship between temperature and Dissolved Oxygen levels in water?"};
    private string[][] answers = {
        new string[] { "The amount of oxygen in the air", "The amount of oxygen present in water", "The amount of nitrogen in water", "The amount of oxygen in soil"},
        new string[] { "1-3 mg/L", "5-9 mg/L", "10-14 mg/L", "0-2 mg/L" },
        new string[] { "It supports the growth of fish and plants", "It leads to hypoxia, which can cause fish kills", "It improves water quality", "It increases biodiversity" },
        new string[] { "Low oxygen concentration", "High photosynthetic activity during daylight hours", "Water pollution", "Decomposition of organic matter" },
        new string[] { "Sulfuric Acid", "Alkaline Iodide Azide", "Manganese Sulfate", "Distilled Water" },
        new string[] { "Higher temperatures increase DO levels", "Lower temperatures decrease DO levels", "Lower temperatures increase DO levels", "Temperature has no effect on DO levels" }


    };
    private int[] correctAnswers = { 1, 1,1,1,2,2 }; // Indices of correct answers
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
        Debug.Log("Dissolved oxygen MCQ completed, hiding UI elements");
        // Hide the MCQ panel and other elements when all questions are answered
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `dissolved_oxygen_Script` to hide images and reset
        FindObjectOfType<dissolved_oxygen_Script>().CompleteMCQ();
    }

    // Method to reset the MCQ for the next session
    public void ResetMCQ()
    {
        // Reset the question index to start from the first question
        currentQuestion = 0;

        // Reset button colors and remove listeners
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
            btn.onClick.RemoveAllListeners();
        }

        // Hide the panel until it's activated again
        mcqPanel.SetActive(false);
    }
}
