using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class water_temp_mcq_manager : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What does an alkalinity test measure?", "How does low alkalinity (<10 mg/L) affect water bodies?", "What is the acceptable alkalinity level for most healthy water bodies?", "Why is it important to titrate slowly during an alkalinity test?", "In an alkalinity test, after titration with acid (HCl), what does the total volume of acid used indicate?", "What is the purpose of adding an alkalinity indicator, like phenolphthalein, in an alkalinity test?"};
    private string[][] answers = {
        new string[] { "How well water can resist pH changes", "The amount of dissolved oxygen in water", "The temperature of the water", "The amount of salt in water" },
        new string[] { "It makes the water resistant to acidification", "It makes the water more sensitive to pH changes and prone to acidification", "It prevents pollution from affecting the water quality", "It helps aquatic plants grow faster" },
        new string[] { "10-100 mg/L", "200-300 mg/L", "20-200 mg/L", "0-10 mg/L" },
        new string[] { "To prevent the water from evaporating", "To ensure accurate measurement of the acid needed to change the pH", "To decrease the water temperature", "To reduce oxygen in the water sample" },
        new string[] { "The amount of dissolved oxygen in the water", "The alkalinity level of the water sample", "The pH value of the water", "The presence of harmful chemicals" },
        new string[] { "To neutralize the water", "To prevent oxygen from dissolving in the sample", "To cause a color change, indicating a reaction with the titration acid", "To remove dissolved solids from the sample" }



    };
    private int[] correctAnswers = { 0, 1,2,1,1,2 }; // Indices of correct answers
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
        Debug.Log("Water temperature MCQ completed, hiding UI elements");
        // Hide the MCQ panel and other elements when all questions are answered
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `water_temp_script` to hide images and reset
        FindObjectOfType<water_temp_script>().CompleteMCQ();
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
