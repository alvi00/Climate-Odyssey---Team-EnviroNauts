using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class level_changer_Script : MonoBehaviour
{
    public void water_pollution()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void urbanization()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void flood()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void back()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
