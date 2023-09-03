using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private Slider musicSlider, sfxSlider;
    private static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("Destroying instance");
            Destroy(gameObject);
            return;
        } else {
            Debug.Log("Creating instance");
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public static AudioManager Instance
    {
        get { return instance; }
    }

    public void PlayMusic(string clipname)
    {
        Sound s = Array.Find(Instance.musicSounds, x => x.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist");
            return;
        }
        else
        {
            Instance.musicSource.clip = s.clip;
            Instance.musicSource.loop = true;
            Instance.musicSource.Play();
        }
    }

    public bool IsMusicPlaying()
    {
        return Instance.musicSource.isPlaying;
    }

    public void PlaySFX(string clipname)
    {
        Sound s = Array.Find(Instance.sfxSounds, x => x.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist");
            return;
        }
        else
        {
            Instance.sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        Instance.musicSource.mute = !Instance.musicSource.mute;
    }

    public void ToggleSFX()
    {
        Instance.sfxSource.mute = !Instance.sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        Instance.musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        Instance.sfxSource.volume = volume;
    }

    public void MusicVolumeControl()
    {
        Debug.Log("slider adjusting for music");
        Instance.musicSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "BackGroundSlider");
        if (Instance.musicSlider != null)
        {
            Debug.Log("slider volume: " + Instance.musicSlider.value);
            MusicVolume(Instance.musicSlider.value);
        }
    }

    public void sfxVolumeControl()
    {
        Instance.sfxSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "EffectsSlider");
        Debug.Log("slider adjusting for effects");
        if (Instance.sfxSlider != null)
        {
            Debug.Log("slider volume: " + Instance.sfxSlider.value);
            SfxVolume(Instance.sfxSlider.value);
        }
    }


}
