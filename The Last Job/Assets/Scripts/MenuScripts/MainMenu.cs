using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject options;

    public GameObject pauseMenu;
    public bool isPaused=false;

    private void Start()
    {        bool IsFullscreee = (PlayerPrefs.GetInt("fullscreen") == 1) ? true : false;
    }

    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Pause");
            if (isPaused)
            {
                Cursor.visible = true;
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PlayGame()
    { 
       
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }



    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }


   
}
