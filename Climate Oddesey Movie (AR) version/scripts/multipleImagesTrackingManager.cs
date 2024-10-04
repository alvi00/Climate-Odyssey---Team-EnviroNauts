using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class multipleImagesTrackingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabtospawn;
    [SerializeField] private GameObject firstCanvas; // Reference to the first canvas
    [SerializeField] private GameObject secondCanvas; // Reference to the second canvas
    private ARTrackedImageManager _arTrackedImageManager;
    private Dictionary<string, GameObject> _arObjects;
    private HashSet<string> _scannedImages; // Keep track of already scanned images

    private const string bucketVariantPrefabName = "Bucket Variant"; // Constant for the "Bucket Variant" prefab name

    private void Awake()
    {
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        _arObjects = new Dictionary<string, GameObject>();
        _scannedImages = new HashSet<string>(); // Initialize the set
    }

    private void Start()
    {
        _arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        // Spawn the game objects per image and hide them
        foreach (GameObject prefab in prefabtospawn)
        {
            GameObject newArObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newArObject.name = prefab.name;
            newArObject.gameObject.SetActive(false);
            _arObjects.Add(newArObject.name, newArObject);
        }

        // Hide both canvases initially
        if (firstCanvas != null)
        {
            firstCanvas.SetActive(false);
        }

        if (secondCanvas != null)
        {
            secondCanvas.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // We won't set the object inactive anymore when the image is removed
        }
    }

    public void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (_scannedImages.Contains(imageName))
        {
            return; // The prefab has already been placed
        }

        if (trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
        {
            if (_arObjects.ContainsKey(imageName))
            {
                _arObjects[imageName].gameObject.SetActive(true);
                _arObjects[imageName].transform.position = trackedImage.transform.position;
                _scannedImages.Add(imageName); // Mark image as scanned

                // Check if all prefabs are visible
                if (AllPrefabsVisible())
                {
                    if (firstCanvas != null)
                    {
                        firstCanvas.SetActive(true); // Show the first canvas
                    }
                }
            }
        }
    }

    private bool AllPrefabsVisible()
    {
        foreach (var prefab in prefabtospawn)
        {
            if (!_scannedImages.Contains(prefab.name))
            {
                return false; // Return false if any prefab is not scanned
            }
        }
        return true; // All prefabs are visible
    }

    // Method to show the second canvas when the button is pressed
    public void ShowSecondCanvas()
    {
        if (firstCanvas != null)
        {
            firstCanvas.SetActive(false); // Hide the first canvas
        }

        if (secondCanvas != null)
        {
            secondCanvas.SetActive(true); // Show the second canvas
        }
    }

    // Method to hide the "Bucket Variant" prefab after the water collection button is pressed
    public void HideBucketVariantPrefab()
    {
        if (_arObjects.ContainsKey(bucketVariantPrefabName))
        {
            _arObjects[bucketVariantPrefabName].SetActive(false); // Hide the "Bucket Variant" prefab
        }
    }
}
