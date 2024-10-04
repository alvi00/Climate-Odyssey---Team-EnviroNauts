using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScaleWithDistance : MonoBehaviour
{
    public Transform cameraTransform;
    public float scaleFactor = 1.0f;  // Factor to control how much the text scales with distance
    public float minScale = 0.1f;      // Minimum scale limit
    public float maxScale = 2.0f;      // Maximum scale limit

    void Update()
    {
        // Get the distance between the text and the camera
        float distance = Vector3.Distance(transform.position, cameraTransform.position);

        // Calculate the new scale based on distance and the scaling factor
        float newScale = distance * scaleFactor;

        // Clamp the scale between the minimum and maximum values
        newScale = Mathf.Clamp(newScale, minScale, maxScale);

        // Apply the new scale to the object
        transform.localScale = Vector3.one * newScale;
    }
}
