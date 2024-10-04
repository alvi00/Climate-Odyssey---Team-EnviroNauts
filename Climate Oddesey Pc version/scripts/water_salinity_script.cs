using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_salinity_script : MonoBehaviour
{
    public GameObject press_for_ws;
    public GameObject[] ws_images; // Array to hold multiple water salinity images
    public GameObject mcqPanel; // The panel for MCQs
    public mcq_manager_water_salinity_script mcqManager; // Reference to the MCQ Manager

    private int currentImageIndex = 0;
    private bool isPlayerInRange = false; // Track if the player is in range
    private bool mcqCompleted = false; // Flag to track if MCQ is completed

    void Start()
    {
        press_for_ws.SetActive(false);
        mcqPanel.SetActive(false); // Initially hide the MCQ panel

        // Ensure all images are hidden at the start
        foreach (GameObject img in ws_images)
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
                press_for_ws.SetActive(false);
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
            press_for_ws.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            press_for_ws.SetActive(false);
            HideAllImages();
            currentImageIndex = 0; // Reset the image index

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
        HideAllImages();
        if (currentImageIndex >= 0 && currentImageIndex < ws_images.Length)
        {
            ws_images[currentImageIndex].SetActive(true);
        }
    }

    private void NextImage()
    {
        if (currentImageIndex < ws_images.Length - 1)
        {
            currentImageIndex++;
        }
        else
        {
            // If all images are viewed, check if the MCQ has already been completed
            if (!mcqCompleted)
            {
                // Show the MCQ only if it hasn't been completed
                ShowMCQ();
            }
        }

        ShowCurrentImage();
    }

    private void ShowMCQ()
    {
        HideAllImages();
        mcqPanel.SetActive(true); // Show the MCQ panel when images are done
        mcqManager.StartMCQ(); // Start the MCQ system
    }

    private void HideAllImages()
    {
        foreach (GameObject img in ws_images)
        {
            img.SetActive(false);
        }
    }

    // Call this method after the MCQ is completed
    public void CompleteMCQ()
    {
        press_for_ws.SetActive(false);
        HideAllImages();
        currentImageIndex = 0;

        mcqPanel.SetActive(false); // Hide the MCQ panel
        mcqCompleted = true; // Mark MCQ as completed

        // Call the lab timer to indicate a test has been completed
        FindObjectOfType<lab_timer>().CompleteTest();
    }
}
