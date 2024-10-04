using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    public GameObject[] images; // Array of image GameObjects
    private int currentImageIndex = 0;

    private void Start()
    {
        SetActiveImage(currentImageIndex); // Display the first image initially
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
            gameObject.SetActive(false); // Hide the canvas if all images are shown
        }
    }

    private void SetActiveImage(int index)
    {
        // Hide all images
        foreach (GameObject img in images)
        {
            img.SetActive(false);
        }

        // Show the current image
        if (index < images.Length)
        {
            images[index].SetActive(true);
        }
    }

    public bool IsLastImage()
    {
        return currentImageIndex >= images.Length - 1;
    }

    public void ResetImages()
    {
        currentImageIndex = 0; // Reset to the first image
        SetActiveImage(currentImageIndex); // Ensure the first image is shown again
        gameObject.SetActive(true); // Make sure the canvas is re-enabled
    }
}
