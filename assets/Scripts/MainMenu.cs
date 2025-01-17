using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource Bgaudio;

    public void playGame()
    {
        SceneManager.LoadScene("Level_1");
        //SceneManager.LoadScene( 1 );  //by passing the id
        //SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1);    //next scene specified in the order
    }

    public void quitGame()
    {
        // save any game data here
#if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
