using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector playableDirector;

    // GameObjects to control
    public GameObject[] canvases; // Array to hold the Canvas objects
    public GameObject gameplayCamera; // The camera for gameplay
    public GameObject timelineCamera; // The camera used in the timeline
    public GameObject timelineCharacter; // The character used in the timeline

    void Start()
    {
        // Deactivate the UI Canvases and the gameplay camera
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(false);
        }
        gameplayCamera.SetActive(false);

        // Play the timeline
        playableDirector.Play();

        // Subscribe to the stopped event to know when the timeline ends
        playableDirector.stopped += OnCutsceneFinished;
    }

    void OnCutsceneFinished(PlayableDirector director)
    {
        // Unsubscribe from the stopped event
        director.stopped -= OnCutsceneFinished;

        // Reactivate the UI Canvases and the gameplay camera
        foreach (GameObject canvas in canvases)
        {
            canvas.SetActive(true);
        }
        gameplayCamera.SetActive(true);

        // Deactivate the timeline camera and character
        timelineCamera.SetActive(false);
        timelineCharacter.SetActive(false);
    }
}
