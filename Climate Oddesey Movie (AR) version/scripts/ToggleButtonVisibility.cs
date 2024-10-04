using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonVisibility : MonoBehaviour
{
    public Button toggleButton;  // The button used to toggle visibility
    public Button otherButton;   // Another button that activates after all buttons are clicked
    public GameObject[] buttons; // Array of buttons to show/hide

    private bool areButtonsActive = false;  // Track current visibility state
    private bool[] buttonPressedStates;     // Track if each button is pressed

    private void Start()
    {
        // Initialize the pressed states array based on the number of buttons
        buttonPressedStates = new bool[buttons.Length];

        // Set initial state
        SetButtonsActive(areButtonsActive);

        // Add listener to toggle button
        toggleButton.onClick.AddListener(ToggleButtons);

        // Add listeners to individual buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;  // Capture the current index for the closure
            buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClicked(index));
        }

        // Make sure the other button is initially inactive
        otherButton.gameObject.SetActive(false);
    }

    // Method to toggle the visibility of the buttons
    private void ToggleButtons()
    {
        areButtonsActive = !areButtonsActive;  // Toggle state
        SetButtonsActive(areButtonsActive);
    }

    // Method to set the active state of the buttons
    private void SetButtonsActive(bool isActive)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(isActive);
        }
    }

    // Method called when any button is clicked
    private void OnButtonClicked(int index)
    {
        buttonPressedStates[index] = true;  // Mark the button as pressed

        // Check if all buttons have been pressed
        if (AllButtonsPressed())
        {
            // Deactivate the toggle button and the other buttons
            toggleButton.gameObject.SetActive(false);
            SetButtonsActive(false);  // Hide all buttons

            // Activate the other button
            otherButton.gameObject.SetActive(true);
        }
    }

    // Method to check if all buttons have been pressed
    private bool AllButtonsPressed()
    {
        foreach (bool isPressed in buttonPressedStates)
        {
            if (!isPressed)
                return false;  // Return false if any button hasn't been pressed yet
        }
        return true;  // All buttons have been pressed
    }
}
