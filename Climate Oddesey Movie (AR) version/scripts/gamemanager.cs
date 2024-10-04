using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TestManager[] testManagers;    // Array of TestManager for each test
    public GameObject nextButton;         // Button to show for navigating images/questions
    public MCQManager mcqManager;         // Reference to the MCQManager
    public GameObject completionCanvas;
    private TestManager activeTest;
    public QuizQuestion[][] questions;      // Array of question sets for each test

    private int currentQuestionIndex = 0;
    private bool isMCQPhase = false;        // Indicates if it's MCQ phase after the images
    private bool resetFlag = false;         // Reset flag to restart the process
    private int currentTestIndex = 0;       // Track the active test

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager instance
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
        // Initialize questions for different tests
        questions = new QuizQuestion[][]
        {
            // Test 1 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What pH range is considered safe for most freshwater organisms, according to GLOBE standards?",
                    options = new string[] { "0 to 6", "6.0 to 8.5", "7 to 9", "8.5 to 10" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What should you do if your pH readings are not within 1.0 units of the average during the test?",
                    options = new string[] { " Accept the readings as they are", "Discard the sample and start a new test", "Repeat the measurements", "Use the highest pH reading" },
                    correctAnswerIndex = 2
                },
                  new QuizQuestion
                {
                    text = " What might a pH level above 8.5 indicate in natural water bodies?",
                    options = new string[] { "The water is highly acidic", "Excessive algal growth or chemical imbalances", " High levels of toxic metals", " An increase in wildlife" },
                    correctAnswerIndex = 1
                },
                  new QuizQuestion
                {
                    text = " What is the purpose of repeating steps 4-6 with new water samples and pH paper?",
                    options = new string[] { "To ensure the accuracy of the pH reading", "To get different pH values", "To reduce the pH value of the wate", "To increase the pH value of the wate" },
                    correctAnswerIndex = 0
                },
                  new QuizQuestion
                {
                    text = "Why is low pH (<6) harmful to aquatic life, according to GLOBE standards?",
                    options = new string[] { "It reduces oxygen in the water", "It increases the solubility of toxic metals", "It decreases water temperature", "It increases the growth of aquatic plants" },
                    correctAnswerIndex = 1
                },
                  new QuizQuestion
                {
                    text = "Which tool can be used to measure pH?",
                    options = new string[] { "Thermometer", "pH paper", "Ruler", "Scale" },
                    correctAnswerIndex = 1
                }
            },
            // Test 2 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What is Dissolved Oxygen (DO)?",
                    options = new string[] { "The amount of oxygen in the air", "The amount of oxygen present in water", "The amount of nitrogen in water", "The amount of oxygen in soit" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What is a normal range for Dissolved Oxygen levels in most freshwater ecosystems?",
                    options = new string[] { "1-3 mg/L", "5-9 mg/L", "10-14 mg/L", "0-2 mg/L" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What happens to aquatic life when DO levels drop below 2 mg/L?",
                    options = new string[] { "It supports the growth of fish and plants", "It leads to hypoxia, which can cause fish kill", "It improves water quality", " It increases biodiversity" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "Why might DO levels above 9 mg/L occur in water?",
                    options = new string[] { "Low oxygen concentration", "High photosynthetic activity during daylight hours", "Water pollution", "Decomposition of organic matter" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "Which reagent is used first when conducting a Dissolved Oxygen test with a chemical kit?",
                    options = new string[] { " Sulfuric Acid", "Alkaline Iodide Azide", " Manganese Sulfate", "Distilled Water" },
                    correctAnswerIndex = 2
                },
                new QuizQuestion
                {
                    text = ".What is the relationship between temperature and Dissolved Oxygen levels in water?",
                    options = new string[] { " Higher temperatures increase DO levels", "Lower temperatures decrease DO levels", "Lower temperatures increase DO levels", " Temperature has no effect on DO levels" },
                    correctAnswerIndex = 2
                }
            },
                        // Test 3 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What does an alkalinity test measure?",
                    options = new string[] { "How well water can resist pH changes", "The amount of dissolved oxygen in water", "The temperature of the water", "The amount of salt in water" },
                    correctAnswerIndex = 0
                },
                new QuizQuestion
                {
                    text = "How does low alkalinity (<10 mg/L) affect water bodies?",
                    options = new string[] { "It makes the water resistant to acidification", "It makes the water more sensitive to pH changes and prone to acidification", "It prevents pollution from affecting the water quality", "It helps aquatic plants grow faster" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What is the acceptable alkalinity level for most healthy water bodies?",
                    options = new string[] { "10-100 mg/L", "200-300 mg/L", "20-200 mg/L", " 0-10 mg/L" },
                    correctAnswerIndex = 2
                },
                new QuizQuestion
                {
                    text = "Why is it important to titrate slowly during an alkalinity test?",
                    options = new string[] { "To prevent the water from evaporating", "To ensure accurate measurement of the acid needed to change the pH", "To decrease the water temperature", "To reduce oxygen in the water sample" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "In an alkalinity test, after titration with acid (HCl), what does the total volume of acid used indicate?",
                    options = new string[] { "The amount of dissolved oxygen in the water", "The alkalinity level of the water sample", "The pH value of the water", "The presence of harmful chemicals" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What is the purpose of adding an alkalinity indicator, like phenolphthalein, in an alkalinity test?",
                    options = new string[] { "To neutralize the water", "To prevent oxygen from dissolving in the sample", "To cause a color change, indicating a reaction with the titration acid", "To remove dissolved solids from the sample" },
                    correctAnswerIndex = 2
                }
            },
                        // Test 4 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What does conductivity in water primarily measure?",
                    options = new string[] { "Oxygen content", "The ability to conduct electrical current, influenced by dissolved ions", "The amount of dissolved oxygen", "The temperature of the water" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What is indicated by low conductivity levels (<50 µS/cm) in water?",
                    options = new string[] { "High levels of pollution", "Very pure water with few dissolved ions", "Excessive nutrient enrichment", "Water is very warm" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = ".Which of the following factors can cause high conductivity (>500 µS/cm) in water bodies?",
                    options = new string[] { " Low oxygen levels", "Natural mineral deposits, pollution, or runoff", "Very low pH", "Lack of nutrients in the ecosystem" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = ".Why is it important to record the water temperature before conducting the conductivity test?",
                    options = new string[] { " Conductivity readings are temperature-dependent, and water needs to be between 20˚ and 30˚ C", "The temperature affects the oxygen content in water", "Temperature affects the number of dissolved ions", "To determine the water's pH level" },
                    correctAnswerIndex = 0
                },
                new QuizQuestion
                {
                    text = ".How can you adjust the reading of the conductivity meter during calibration if it does not match the standard solution value?",
                    options = new string[] { "Rinse the electrode again with distilled water", "Use a small screwdriver to adjust the calibration screw", "Submerge the probe deeper in the solution", " Wait for the water to cool down" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = ".What should you do if one or more conductivity measurements are not within 40 µS/cm of the average?",
                    options = new string[] { " Ignore the readings", "Recalibrate the meter", "Take fresh samples and repeat the measurements", "Adjust the pH of the water" },
                    correctAnswerIndex = 2
                }
            },
                        // Test 5 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What does water transparency measure?",
                    options = new string[] { "The amount of oxygen in the water", "The clarity of water, indicating the presence of suspended particles", "The temperature of water", "The level of acidity in water" },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "What is one of the possible causes of low transparency in water?",
                    options = new string[] { "High water temperature", " Increased sunlight penetration", " High levels of suspended particles, such as sediment or algae", "increased dissolved oxygen" },
                    correctAnswerIndex = 2
                },
                 new QuizQuestion
                {
                    text = "According to GLOBE values, what does a transparency reading of less than 1 meter suggest?",
                    options = new string[] { " Excellent water quality", "High levels of dissolved oxygen", "Significant pollution or issues affecting water clarity", " Water is too cold for measurement" },
                    correctAnswerIndex = 2
                },
                 new QuizQuestion
                {
                    text = "What is the importance of shading the Secchi disk during transparency measurement?",
                    options = new string[] { " To prevent the Secchi disk from being submerged", "To reduce glare and ensure accurate readings", " To cool down the water sample", " To prevent pollution from affecting the measurement" },
                    correctAnswerIndex = 1
                },
                 new QuizQuestion
                {
                    text = ".When marking the rope during transparency measurement, why is it important to ensure the marks are consistent?",
                    options = new string[] { "To standardize the distance from the water surface to the Secchi disk", "To measure the depth of the water", "To compare the distance between the observer and the water", "To avoid touching the rope with your hands" },
                    correctAnswerIndex = 0
                },
                 new QuizQuestion
                {
                    text = "What would be the most appropriate course of action if the transparency reading is consistently below 1 meter in a lake?",
                    options = new string[] { "T Add more Secchi disks to increase accuracy.", "Consider possible sources of pollution or sedimentation affecting water clarity.", "Move to a different location in the lake to take new measurements.", "Calibrate the Secchi disk before remeasuring" },
                    correctAnswerIndex = 1
                }
            },
                        // Test 6 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "What is the primary source of high nitrate levels in water bodies?",
                    options = new string[] { "Atmospheric pollution", " Industrial waste and sewage", " Agricultural runoff and fertilizers", "Oceanic currents" },
                    correctAnswerIndex = 2
                },
                new QuizQuestion
                {
                    text = "What is the effect of excessive nitrate levels on aquatic ecosystems?",
                    options = new string[] { "Increased oxygen levels in water", " Promotion of algal blooms, reducing oxygen levels", "Decrease in water pH", "Improved water clarity" },
                    correctAnswerIndex = 0
                },
                new QuizQuestion
                {
                    text = ".What happens if nitrate levels exceed 10 mg/L in a freshwater system?",
                    options = new string[] { "It may enhance plant growth without harmful effects", "It indicates high pollution levels, which can harm aquatic life", "It suggests balanced nutrient levels.", " It improves the oxygen content in the water." },
                    correctAnswerIndex = 1
                },
                new QuizQuestion
                {
                    text = "Which of the following conditions is most likely caused by high nitrate levels?",
                    options = new string[] { "Eutrophication", "Reduced nutrient levels", " Increased water pH", "Higher transparency" },
                    correctAnswerIndex = 0
                },
                new QuizQuestion
                {
                    text = "What is the primary effect of high nitrate levels on aquatic organisms?",
                    options = new string[] { "Increased reproductive rates", "Enhanced growth of phytoplankton", " Oxygen depletion leading to fish kills", "Stabilization of food webs" },
                    correctAnswerIndex = 0
                },
                new QuizQuestion
                {
                    text = "What is the acceptable range of nitrate levels for freshwater systems, according to GLOBE values?",
                    options = new string[] { "0 - 1 mg/L", "1 - 5 mg/L", "1 - 10 mg/L", "10 - 20 mg/L" },
                    correctAnswerIndex = 0
                }
            },
             // Test 7 Questions
            new QuizQuestion[]
            {
                new QuizQuestion
                {
                    text = "Which of the following helps to prevent water pollution?",
                    options = new string[] { "Recycling used oil instead of dumping it.", "Washing cars near rivers.", "Using more plastic products.", "Pouring chemicals down the drain." },
                    correctAnswerIndex = 0
                }

            }

        };

        // Reset MCQ UI on start
        mcqManager.ResetMCQ();
        nextButton.SetActive(false);  // Hide the next button initially
    }

    // Activate the test by index and start the image display process
    public void ActivateTest(int testIndex)
    {
        currentTestIndex = testIndex;

        // Deactivate the previous active test if any
        if (activeTest != null)
        {
            activeTest.gameObject.SetActive(false);
        }

        // Activate the selected test
        activeTest = testManagers[testIndex];
        activeTest.gameObject.SetActive(true);

        // Show the next button for image navigation
        nextButton.SetActive(true);
        isMCQPhase = false;
        resetFlag = false;
    }

    // Called when the "Next" button is pressed
    public void OnNextButtonPressed()
    {
        // If resetFlag is set, restart the test
        if (resetFlag)
        {
            RestartProcess();
            return;
        }

        if (!isMCQPhase)
        {
            // If in image viewing phase, show the next image
            if (activeTest != null)
            {
                activeTest.ShowNextImage();

                // Transition to MCQ phase if the last image is shown
                if (activeTest.IsLastImage())
                {
                    isMCQPhase = true;
                }
            }
        }
        else
        {
            // If in MCQ phase, start displaying questions
            nextButton.SetActive(false);
            currentQuestionIndex = 0;
            PrepareMCQ();
        }
    }

    // Prepare and display the MCQ questions for the active test
    private void PrepareMCQ()
    {
        // Display the next question
        if (currentQuestionIndex < questions[currentTestIndex].Length)
        {
            QuizQuestion currentQuestion = questions[currentTestIndex][currentQuestionIndex];

            // Debug log for tracking
            Debug.Log($"Displaying MCQ for Test {currentTestIndex + 1}, Question {currentQuestionIndex + 1}");

            mcqManager.DisplayMCQ(currentQuestion.text, currentQuestion.options, currentQuestion.correctAnswerIndex, currentTestIndex);
        }
        else
        {
            // Once all questions are done, set reset flag
            resetFlag = true;
            nextButton.SetActive(true);   // Show the "Next" button to restart the process
            mcqManager.ResetMCQ();        // Reset the MCQ UI
        }


    }

    // Called when a question is answered, move to the next question or reset if done
    public void OnQuestionAnswered()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex < questions[currentTestIndex].Length)
        {
            mcqManager.ResetMCQ();
            PrepareMCQ();
        }
        else
        {
            mcqManager.ResetMCQ();
            if (activeTest != null)
            {
                activeTest.ResetImages();
                activeTest.gameObject.SetActive(false);
            }

            nextButton.SetActive(false);

            // Check if all tests are completed
            if (currentTestIndex == testManagers.Length - 1) // If last test completed
            {
                ShowCompletionCanvas(); // Show the completion canvas
            }
        }
    }

    private void ShowCompletionCanvas()
    {
        completionCanvas.SetActive(true);
        // Optionally deactivate other canvases or interactions
    }

    // Restart the test process from the beginning
    private void RestartProcess()
    {
        if (activeTest != null)
        {
            activeTest.ResetImages();  // Reset the images for the active test
        }

        currentQuestionIndex = 0;      // Reset question index
        isMCQPhase = false;            // Reset MCQ phase
        resetFlag = false;             // Reset the flag

        nextButton.SetActive(true);    // Show the next button to start over
    }
}
