using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class escape_button : MonoBehaviour
{
    [Header("Menu Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

  

    // Update method to check for Esc key press
    void Update()
    {
        
    }

    // Method to load level when a button is pressed
    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

   

    // Coroutine for async loading
    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 1.5f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }

}
