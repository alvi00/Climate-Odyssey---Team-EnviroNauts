using UnityEngine;
using UnityEngine.UI;

public class GameManager_Test2 : MonoBehaviour
{
    public static GameManager_Test2 Instance;

    public GameObject imageCanvas; // World-Space Canvas to display images
    public GameObject mcqCanvas;   // World-Space Canvas to display MCQ questions
    public GameObject answerCanvas; // Screen-Space Canvas to display MCQ buttons
    public MCQManager_Test2 mcqManager; // Reference to the new MCQ Manager

    public QuizQuestion[] questions; // Array of questions for Test2
    private int currentQuestionIndex = 0; // Track the current question
    private bool isMCQPhase = false; // Check if in MCQ phase

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mcqManager.ResetMCQ();
        imageCanvas.SetActive(true); // Start by showing images
        mcqCanvas.SetActive(false);  // Hide MCQ canvas at the start
        answerCanvas.SetActive(false); // Hide answer buttons at the start
    }

    public void OnNextButtonPressed()
    {
        if (!isMCQPhase)
        {
            ShowNextImage();
        }
        else
        {
            StartMCQ();
        }
    }

    private void ShowNextImage()
    {
        Test2 imageManager = imageCanvas.GetComponent<Test2>();

        if (imageManager != null)
        {
            imageManager.ShowNextImage();

            if (imageManager.IsLastImage())
            {
                isMCQPhase = true; // Switch to MCQ phase after last image
                imageCanvas.SetActive(false); // Hide image canvas
            }
        }
    }

    private void StartMCQ()
    {
        mcqCanvas.SetActive(true); // Show MCQ canvas
        answerCanvas.SetActive(true); // Show answer buttons
        PrepareMCQ();
    }

    private void PrepareMCQ()
    {
        if (currentQuestionIndex < questions.Length)
        {
            QuizQuestion currentQuestion = questions[currentQuestionIndex];
            mcqManager.DisplayMCQ(currentQuestion.text, currentQuestion.options, currentQuestion.correctAnswerIndex);
        }
        else
        {
            // All questions finished, hide everything
            mcqManager.ResetMCQ();
            mcqCanvas.SetActive(false);
            answerCanvas.SetActive(false);
        }
    }

    public void OnQuestionAnswered()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Length)
        {
            mcqManager.ResetMCQ(); // Reset MCQ for the next question
            PrepareMCQ();
        }
        else
        {
            mcqManager.ResetMCQ(); // Reset after all questions are answered
            mcqCanvas.SetActive(false);
            answerCanvas.SetActive(false);
        }
    }
}
