using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class asyncmanager_scipt_from_level_change : MonoBehaviour
{
    [Header("Menu Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject main_menu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string leveltoload)
    {
        main_menu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(loadLevelAsync(leveltoload));
    }

    IEnumerator loadLevelAsync(string leveltoload)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(leveltoload);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 1.5f);
            loadingSlider.value = progressValue;
            yield return null;
        }

    }
}
