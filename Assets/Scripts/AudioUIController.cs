using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SFXSlider;
    [SerializeField] public GameObject panel;
    static int counter = 0;
    public void TogglePanel ()
    {
        if (counter % 2 == 1) 
            panel.SetActive(false);
        else 
            panel.SetActive(true);
        counter++;
    }
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(musicSlider.value);
    }
}
