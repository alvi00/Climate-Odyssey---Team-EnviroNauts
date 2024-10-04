using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainMenu : MonoBehaviour
{
    // Reference to the button that will load the main menu
    public Button loadMenuButton;

    void Start()
    {
        // Assign the button's onClick event to the LoadMainMenu method
        if (loadMenuButton != null)
        {
            loadMenuButton.onClick.AddListener(LoadMainMenuScene);
        }
    }

    // Method to load the main menu scene
    private void LoadMainMenuScene()
    {
        // Change "MainMenu" to the actual name of your main menu scene
        SceneManager.LoadScene("Main Menu");
    }
}
