using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcq_manager_for_nitrate_phosphate : MonoBehaviour
{
    public Text questionText; // UI Text to display the question
    public Button[] answerButtons; // Buttons for answers
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public int pointsPerCorrectAnswer = 20; // Points for correct answer

    private string[] questions = { "What is the primary source of high nitrate levels in water bodies?", "What is the effect of excessive nitrate levels on aquatic ecosystems?", "What happens if nitrate levels exceed 10 mg/L in a freshwater system?", "Which of the following conditions is most likely caused by high nitrate levels?", "What is the primary effect of high nitrate levels on aquatic organisms?", "What is the acceptable range of nitrate levels for freshwater systems, according to GLOBE values?" };
    private string[][] answers = {
        new string[] { "Atmospheric pollution", "Industrial waste and sewage", "Agricultural runoff and fertilizers", "Oceanic currents" },
        new string[] { "Increased oxygen levels in water", "Promotion of algal blooms, reducing oxygen levels", "Decrease in water pH", "Improved water clarity" },
        new string[] { "It may enhance plant growth without harmful effects.", "It indicates high pollution levels, which can harm aquatic life.", "It suggests balanced nutrient levels.", "It improves the oxygen content in the water." },
        new string[] { "Eutrophication", "Reduced nutrient levels", "Increased water pH", "Higher transparency" },
        new string[] { "Increased reproductive rates", "Enhanced growth of phytoplankton", "Oxygen depletion leading to fish kills", "Stabilization of food webs" },
        new string[] { "0 - 1 mg/L", "1 - 5 mg/L", "1 - 10 mg/L", "10 - 20 mg/L" }
    };
    private int[] correctAnswers = { 2,1,1,0,2,2 }; // Indices of correct answers
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
        Debug.Log("Nitrate/Phosphate MCQ completed, hiding UI elements");
        mcqPanel.SetActive(false);

        // Call the CompleteMCQ method on the `nitrate_phosphate_script` to hide images and reset
        FindObjectOfType<nitrate_phosphate_script>().CompleteMCQ();
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
