using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SettingsController : MonoBehaviour
{

    /*[SerializeField]
    //private AudioManger audioContol;

    [SerializeField]
    private AudioSource[] sounds;*/
    public static SettingsController Instance;
    [SerializeField]
    private GameObject[] popUps;
    [SerializeField]
    private GameObject popupParent;
    [SerializeField]
    private MouseControl mouseControl;


    [SerializeField]
    private SettingsDataLive settingsData;
    public int popUpIndex;

    
    private void Start()
    {
        popupParent.SetActive(true);
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(true);
        }

        settingsData.popUpIndex = 0;

    }


    private void PlayGameAudio()
    {
        AudioManager.Instance.PlayMusic("Background");
    }
    

    private void Update()
    {


        popUpIndex = settingsData.popUpIndex;

     
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }




        if (popUpIndex == 0)
        {
            //show first screen
            //popUps[0].SetActive(true);
           // popUps[1].SetActive(false);
            //popUps[2].SetActive(false);
         
            
        }
        else if (popUpIndex == 1)
        {
            //show second screen
           // popUps[0].SetActive(false);
            //popUps[1].SetActive(true);
            //popUps[2].SetActive(false);

        }
        else if (popUpIndex == 2)
        {
           
        }
        
    }
}