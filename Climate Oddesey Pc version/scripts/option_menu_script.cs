using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class option_menu_script : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    void Start()
    {
        // Get all available screen resolutions
        resolutions = Screen.resolutions;

        // Clear the dropdown options to avoid duplicate entries
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Format the resolution as a string
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // Check if this is the current resolution
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Add options to the dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // Set the screen resolution based on the selected dropdown option
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        // Set the audio volume
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        // Set fullscreen mode
        Screen.fullScreen = isFullscreen;
    }
}
