using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageSequenceController : MonoBehaviour
{
    public GameObject[] images;  // Array to hold the images
    public Button showImagesButton;  // Reference to the button to start showing images
    public Button nextButton;    // Reference to the "Next" button
    public Button playButton;    // Reference to the "Play" button
    private int currentImageIndex = 0;  // Track the current image
    public WaterSampleTimer waterSampleTimer;  // Reference to the WaterSampleTimer script

    void Start()
    {
        // Initially hide all images
        for (int i = 0; i < images.Length; i++)
        {
            images[i].SetActive(false);
        }

        // Assign button functionality
        showImagesButton.onClick.AddListener(ShowFirstImage);
        nextButton.onClick.AddListener(ShowNextImage);
        playButton.onClick.AddListener(StartTimerAndHideImages);

        // Initially hide the next and play buttons
        nextButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check for keyboard input for desktop
        HandleInput();
    }

    void HandleInput()
    {
        // Desktop: If 'N' is pressed, go to the next image
        if (Input.GetKeyDown(KeyCode.N) && nextButton.gameObject.activeSelf)
        {
            ShowNextImage();
        }

        // Desktop: If 'E' is pressed, start the timer and hide images
        if (Input.GetKeyDown(KeyCode.E) && playButton.gameObject.activeSelf)
        {
            StartTimerAndHideImages();
        }
    }

    public void ShowFirstImage()
    {
        // Reset image sequence and display the first image
        currentImageIndex = 0;
        images[currentImageIndex].SetActive(true);

        // Hide the showImagesButton, enable nextButton
        showImagesButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    void ShowNextImage()
    {
        if (currentImageIndex < images.Length - 1)
        {
            images[currentImageIndex].SetActive(false);
            currentImageIndex++;
            images[currentImageIndex].SetActive(true);

            // If the last image is reached, show the Play button and hide the Next button
            if (currentImageIndex == images.Length - 1)
            {
                nextButton.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);
            }
        }
    }

    void StartTimerAndHideImages()
    {
        HideAllImages();

        // Check if the sample has been collected before starting the timer
        if (!waterSampleTimer.sampleCollected)
        {
            waterSampleTimer.StartTimer();
        }

        // Hide the Play button after starting the process
        playButton.gameObject.SetActive(false);
    }

    void HideAllImages()
    {
        foreach (GameObject image in images)
        {
            image.SetActive(false);
        }

        // Ensure buttons are properly hidden after the sequence
        nextButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);

        // Reactivate the showImagesButton for replaying
        showImagesButton.gameObject.SetActive(true);
    }
}
