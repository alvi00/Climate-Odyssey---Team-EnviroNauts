using UnityEngine;
using UnityEngine.UI;

public class MCQManager_Test2 : MonoBehaviour
{
    public Text questionText; // To display the question
    public Button[] answerButtons; // The four answer buttons in screen space

    private int correctAnswerIndex = 0; // Index of the correct answer

    private void Start()
    {
        ResetMCQ(); // Hide buttons initially
    }

    public void DisplayMCQ(string question, string[] options, int correctAnswer)
    {
        questionText.text = question;
        correctAnswerIndex = correctAnswer;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = options[i];
            answerButtons[i].gameObject.SetActive(true); // Show the screen-space buttons
        }
    }

    public void OnAnswerSelected(int selectedIndex)
    {
        // Mark the selected answer (correct or incorrect)
        if (selectedIndex != correctAnswerIndex)
        {
            // Set the wrong selected answer to red
            answerButtons[selectedIndex].GetComponent<Image>().color = Color.red;
        }

        // Set the correct answer button to green
        answerButtons[correctAnswerIndex].GetComponent<Image>().color = Color.green;

        // Disable all answer buttons after an answer is selected
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
        }

        // Notify GameManager_Test2 to proceed to the next step
        GameManager_Test2.Instance.OnQuestionAnswered();
    }

    public void ResetMCQ()
    {
        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // Reset color
            btn.interactable = true; // Enable buttons for the next question
            btn.gameObject.SetActive(false); // Hide the buttons until needed
        }

        questionText.text = ""; // Clear question text
    }
}
