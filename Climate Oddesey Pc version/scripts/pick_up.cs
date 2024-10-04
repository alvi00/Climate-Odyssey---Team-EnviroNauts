using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;  // Add this for Timeline functionality

public class pick_up : MonoBehaviour
{
    public GameObject pick_bucket;
    public GameObject pick_up_text;

    // Static variable to track if the bucket has been picked up
    public static bool bucketPickedUp = false;

    // Reference to the WaterSampleTimer script
    public WaterSampleTimer waterSampleTimer;

    // Reference to the PlayableDirector for the Timeline
    public PlayableDirector pickupTimeline;

    // References to the main gameplay character and the duplicate used for the Timeline
    public GameObject playerCharacter;
    public GameObject timelineCharacter; // Duplicate character for the Timeline

    // Reference to the interactable bucket (whose collider will be toggled)
    public GameObject interactBucket;

    // Cameras
    public GameObject all_time_camera;
    public GameObject timeline_camera;

    private Collider interactBucketCollider;  // Reference to the interactable bucket's collider

    void Start()
    {
        pick_bucket.SetActive(false);  // Ensure the bucket is initially hidden
        pick_up_text.SetActive(false);  // Ensure the pickup text is initially hidden

        // Ensure the Timeline character and the Timeline itself are not active at the start
        timelineCharacter.SetActive(false);
        pickupTimeline.gameObject.SetActive(false);

        // Get the Collider component of the interact bucket
        if (interactBucket != null)
        {
            interactBucketCollider = interactBucket.GetComponent<Collider>();
            if (interactBucketCollider != null)
            {
                interactBucketCollider.isTrigger = true;  // Ensure it's a trigger initially
            }
        }

        // Ensure both cameras are correctly initialized
        if (all_time_camera != null) all_time_camera.gameObject.SetActive(true);
        if (timeline_camera != null) timeline_camera.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pick_up_text.SetActive(true);  // Show pickup text when in trigger area

            // Desktop: If 'E' is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartTimeline();
            }

            // Mobile: If the screen is tapped
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                StartTimeline();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Hide the pickup text when leaving the trigger area
        pick_up_text.SetActive(false);
    }

    // Method to start the timeline
    private void StartTimeline()
    {
        // Hide the pickup text and start the Timeline
        pick_up_text.SetActive(false);
        StartCoroutine(PlayTimelineAndHandleCharacter());
    }

    // Coroutine to play the timeline, handle character visibility, and control the bucket's collider
    private IEnumerator PlayTimelineAndHandleCharacter()
    {
        // Hide the bucket and main gameplay character when the timeline starts
        pick_bucket.SetActive(false);  // Hide the bucket
        playerCharacter.SetActive(false);  // Hide the player character

        // Disable the interact bucket's collider trigger state
        if (interactBucketCollider != null)
        {
            interactBucketCollider.isTrigger = false;  // Disable the trigger
        }

        // Activate the Timeline duplicate character
        timelineCharacter.SetActive(true);

        // Switch cameras
        if (all_time_camera != null) all_time_camera.gameObject.SetActive(false);
        if (timeline_camera != null) timeline_camera.gameObject.SetActive(true);

        // Enable the Timeline GameObject and start playing the timeline
        pickupTimeline.gameObject.SetActive(true);
        pickupTimeline.Play();

        // Hide the pickup text during the timeline playback
        pick_up_text.SetActive(false);

        // Wait until the timeline has finished playing
        yield return new WaitUntil(() => pickupTimeline.state != PlayState.Playing);

        // After the timeline finishes:
        // - Show the gameplay character again
        // - Deactivate the timeline character and timeline itself
        playerCharacter.SetActive(true);
        timelineCharacter.SetActive(false);
        pickupTimeline.gameObject.SetActive(false);

        // Switch cameras back
        if (all_time_camera != null) all_time_camera.gameObject.SetActive(true);
        if (timeline_camera != null) timeline_camera.gameObject.SetActive(false);

        // Show the bucket after the Timeline finishes
        pick_bucket.SetActive(true);

        // Reset the interact bucket's collider to be a trigger again
        if (interactBucketCollider != null)
        {
            interactBucketCollider.isTrigger = true;  // Reset the trigger
        }

        // Deactivate the bucket GameObject if needed
        this.gameObject.SetActive(false);

        // Notify the timer script that the sample has been collected
        waterSampleTimer.CollectSample();

        // Set the flag to true when the bucket is picked up
        bucketPickedUp = true;
    }
}
