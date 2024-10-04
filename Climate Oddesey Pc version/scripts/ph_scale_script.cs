using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ph_scale_script : MonoBehaviour
{
    public GameObject press_for_ph_scale;
    public GameObject[] ph_images; // Array to store multiple pH scale images
    public GameObject mcqPanel; // The panel for MCQs
    public ph_scale_mcq_manager mcqManager; // Reference to the MCQ Manager

    private int currentImageIndex = 0;
    private bool isPlayerInRange = false; // Track if the player is in range
    private bool mcqCompleted = false; // Flag to track if MCQ is completed

    void Start()
    {
        press_for_ph_scale.SetActive(false);
        mcqPanel.SetActive(false); // Initially hide the MCQ panel

        // Ensure all images are hidden at the start
        foreach (GameObject img in ph_images)
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
                press_for_ph_scale.SetActive(false);
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
            press_for_ph_scale.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            press_for_ph_scale.SetActive(false);
            HideAllImages();
            currentImageIndex = 0;

            mcqPanel.SetActive(false); // Hide the MCQ panel

            // Reset MCQ only if it's not completed
            if (!mcqCompleted)
            {
                mcqManager.ResetMCQ();
            }
        }
    }

    private void ShowCurrentImage()
    {
        HideAllImages();
        if (currentImageIndex >= 0 && currentImageIndex < ph_images.Length)
        {
            ph_images[currentImageIndex].SetActive(true);
        }
    }

    private void NextImage()
    {
        if (currentImageIndex < ph_images.Length - 1)
        {
            currentImageIndex++;
        }
        else
        {
            // If all images are viewed, check if MCQ is completed
            if (!mcqCompleted)
            {
                ShowMCQ(); // Show MCQ if it hasn’t been completed yet
            }
        }

        ShowCurrentImage(); // Display the current image
    }

    private void ShowMCQ()
    {
        HideAllImages();
        mcqPanel.SetActive(true); // Show MCQ panel only after images are done
        mcqManager.StartMCQ(); // Start the MCQ manager
    }

    private void HideAllImages()
    {
        foreach (GameObject img in ph_images)
        {
            img.SetActive(false);
        }
    }

    // Call this after the MCQ is completed
    public void CompleteMCQ()
    {
        press_for_ph_scale.SetActive(false);
        HideAllImages();
        currentImageIndex = 0;

        mcqPanel.SetActive(false); // Hide the MCQ panel
        mcqCompleted = true; // Mark MCQ as completed

        // Call the lab timer to indicate a test has been completed
        FindObjectOfType<lab_timer>().CompleteTest();
    }

}
