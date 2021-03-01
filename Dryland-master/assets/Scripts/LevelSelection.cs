using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    static GameObject _music = null;

    // Start is called before the first frame update
    void Start()
    {
        _music = GameObject.FindGameObjectWithTag("MenuMusic");
    }

    public void level1()
    {
        Destroy(_music);
        SceneManager.LoadScene("Level_1");
    }

    public void level2()
    {
        Destroy(_music);
        SceneManager.LoadScene("Level_2");
    }
}
