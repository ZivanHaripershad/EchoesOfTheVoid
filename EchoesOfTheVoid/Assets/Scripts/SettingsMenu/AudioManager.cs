using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public static AudioManager Instance;

    public Sound[] musicSounfs, sfxSounfs;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Start()
    {
        //PlayMusic("Background");
    }

    public void PlayMusic(string clipname)
    {
        Sound s = Array.Find(musicSounfs, x => x.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist");
            return;
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipname)
    {
        Sound s = Array.Find(sfxSounfs, x => x.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist");
            return;
        }
        else
        {
           sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void sfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void MusicVolumeControl()
    {
        MusicVolume(musicSlider.value);
    }

    public void sfxVolumeControl()
    {
        sfxVolume(sfxSlider.value);
    }


}
