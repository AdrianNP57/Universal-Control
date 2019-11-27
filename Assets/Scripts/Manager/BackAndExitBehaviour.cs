using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackAndExitBehaviour : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyUp("escape"))
        {
            BackAndExit();
        }
    }

    public void BackAndExit()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
