using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesScene1BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Music Playing: " + AudioManager.Instance.IsMusicPlaying());
        Debug.Log("What Is Playing: " + AudioManager.Instance.musicSource.clip.name);
        
        if(!AudioManager.Instance.IsMusicPlaying() || AudioManager.Instance.musicSource.clip.name != "mainmenusound")
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.MainMenuMusic);
    }
}
