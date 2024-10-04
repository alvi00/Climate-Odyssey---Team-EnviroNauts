using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ph_scale_mcq_manager : MonoBehaviour
{
    public Text questionText; // UI text for displaying the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points awarded for a correct answer

    private string[] questions = {
        "What pH range is considered safe for most freshwater organisms, according to GLOBE standards?",
        "What should you do if your pH readings are not within 1.0 units of the average during the test?",
        "What might a pH level above 8.5 indicate in natural water bodies?",
        "What is the purpose of repeating steps 4-6 with new water samples and pH paper?",
        "Why is low pH (<6) harmful to aquatic life, according to GLOBE standards?",
        "Which tool can be used to measure pH?"
    };

    private string[][] answers = {
        new string[] { "0 to 6", "6.0 to 8.5", "7 to 9", "8.5 to 10" },
        new string[] { "Accept the readings as they are", "Discard the sample and start a new test", "Repeat the measurements", "Use the highest pH reading" },
        new string[] { "The water is highly acidic", "Excessive algal growth or chemical imbalances", "High levels of toxic metals", "An increase in wildlife" },
        new string[] { "To ensure the accuracy of the pH reading", "To get different pH values", "To reduce the pH value of the water", "To increase the pH value of the water" },
        new string[] { "It reduces oxygen in the water", "It increases the solubility of toxic metals", "It decreases water temperature", "It increases the growth of aquatic plants" },
        new string[] { "Thermometer", "pH paper", "Ruler", "Scale" }
    };

    private int[] correctAnswers = { 1, 2, 3,0,1,1}; // Indices of the correct answers
    private int currentQuestion = 0;

    public GameObject mcqPanel; // Reference to the MCQ panel

    public void StartMCQ()
    {
        currentQuestion = 0; // Reset question index
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
                int answerIndex = i; // Capture index for listener
                answerButtons[i].onClick.AddListener(() => CheckAnswer(answerIndex));
            }
        }
        else
        {
            CompleteMCQ(); // If all questions are done, complete MCQ
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

        // Move to the next question after a delay
        Invoke("NextQuestion", 1f);
    }

    private void NextQuestion()
    {
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // Reset button colors
        }

        currentQuestion++; // Increment to the next question
        DisplayQuestion(); // Display next question or complete MCQ
    }

    private void CompleteMCQ()
    {
        Debug.Log("pH Scale MCQ completed, hiding UI elements");
        mcqPanel.SetActive(false);

        // Call CompleteMCQ on the `ph_scale_script` to hide images and reset
        FindObjectOfType<ph_scale_script>().CompleteMCQ();
    }

    public void ResetMCQ()
    {
        currentQuestion = 0; // Reset the question index

        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // Reset button colors
            btn.onClick.RemoveAllListeners(); // Remove listeners
        }

        mcqPanel.SetActive(false); // Hide the MCQ panel
    }
}
