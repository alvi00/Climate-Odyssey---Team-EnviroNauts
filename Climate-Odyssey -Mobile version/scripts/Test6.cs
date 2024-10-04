using System.Collections;
using UnityEngine;

public class Test6 : MonoBehaviour
{
    public GameObject[] images; // Array of image GameObjects
    private int currentImageIndex = 0;

    private void Start()
    {
        SetActiveImage(currentImageIndex);
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

        // Show current image
        images[index].SetActive(true);
    }

    // This function will return true if the current image is the last one
    public bool IsLastImage()
    {
        return currentImageIndex >= images.Length - 1;
    }
}
