using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class main_menu_script : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
