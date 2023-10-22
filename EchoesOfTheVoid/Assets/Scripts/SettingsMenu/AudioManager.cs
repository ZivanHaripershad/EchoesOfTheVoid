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
    private static AudioManager _instance;

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
    private MusicFileNames currentlyPlaying;

    public float currentMusicSliderValue;
    public float currentSfxSliderValue;

    void Awake()
    {
        DontDestroyOnLoad(this);

        audioCoroutine = null;
        isReduced = false;

        currentlyPlaying = MusicFileNames.NoMusic;
        currentMusicSliderValue = 0.118f;
        currentSfxSliderValue = 0.7f;
    }

    public enum MusicFileNames
    {
        NoMusic,
        MainMenuMusic, 
        GamePlayMusic, 
        BossMusic, 
        TutorialLevelMusic, 
        EndingMusic, 
        GreenBossMusic,
        Level3Music,
        AchievementsPageMusic,
        UpgradeScreenMusic,
        Level2BackgroundMusic
    }

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // If the _instance is null, try to find an existing AudioManager in the scene
                _instance = FindObjectOfType<AudioManager>();

                // If no AudioManager exists, create a new one
                if (_instance == null)
                {
                    GameObject newGameObject = new GameObject("AudioManager");
                    _instance = newGameObject.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    public void PlayMusic(MusicFileNames clip)
    {

        if (clip == _instance.currentlyPlaying)
            return;

        String fileName = "";

        switch (clip)
        {
            case MusicFileNames.MainMenuMusic:
                fileName = "MainMenuMusic";
                break;
            case MusicFileNames.GamePlayMusic:
                fileName = "GamePlayMusic";
                break;
            case MusicFileNames.BossMusic:
                fileName = "BossMusic";
                break;
            case MusicFileNames.TutorialLevelMusic:
                fileName = "TutorialLevelMusic";
                break;
            case MusicFileNames.EndingMusic:
                fileName = "EndingMusic";
                break;
            case MusicFileNames.GreenBossMusic:
                fileName = "GreenBossMusic";
                break;
            case MusicFileNames.Level3Music:
                fileName = "Level3Music";
                break;
            case MusicFileNames.AchievementsPageMusic:
                fileName = "AchievementsPageMusic";
                break;
            case MusicFileNames.UpgradeScreenMusic:
                fileName = "UpgradeScreenMusic";
                break;
            case MusicFileNames.Level2BackgroundMusic:
                fileName = "Level2BackgroundMusic";
                break;
        }
        
        Sound s = Array.Find(_instance.musicSounds, x => x.clipName == fileName);
        
        if (s == null)
            Debug.Log("sound not found");
        
        _instance.musicSource.Stop();
        _instance.musicSource.clip = s.clip;
        _instance.musicSource.loop = true;
        _instance.musicSource.Play();
    }

    void SetSoundSpeed()
    {
        _instance.musicSource.pitch = audioSpeed;
        _instance.sfxSource.pitch = audioSpeed;
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
        return _instance.musicSource.isPlaying;
    }

    public void PlaySFX(string clipname)
    {
        Sound s = Array.Find(_instance.sfxSounds, x => x.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist");
            return;
        }
        else
        {
            _instance.sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusicOff()
    {
        _instance.musicSource.Stop();
    }
    
    public void ToggleMusic()
    {
        _instance.musicSource.mute = !_instance.musicSource.mute;
    }

    public void ToggleSFX()
    {
        _instance.sfxSource.mute = !_instance.sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        _instance.currentMusicSliderValue = volume;
        _instance.musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        _instance.currentSfxSliderValue = volume;
        _instance.sfxSource.volume = volume;
    }

    public void MusicVolumeControl()
    {
        Debug.Log("slider adjusting for music");
        _instance.musicSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "BackGroundSlider");
        if (_instance.musicSlider != null)
        {
            Debug.Log("slider volume: " + _instance.musicSlider.value);
            MusicVolume(_instance.musicSlider.value);
        }
    }

    public void sfxVolumeControl()
    {
        _instance.sfxSlider = FindObjectsOfType<Slider>().ToList().Find( x=>x.name == "EffectsSlider");
        Debug.Log("slider adjusting for effects");
        if (_instance.sfxSlider != null)
        {
            Debug.Log("slider volume: " + _instance.sfxSlider.value);
            if (!_instance.sfxSlider.value.Equals(_instance.currentSfxSliderValue))
            {
                PlaySFX("OrbDeposit");
            }
            SfxVolume(_instance.sfxSlider.value);
        }
    }


}
