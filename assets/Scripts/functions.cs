using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class functions : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
            {
                resumeGame();
            } else
            {
                pauseGame();
            }
        }
    }

    public GameObject pauseMenu;
    public static bool isPaused;

    //setting menu
    public GameObject settingsMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void pauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void goToSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void returnToPauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void quitGame()
    {
        quit();
    }

    private void quit()
    {
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}