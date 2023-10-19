using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsBackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is Music Playing: " + AudioManager.Instance.IsMusicPlaying());
        Debug.Log("What Is Playing: " + AudioManager.Instance.musicSource.clip.name);
        
        
        AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.AchievementsPageMusic);
    }
}
