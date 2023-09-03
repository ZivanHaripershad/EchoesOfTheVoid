using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!AudioManager.Instance.IsMusicPlaying())
            AudioManager.Instance.PlayMusic("MainMenuMusic");
    }
}
