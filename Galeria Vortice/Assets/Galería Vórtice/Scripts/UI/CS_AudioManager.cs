using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_AudioManager : MonoBehaviour
{
    public static CS_AudioManager instance;

    public string BGMusic;

    public CS_Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    //If there is no Audio Manager, add this, otherwise, destroy this
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //On start play the background music
    private void Start()
    {
        PlayMusic(BGMusic);
    }

    //To use any play in the script add CS_AudioManager.instance.PlayMusic("Name");
    //To stop in the script add CS_AudioManager.instance.musicSource.Stop();
    public void PlayMusic(string name)
    {
        CS_Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Music " + name + " not found!");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    //To use any play in the script add CS_AudioManager.instance.PlaySFX("Name");
    //To stop in the script add CS_AudioManager.instance.sfxSource.Stop();
    public void PlaySFX(string name)
    {
        CS_Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX " + name + " not found!");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    //Mutes and unmutes the music source
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    //Mutes and unmutes the sfx source
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        instance.PlaySFX("UI");
    }

    //Changes the volume of the music
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume * volume;
    }

    //Changes the volume of the SFX
    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume * volume;
    }
}
