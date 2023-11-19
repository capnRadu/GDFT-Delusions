using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene 1");
    }

    public void Restart()
    {
        if (SceneManager.GetActiveScene().name == "Scene 1")
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene("Scene 1");
        }
        else if (SceneManager.GetActiveScene().name == "Scene 2")
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene("Scene 2");
        }
        else if (SceneManager.GetActiveScene().name == "Scene 3")
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            SceneManager.LoadScene("Scene 3");
        }
    }

    public void ExitMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Scene 0");
    }
}
