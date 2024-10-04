using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // Required for Button functionality

public class ToggleCanvasOnClick : MonoBehaviour
{
    public Button yourButton; // Reference to the button
    public Canvas yourCanvas; // Reference to the canvas

    void Start()
    {
        // Initially hide the Canvas
        yourCanvas.gameObject.SetActive(false);

        // Add a listener to the button to call ToggleCanvas when clicked
        yourButton.onClick.AddListener(ToggleCanvas);
    }

    // Method to toggle the Canvas's visibility
    void ToggleCanvas()
    {
        // Toggle the active state of the canvas
        yourCanvas.gameObject.SetActive(!yourCanvas.gameObject.activeSelf);
    }
}
