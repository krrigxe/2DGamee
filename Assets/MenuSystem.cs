using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    [Header("All Menus")]
    public GameObject pauseMenuUI;
  
    public GameObject endGameMenuUi;
    public static bool GameIsStoped = false;

   public damagable Damagable;



    private void Update()
    {
        if (!endGameMenuUi.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsStoped)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (!Damagable.IsAlive)
        {
            endGameMenuUi.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
      
        Time.timeScale = 1f;
        GameIsStoped = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
      
        Time.timeScale = 0f;
        GameIsStoped = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Debug.Log("restart");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("mainMenu");
        Time.timeScale = 1f;
        Debug.Log("its working");
    }
    public void QuitGame()
    {
        Debug.Log("qutting Game...");
        Application.Quit();
    }
    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        Time.timeScale = 1f;
    }
}
