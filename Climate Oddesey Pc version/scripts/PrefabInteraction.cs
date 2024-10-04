using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabInteraction : MonoBehaviour
{
    public Canvas interactionCanvas;   // Reference to the Canvas
    private GameObject player;         // Reference to the player
    private bool isPlayerInRange = false;  // Check if the player is in range
    public GameObject prefabToDeactivate;  // The Prefab that will be deactivated

    void Start()
    {
        // Initially hide the Canvas
        interactionCanvas.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger zone
        if (other.CompareTag("Player"))
        {
            // Show the Canvas
            interactionCanvas.gameObject.SetActive(true);
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has left the trigger zone
        if (other.CompareTag("Player"))
        {
            // Hide the Canvas
            interactionCanvas.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        // Check if the player is in range and presses the 'E' key
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Deactivate the Prefab
            prefabToDeactivate.SetActive(false);

            // Award points
            ScoreManager.instance.AddPoints(20);

            // Hide the Canvas after pressing 'E'
            interactionCanvas.gameObject.SetActive(false);

            // Optionally, you can reset the isPlayerInRange flag
            isPlayerInRange = false;
        }
    }
}
