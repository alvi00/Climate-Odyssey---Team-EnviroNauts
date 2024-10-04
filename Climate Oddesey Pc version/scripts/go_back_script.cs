using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class go_back_script : MonoBehaviour
{
    public void go_back_to_main_menu()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
