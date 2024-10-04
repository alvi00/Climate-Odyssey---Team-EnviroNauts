using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using System.Collections; // Required for Coroutines

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timeline;  // Assign your PlayableDirector in the Inspector
    public string nextSceneName;       // Set the name of the next scene in the Inspector

    // Called when the button is pressed
    public void OnButtonPress()
    {
        // Start the timeline
        timeline.Play();

        // Start a coroutine to wait for the timeline to finish
        StartCoroutine(WaitForTimeline());
    }

    // Coroutine to wait until the timeline finishes playing
    private IEnumerator WaitForTimeline()
    {
        Debug.Log("Timeline started.");

        // Wait while the timeline is playing
        while (timeline.state == PlayState.Playing)
        {
            yield return null; // Wait for the next frame
        }

        Debug.Log("Timeline finished.");

        // Once the timeline is done, load the next scene
        LoadNextScene();
    }

    // Function to load the next scene, ensuring single mode
    private void LoadNextScene()
    {
        // Load the next scene in Single mode to make sure the current scene is completely unloaded
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }

    // This method is called when the scene is reloaded or when the object is enabled
    void OnEnable()
    {
        // Reset the timeline to its start state to ensure it's ready to play again
        if (timeline != null)
        {
            timeline.Stop();
            timeline.time = 0; // Reset timeline to the beginning
        }
    }

    // Make sure to stop all running coroutines when the object is disabled
    void OnDisable()
    {
        StopAllCoroutines(); // Stop any coroutines that may still be running
    }
}
