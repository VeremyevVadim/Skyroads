using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource[] soundSources;
    [SerializeField] private AudioSource[] musicSources;

    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    public float GetSoundVolume()
    {
        return soundSlider.value;
    }
    
    public float GetMusicVolume()
    {
        return musicSlider.value;
    }

    public void OnSoundChange()
    {
        foreach (var source in soundSources)
        {
            source.volume = soundSlider.value;
        }
    }
    
    public void OnMusicChange()
    {
        foreach (var source in musicSources)
        {
            source.volume = musicSlider.value;
        }
    }
}
