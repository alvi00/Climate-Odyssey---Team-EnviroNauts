using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_game : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject PauseButton;

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1; // Ensure time scale is reset to 1 when the scene starts
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);
        isPaused = true;
        Debug.Log("pause button press");
        Debug.Log("Game Paused, Time.timeScale: " + Time.timeScale);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
        isPaused = false;
        Debug.Log("Resime button press");
        Debug.Log("Game Resumed, Time.timeScale: " + Time.timeScale);
    }

    public void ExitToMenu()
    {
        // Ensure time scale is reset to 1 when exiting to main menu or another scene
        Time.timeScale = 1;
        Debug.Log("Exit button press");
        Debug.Log("Exiting to menu, Time.timeScale reset to: " + Time.timeScale);
        // Load the main menu or another scene here, for example:
        // SceneManager.LoadScene("MainMenu");
    }
}
