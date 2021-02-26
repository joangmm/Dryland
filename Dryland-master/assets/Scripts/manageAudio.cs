using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manageAudio : MonoBehaviour
{
    public AudioSource BGaudio;
    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = BGaudio.volume;
    }

    void OnGUI()
    {
        //Makes the volume of the Audio match the Slider value
        BGaudio.volume = slider.value;
    }
}
