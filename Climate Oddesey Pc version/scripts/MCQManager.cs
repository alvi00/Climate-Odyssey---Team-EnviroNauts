using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCQManager : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What is the pH of pure water?", "What is the pH of lemon juice?" };
    private string[][] answers = {
        new string[] { "7", "6", "8", "5" },
        new string[] { "2", "4", "7", "9" }
    };
    private int[] correctAnswers = { 0, 0 }; // Indices of correct answers
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
        Debug.Log("MCQ completed, hiding UI elements");
        // Hide the MCQ panel and other elements when all questions are answered
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `ph_scale_script` to hide images and reset
        FindObjectOfType<ph_scale_script>().CompleteMCQ();
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
