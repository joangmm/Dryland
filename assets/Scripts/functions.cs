using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class functions : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    public Player player;
    

    //setting menu
    public GameObject settingsMenu;

    //controllers menu
    public GameObject controllersMenu;

    //gameover menu
    public GameObject gameOverMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        controllersMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quit();
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
        /*
        if ( isAlive )
        {
            gameOver();
        }*/
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

    public void goToControllers()
    {
        pauseMenu.SetActive(false);
        controllersMenu.SetActive(true);
    }

    public void returnToPauseMenu()
    {
        controllersMenu.SetActive(false);
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

    public void gameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void gameWin()
    {
        SceneManager.LoadScene("GameWinL1");
    }
}