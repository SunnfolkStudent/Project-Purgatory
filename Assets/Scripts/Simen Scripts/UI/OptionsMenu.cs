using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider VolumeSlider;  //Used for the slider in the options menu
    public AudioMixer mixer;

    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }
}
