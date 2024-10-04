using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisibilityController : MonoBehaviour
{
    void Start()
    {
        // Make the cursor visible
        Cursor.visible = true;

        // Ensure the cursor is not locked, so it can move freely
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        // Continuously make sure the cursor remains visible and unlocked
        if (!Cursor.visible)
        {
            Cursor.visible = true;
        }

        if (Cursor.lockState != CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
