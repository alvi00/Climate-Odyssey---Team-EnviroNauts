using System.Collections;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public GameObject[] images;       // Array of image GameObjects on Canvas1
    public GameObject canvas2;        // Reference to Canvas2 (MCQ Questions)
    public GameObject canvas3;        // Reference to Canvas3 (MCQ Answer buttons)
    private int currentImageIndex = 0;
    private bool isMCQPhase = false;

    private void Start()
    {
        SetActiveImage(currentImageIndex);
        canvas2.SetActive(false); // Initially hide MCQ question canvas
        canvas3.SetActive(false); // Initially hide MCQ answer buttons
    }

    public void ShowNextImage()
    {
        if (currentImageIndex < images.Length - 1)
        {
            currentImageIndex++;
            SetActiveImage(currentImageIndex);
        }
        else
        {
            // When the last image is displayed, trigger the MCQ phase
            isMCQPhase = true;
            canvas2.SetActive(true); // Show MCQ question canvas
            canvas3.SetActive(true); // Show MCQ answer buttons
            gameObject.SetActive(false); // Hide image canvas (Canvas1)
        }
    }

    private void SetActiveImage(int index)
    {
        // Hide all images
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }

        // Show current image
        images[index].SetActive(true);
    }

    public bool IsLastImage()
    {
        return currentImageIndex >= images.Length - 1;
    }

    public void ResetImages()
    {
        currentImageIndex = 0;
        SetActiveImage(currentImageIndex);
        gameObject.SetActive(true); // Reactivate Canvas1
    }
}
