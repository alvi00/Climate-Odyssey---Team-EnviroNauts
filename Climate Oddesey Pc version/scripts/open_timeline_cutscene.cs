using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class opne_timeline_cutscene : MonoBehaviour
{
    public PlayableDirector playableDirector;

    // GameObjects to control
    public GameObject[] canvases; // Array to hold the Canvas objects
    public GameObject gameplayCamera; // The camera for gameplay
    public GameObject timelineCamera; // The camera used in the timeline
    public GameObject timelineCharacter; // The character used in the timeline
    public GameObject mainCharacter; // The main character used for gameplay
    public GameObject cylender1; // The bucket that should be activated after the cutscene
    public GameObject cylender2; // Another game object to be activated after the cutscene
    public Button skipButton; // The skip button
    public GameObject editedthings; // Any additional game objects to activate
    public GameObject additionalCanvas; // New canvas to show after cutscene or skip
    public ImageSequenceController imageSequenceController; // Reference to ImageSequenceController

    void Start()
    {
        // Deactivate UI Canvases, gameplay camera, main character, bucket, and other game object
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
        gameplayCamera.SetActive(false);
        mainCharacter.SetActive(false);
        cylender1.SetActive(false);
        cylender2.SetActive(false);
        additionalCanvas.SetActive(false); // Ensure the new canvas is hidden initially

        // Activate the timeline camera and character
        timelineCamera.SetActive(true);
        timelineCharacter.SetActive(true);

        // Set the speed of the timeline (optional)
        playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1.5f);

        // Play the timeline
        playableDirector.Play();

        // Subscribe to the stopped event to know when the timeline ends
        playableDirector.stopped += OnCutsceneFinished;

        // Show the skip button during the timeline
        skipButton.gameObject.SetActive(true);

        // Add listener to skip button
        skipButton.onClick.AddListener(SkipTimeline);
    }

    void OnCutsceneFinished(PlayableDirector director)
    {
        // Unsubscribe from the stopped event
        director.stopped -= OnCutsceneFinished;

        // Handle post-timeline actions (reactivate UI Canvases, etc.)
        HandlePostCutsceneActions();
    }

    // Function to skip the timeline
    void SkipTimeline()
    {
        // Stop the timeline
        playableDirector.Stop();

        // Handle post-timeline actions
        HandlePostCutsceneActions();
    }

    // Consolidated function to handle actions after the timeline ends or is skipped
    void HandlePostCutsceneActions()
    {
        // Reactivate UI Canvases and gameplay camera
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(true);
        }
        gameplayCamera.SetActive(true);

        // Deactivate the timeline camera and character
        timelineCamera.SetActive(false);
        timelineCharacter.SetActive(false);

        // Activate the main character, bucket, and other game object
        mainCharacter.SetActive(true);
        cylender1.SetActive(true);
        cylender2.SetActive(true);

        // Show the additional canvas
        additionalCanvas.SetActive(true);

        // Activate any additional game objects
        editedthings.SetActive(true);

        // Show the image sequence canvas
        imageSequenceController.ShowFirstImage(); // Ensure this triggers the image sequence

        // Hide the skip button after the cutscene is done
        skipButton.gameObject.SetActive(false);
    }
}
