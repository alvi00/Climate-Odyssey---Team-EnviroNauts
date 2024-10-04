using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class last_mcq_manager : MonoBehaviour
{
    public Text questionText; // UI Text for displaying questions
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answers

    private string[] questions = { "What is one of the most effective ways to reduce water pollution caused by industrial waste?" };
    private string[][] answers = {
        new string[] { "Increase agricultural production near water sources", "Build more dams on rivers", "Enforce stricter regulations on wastewater treatment", "Use more chemical fertilizers\r\n" }
    };
    private int[] correctAnswers = { 2 }; // Indices of the correct answers
    private int currentQuestion = 0;

    public GameObject mcqPanel;
    public lab_timer timer; // Reference to lab_timer to call ShowCompletionUI

    public void StartMCQ()
    {
        currentQuestion = 0;
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        if (currentQuestion < questions.Length)
        {
            questionText.text = questions[currentQuestion];
            for (int i = 0; i < answers[currentQuestion].Length; i++)
            {
                answerButtons[i].GetComponentInChildren<Text>().text = answers[currentQuestion][i];
                answerButtons[i].onClick.RemoveAllListeners();
                int answerIndex = i; // Capture the current index
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
            }
        }
        else
        {
            CompleteMCQ();
        }
    }

    void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == correctAnswers[currentQuestion])
        {
            answerButtons[selectedAnswerIndex].GetComponent<Image>().color = correctColor;
        }
        else
        {
            answerButtons[selectedAnswerIndex].GetComponent<Image>().color = incorrectColor;
        }

        // Move to the next question after a delay
        Invoke("NextQuestion", 1f); // Adjust the delay if necessary
    }

    void NextQuestion()
    {
        // Reset button colors
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
        }

        currentQuestion++;
        DisplayQuestion();
    }

    void CompleteMCQ()
    {
        Debug.Log("MCQ finished.");
        mcqPanel.SetActive(false); // Hide MCQ panel when done
        timer.ShowCompletionUI(); // Show the completion UI after the MCQ is done
    }
}
