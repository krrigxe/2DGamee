using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public void playMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
        Time.timeScale = 1f;
    }
    public void LevelMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("qutting Game...");
        Application.Quit();
    }
}
