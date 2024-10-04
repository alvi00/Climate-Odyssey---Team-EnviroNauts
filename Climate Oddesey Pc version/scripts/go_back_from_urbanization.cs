using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go_back_from_urbanization : MonoBehaviour
{
    void Update()
    {
        // Check if the "Esc" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call the function to go back to the main menu
            go_back_to_main_menu();
        }
    }

    public void go_back_to_main_menu()
    {
        // Load the main menu scene (assuming it's at index 1)
        SceneManager.LoadSceneAsync(1);
    }
}
