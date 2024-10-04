using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class last_image_manager : MonoBehaviour
{
    public GameObject[] labImages; // Array to hold multiple lab images
    public GameObject mcqPanel; // The panel for MCQs
    public last_mcq_manager mcqManager; // Reference to the last_mcq_manager

    private int currentImageIndex = 0; // To track which image is currently displayed
    private bool tasksCompleted = false; // Ensure images and MCQ start only after tasks are completed

    void Start()
    {
        mcqPanel.SetActive(false); // Initially hide the MCQ panel

        // Ensure all images are hidden at the start
        foreach (GameObject img in labImages)
        {
            img.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the player presses 'N' to move to the next image, but only if tasks are completed
        if (tasksCompleted && Input.GetKeyDown(KeyCode.N))
        {
            ShowNextImage();
        }
    }

    // This method will be called by the lab_timer when the tasks are completed
    public void StartImageSequence()
    {
        tasksCompleted = true; // Allow image sequence to begin
        currentImageIndex = 0; // Reset the image index
        ShowCurrentImage(); // Show the first image
    }

    private void ShowCurrentImage()
    {
        HideAllImages(); // Hide all images first

        // Show the current image if it's within the valid range
        if (currentImageIndex >= 0 && currentImageIndex < labImages.Length)
        {
            labImages[currentImageIndex].SetActive(true);
        }
    }

    private void ShowNextImage()
    {
        // Hide the current image and increment the index
        if (currentImageIndex < labImages.Length - 1)
        {
            currentImageIndex++;
            ShowCurrentImage();
        }
        else
        {
            ShowMCQ(); // If all images are shown, start the MCQ
        }
    }

    private void ShowMCQ()
    {
        HideAllImages(); // Hide all images when showing the MCQ
        mcqPanel.SetActive(true); // Show the MCQ panel
        mcqManager.StartMCQ(); // Start the MCQ sequence
    }

    private void HideAllImages()
    {
        // Hide all images
        foreach (GameObject img in labImages)
        {
            img.SetActive(false);
        }
    }
}
