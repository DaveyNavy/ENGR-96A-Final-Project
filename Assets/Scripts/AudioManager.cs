using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [System.Serializable]
    public struct Sound
    {

        public string name;
        public AudioClip clip;
    }
    public Sound[] music, sfx;
    public AudioSource musicSource, sfxSource;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("Theme");
    }
    void PlayMusic(string name)
    {
        foreach (Sound s in music)
        {
            if (s.name == name)
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }
    }
    public void PlaySFX(string name)
    {
        foreach (Sound s in sfx)
        {
            if (s.name == name)
            {
                sfxSource.clip = s.clip;
                sfxSource.Play();
            }
        }
    }
    public void ToggleMusic()
    {
        if (musicSource.mute)
        {
            musicSource.mute = false;
        }
        else
        {
            musicSource.mute = true;
        }
    }
    public void ToggleSFX()
    {
        if (sfxSource.mute)
        {
            sfxSource.mute = false;
        }
        else
        {
            sfxSource.mute = true;
        }
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}