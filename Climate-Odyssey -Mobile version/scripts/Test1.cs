using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Test1 : MonoBehaviour
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
            // All images displayed, hide the canvas
            gameObject.SetActive(false);
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
