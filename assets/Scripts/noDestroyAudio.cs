using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noDestroyAudio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
