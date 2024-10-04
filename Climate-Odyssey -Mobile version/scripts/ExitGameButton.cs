using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    // Optionally, you can have a button reference if you want to set it up via the script
    [SerializeField] private Button exitButton;

    private void Start()
    {
        // If you want to set it up via the script, uncomment the line below
        // exitButton.onClick.AddListener(ExitGame);

        // If the button is set up in the Unity Editor, just ensure this is called
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    // Method to exit the game
    public void ExitGame()
    {
#if UNITY_EDITOR
            // If running in the editor, stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running as a standalone build, quit the application
        Application.Quit();
#endif
    }
}
