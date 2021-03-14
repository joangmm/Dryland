using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageAudio : MonoBehaviour
{
    private AudioSource BGaudio;
    private Slider slider;

    void Awake()
    {
        BGaudio = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();

        slider = GetComponent<Slider>();
        slider.value = BGaudio.volume;
    }

    void OnGUI()
    {
        //Makes the volume of the Audio match the Slider value
        BGaudio.volume = slider.value;
    }
}
