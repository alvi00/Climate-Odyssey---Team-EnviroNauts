using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSequenceManager : MonoBehaviour
{
    [SerializeField] private GameObject worldSpaceCanvas;  // The canvas with images
    [SerializeField] private GameObject screenOverlayCanvas;  // The canvas with the button
    [SerializeField] private List<GameObject> images;  // List of images to show
    [SerializeField] private Button nextButton;  // The button to go to the next image

    private int currentImageIndex = 0;  // Tracks the current image being shown

    private void Start()
    {
        // Ensure that the first image is active and the others are not
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(i == 0);  // Show only the first image at the start
        }

        // Add a listener to the button to trigger the NextImage method
        nextButton.onClick.AddListener(NextImage);
    }

    private void NextImage()
    {
        // Hide the current image
        images[currentImageIndex].SetActive(false);

        // Move to the next image
        currentImageIndex++;

        // Check if there are more images to show
        if (currentImageIndex < images.Count)
        {
            // Show the next image
            images[currentImageIndex].SetActive(true);
        }
        else
        {
            // Hide both canvases if all images are shown
            worldSpaceCanvas.SetActive(false);
            screenOverlayCanvas.SetActive(false);
        }
    }
}
