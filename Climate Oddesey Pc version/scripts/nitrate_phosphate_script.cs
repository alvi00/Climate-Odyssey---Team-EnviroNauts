using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitrate_phosphate_script : MonoBehaviour
{
    public GameObject press_for_np;
    public GameObject[] np_images; // Array to hold multiple nitrate/phosphate images
    public GameObject mcqPanel; // The panel for MCQs
    public mcq_manager_for_nitrate_phosphate mcqManager; // Reference to the nitrate/phosphate MCQ Manager

    private int currentImageIndex = 0; // To track which image is currently displayed
    private bool isPlayerInRange = false; // Track if the player is in range
    private bool mcqCompleted = false; // Flag to track if MCQ is completed

    void Start()
    {
        press_for_np.SetActive(false);
        mcqPanel.SetActive(false); // Initially hide the MCQ panel

        // Ensure all images are hidden at the start
        foreach (GameObject img in np_images)
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
                press_for_np.SetActive(false);
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
            press_for_np.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            press_for_np.SetActive(false);
            HideAllImages();
            currentImageIndex = 0;

            mcqPanel.SetActive(false);

            if (!mcqCompleted)
            {
                mcqManager.ResetMCQ();
            }
        }
    }

    private void ShowCurrentImage()
    {
        HideAllImages();
        if (currentImageIndex >= 0 && currentImageIndex < np_images.Length)
        {
            np_images[currentImageIndex].SetActive(true);
        }
    }

    private void NextImage()
    {
        if (currentImageIndex < np_images.Length - 1)
        {
            currentImageIndex++;
            ShowCurrentImage();
        }
        else
        {
            if (!mcqCompleted)
            {
                ShowMCQ();
            }
        }
    }

    private void ShowMCQ()
    {
        HideAllImages();
        mcqPanel.SetActive(true); // Show MCQ panel
        mcqManager.StartMCQ(); // Start the MCQ manager
    }

    private void HideAllImages()
    {
        foreach (GameObject img in np_images)
        {
            img.SetActive(false);
        }
    }

    public void CompleteMCQ()
    {
        press_for_np.SetActive(false);
        HideAllImages();
        currentImageIndex = 0;

        mcqPanel.SetActive(false); // Hide the MCQ panel
        mcqCompleted = true; // Mark MCQ as completed

        // Call the lab timer to indicate a test has been completed
        FindObjectOfType<lab_timer>().CompleteTest();
    }

}
