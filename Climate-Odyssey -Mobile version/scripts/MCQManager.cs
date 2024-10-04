using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MCQCanvas
{
    public GameObject canvasObject;    // The MCQ canvas GameObject
    public Text questionText;          // The Text component for the question
    public Button[] mcqButtons;        // The MCQ buttons (A, B, C, D)
    public Button[] answerButtons;     // The corresponding overlay answer buttons
}

public class MCQManager : MonoBehaviour
{
    [Header("MCQ Canvases Configuration")]
    public MCQCanvas[] mcqCanvases;   // Array of MCQ canvases for each test

    private int currentMCQCanvasIndex = 0; // Track which canvas is currently active
    private int correctAnswerIndex = 0;     // Index of the correct answer

    private void Start()
    {
        ResetMCQ(); // Hide all buttons initially
    }

    // This method will be called to display the MCQ on the relevant canvas
    public void DisplayMCQ(string question, string[] options, int correctAnswer, int canvasIndex)
    {
        // Hide all MCQ canvases initially
        HideAllCanvases();

        // Activate the relevant MCQ canvas based on the provided index
        currentMCQCanvasIndex = canvasIndex;
        MCQCanvas activeCanvas = mcqCanvases[canvasIndex];
        activeCanvas.canvasObject.SetActive(true);

        // Set up the question and options
        activeCanvas.questionText.text = question;
        correctAnswerIndex = correctAnswer;

        // Show and configure the MCQ buttons
        for (int i = 0; i < activeCanvas.mcqButtons.Length; i++)
        {
            activeCanvas.mcqButtons[i].GetComponentInChildren<Text>().text = options[i];
            activeCanvas.mcqButtons[i].gameObject.SetActive(true); // Show world-space buttons
            activeCanvas.answerButtons[i].gameObject.SetActive(true); // Show corresponding screen overlay buttons

            // Remove previous listeners to avoid duplicate calls
            activeCanvas.mcqButtons[i].onClick.RemoveAllListeners();

            // Add the event listener for this button to handle answer selection
            int buttonIndex = i;  // Capture the correct index for the lambda expression
            activeCanvas.mcqButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex));
        }
    }

    // Handle answer selection
    public void OnAnswerSelected(int selectedIndex)
    {
        MCQCanvas currentCanvas = mcqCanvases[currentMCQCanvasIndex];

        if (selectedIndex == correctAnswerIndex)
        {
            // Correct answer, set the correct answer button to green
            currentCanvas.answerButtons[correctAnswerIndex].GetComponent<Image>().color = Color.green;

            // Add 20 points to the score
            ScoreManager.instance.AddPoints(20);
        }
        else
        {
            // Incorrect answer, set the wrong selected answer to red
            currentCanvas.answerButtons[selectedIndex].GetComponent<Image>().color = Color.red;

            // Set the correct answer button to green
            currentCanvas.answerButtons[correctAnswerIndex].GetComponent<Image>().color = Color.green;
        }

        // Disable all answer buttons after an answer is selected
        foreach (Button btn in currentCanvas.answerButtons)
        {
            btn.interactable = false; // Disable all buttons
        }

        // Notify GameManager to proceed to the next step
        GameManager.Instance.OnQuestionAnswered();
    }



    // Reset the MCQ UI for the next question or test
    public void ResetMCQ()
    {
        foreach (MCQCanvas mcqCanvas in mcqCanvases)
        {
            // Reset the button states before the next question
            foreach (Button btn in mcqCanvas.answerButtons)
            {
                btn.GetComponent<Image>().color = Color.white; // Reset color to white
                btn.interactable = true; // Enable buttons for the next question
                btn.gameObject.SetActive(false); // Hide the buttons until needed
            }

            // Hide world-space buttons as well
            foreach (Button btn in mcqCanvas.mcqButtons)
            {
                btn.gameObject.SetActive(false);
            }

            // Clear the question text
            mcqCanvas.questionText.text = "";

            // Hide all MCQ canvases after resetting
            mcqCanvas.canvasObject.SetActive(false);
        }
    }

    // Helper function to hide all MCQ canvases
    private void HideAllCanvases()
    {
        foreach (MCQCanvas mcqCanvas in mcqCanvases)
        {
            mcqCanvas.canvasObject.SetActive(false);
        }
    }
}
