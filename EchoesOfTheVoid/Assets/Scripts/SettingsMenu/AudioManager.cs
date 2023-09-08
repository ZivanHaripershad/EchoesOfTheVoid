using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private Slider musicSlider, sfxSlider;
    private static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    
    [SerializeField]
    private float normalAudioSpeed = 1f;
    [SerializeField]
    private float reducedAudioSpeed = 0.5f;

    [SerializeField] 
    private float audioSpeedChangeRate = 0.8f;

    private float audioSpeed;
    private Coroutine audioCoroutine;
    private bool isReduced;

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

        audioCoroutine = null;
        isReduced = false;
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
        
        Instance.musicSource.clip = s.clip;
        Instance.musicSource.loop = true;
        Instance.musicSource.Play();
        
    }

    void SetSoundSpeed()
    {
        Debug.Log(audioSpeed);
        Instance.musicSource.pitch = audioSpeed;
        Instance.sfxSource.pitch = audioSpeed;
    }

    private IEnumerator ReduceRoutine()
    {
        while (audioSpeed > reducedAudioSpeed)
        {
            audioSpeed -= audioSpeedChangeRate * Time.deltaTime;
            SetSoundSpeed();
            yield return null;
        }

        audioSpeed = reducedAudioSpeed;
        SetSoundSpeed();
    }

    public void ReduceAudioSpeed()
    {
        if (!isReduced)
        {
            isReduced = true;
            
            if (audioCoroutine != null)
                StopCoroutine(audioCoroutine);

            audioCoroutine = StartCoroutine(ReduceRoutine());
        }
    }
    
    private IEnumerator IncreaseRoutine()
    {
        while (audioSpeed < normalAudioSpeed)
        {
            audioSpeed += audioSpeedChangeRate * Time.deltaTime;
            SetSoundSpeed();
            yield return null;
        }

        audioSpeed = normalAudioSpeed;
        SetSoundSpeed();
    }

    public void IncreaseAudioSpeed()
    {
        if (isReduced)
        {
            isReduced = false;
            
            if (audioCoroutine != null)
                StopCoroutine(audioCoroutine);
        
            audioCoroutine = StartCoroutine(IncreaseRoutine());
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

    public void ToggleMusicOff()
    {
        Instance.musicSource.Stop();
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
