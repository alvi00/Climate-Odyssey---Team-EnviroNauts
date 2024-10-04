using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class go_to_lab_script : MonoBehaviour
{
    public GameObject text_for_lab;
    public GameObject no_bucket_warning_text; // A text object to warn the player if they haven't picked up the bucket

    void Start()
    {
        text_for_lab.SetActive(false);
        no_bucket_warning_text.SetActive(false); // Initially hide the warning text
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (pick_up.bucketPickedUp)
            {
                // If the bucket is picked up, show the enter lab text
                text_for_lab.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    text_for_lab.SetActive(false);
                    SceneManager.LoadSceneAsync(5);
                }
            }
            else
            {
                // If the bucket has not been picked up, show a warning text
                no_bucket_warning_text.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        text_for_lab.SetActive(false);
        no_bucket_warning_text.SetActive(false); // Hide warning text when player leaves the trigger area
    }
}
