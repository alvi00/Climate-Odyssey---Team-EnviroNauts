using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string text; // Question text
    public string[] options; // Array of 4 options
    public int correctAnswerIndex; // Index of the correct answer (0-3)
}
