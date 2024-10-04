using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissolved_oxygen_Script : MonoBehaviour
{
    public GameObject press_for_do;
    public GameObject[] do_images; // Array to hold multiple DO images
    public GameObject mcqPanel; // The panel for MCQs
    public dissolved_oxygen_mcq_manager mcqManager; // Reference to the MCQ Manager

    private int currentImageIndex = 0; // To track which image is currently displayed
    private bool isPlayerInRange = false; // Track if the player is in range
    private bool mcqCompleted = false; // Flag to track if MCQ is completed

    void Start()
    {
        press_for_do.SetActive(false);
        mcqPanel.SetActive(false); // Initially hide the MCQ panel

        // Ensure all images are hidden at the start
        foreach (GameObject img in do_images)
        {
            img.SetActive(false);
        }
    }

    void Update()
    {
        // Only check input if the player is in range
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                press_for_do.SetActive(false);
                ShowCurrentImage();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                NextImage();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                HideAllImages();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            press_for_do.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            press_for_do.SetActive(false);
            HideAllImages(); // Hide all images when the player exits the trigger
            currentImageIndex = 0; // Reset the index so the array restarts when player comes back

            // Hide the MCQ panel when the player exits
            mcqPanel.SetActive(false);

            // Reset the MCQ only if it's not completed
            if (!mcqCompleted)
            {
                mcqManager.ResetMCQ();
            }
        }
    }

    private void ShowCurrentImage()
    {
        // Ensure only the current image is shown
        HideAllImages();
        if (currentImageIndex >= 0 && currentImageIndex < do_images.Length)
        {
            do_images[currentImageIndex].SetActive(true);
        }
    }

    private void NextImage()
    {
        if (currentImageIndex < do_images.Length - 1)
        {
            currentImageIndex++; // Move to the next image
            ShowCurrentImage();
        }
        else
        {
            // If all images are viewed, check if the MCQ has already been completed
            if (!mcqCompleted)
            {
                ShowMCQ();
            }
        }
    }

    private void ShowMCQ()
    {
        HideAllImages();
        mcqPanel.SetActive(true); // Show the MCQ panel when images are done
        mcqManager.StartMCQ(); // Start the MCQ system
    }

    private void HideAllImages()
    {
        foreach (GameObject img in do_images)
        {
            img.SetActive(false);
        }
    }

    // Call this method after the MCQ is completed
    public void CompleteMCQ()
    {
        press_for_do.SetActive(false);
        HideAllImages();
        currentImageIndex = 0;

        mcqPanel.SetActive(false); // Hide the MCQ panel
        mcqCompleted = true; // Mark MCQ as completed

        // Call the lab timer to indicate a test has been completed
        FindObjectOfType<lab_timer>().CompleteTest();
    }

}
