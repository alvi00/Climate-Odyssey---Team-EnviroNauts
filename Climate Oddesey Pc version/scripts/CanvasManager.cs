using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    public Canvas canvas1;   // First canvas with an image and a button
    public Canvas canvas2;   // Second canvas with 6 buttons
    public Canvas imageCanvas;  // Canvas that shows the image and close button
    public Image displayImage; // The image to display in the imageCanvas
    public Sprite[] imageOptions; // Array of images for each button in canvas2
    public Button resetButton; // The button to reset everything

    private int currentImageIndex = 0;

    void Start()
    {
        // Initially hide all canvases except the first one
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        imageCanvas.gameObject.SetActive(false);

        // Assign the reset button to its function
        resetButton.onClick.AddListener(ResetAll);
    }

    // Function to show the first canvas
    public void ShowCanvas1()
    {
        canvas1.gameObject.SetActive(true);
    }

    // Function to move from first canvas to second
    public void ShowCanvas2()
    {
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(true);
    }

    // Function to show image and button when any button in canvas2 is clicked
    public void ShowImage(int imageIndex)
    {
        currentImageIndex = imageIndex;
        displayImage.sprite = imageOptions[currentImageIndex]; // Set the selected image
        imageCanvas.gameObject.SetActive(true); // Show the image canvas
    }

    // Function to close the image canvas
    public void CloseImageCanvas()
    {
        imageCanvas.gameObject.SetActive(false); // Hide the image canvas
    }

    // Function to reset everything
    public void ResetAll()
    {
        // Hide all canvases
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        imageCanvas.gameObject.SetActive(false);

        // Reset image index and any other variables as needed
        currentImageIndex = 0;

        // Optionally, if you want to reset any displayed image or UI element:
        displayImage.sprite = null; // Clear the displayed image, or set it to a default
    }
}
