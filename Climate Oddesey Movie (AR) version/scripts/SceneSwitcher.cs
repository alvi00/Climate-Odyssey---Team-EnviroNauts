using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement; // For handling scene transitions

public class SceneSwitcher : MonoBehaviour
{
    // Method to switch to a specific scene by name
    public void SwitchToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to switch to a specific scene after a delay
    public void SwitchToSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(SwitchSceneAfterDelay(sceneName, delay));
    }

    // Coroutine to handle delayed scene loading
    private IEnumerator SwitchSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // Example method to switch to a scene using a button click
    public void OnButtonPressed(string sceneName)
    {
        SwitchToScene(sceneName);
    }

    // Example method to switch scenes based on a timer running out (5 minutes)
    public void SwitchSceneAfterFiveMinutes(string sceneName)
    {
        SwitchToSceneWithDelay(sceneName, 300f); // 300 seconds = 5 minutes
    }
}
