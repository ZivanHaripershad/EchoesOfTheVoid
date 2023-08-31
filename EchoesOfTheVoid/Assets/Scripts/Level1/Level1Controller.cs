using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    [SerializeField]
    private Level1Data level1Data;
    [SerializeField]
    private GameManagerData gameManagerData;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private EnemySpawningLevel1 enemySpawning;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private HealthCount healthCount;
    [SerializeField]
    private AudioSource[] sounds;
    [SerializeField]
    private BulletSpawnScript bulletSpawnScript;
    [SerializeField]
    private GameObject[] popUps;
    [SerializeField]
    private GameObject popupParent;
    [SerializeField]
    private MouseControl mouseControl;
    [SerializeField]
    private HealthDeposit healthDeposit;
    [SerializeField]
    private Text planetHealthNum;
    [SerializeField]
    private Text orbsNumber;
    [SerializeField]
    private Text enemiesNumber;
    [SerializeField] 
    private OrbCounter orbCounter;
    [SerializeField]
    private GlobalVariables variables;

    private int popUpIndex;
    private bool soundsChanged;

    [SerializeField]
    private float normalAudioSpeed;
    [SerializeField]
    private float reducedAudioSpeed;

    [SerializeField] 
    private float audioSpeedChangeRate;
    
    private float audioSpeed;

    private Coroutine audioCoroutine;

    private IEnumerator DecreaseSpeed()
    {
        while (audioSpeed > reducedAudioSpeed)
        {
            audioSpeed -= audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }

        audioSpeed = reducedAudioSpeed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (audioSpeed < normalAudioSpeed)
        {
            audioSpeed += audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }
        audioSpeed = normalAudioSpeed;
    }

    public void ReduceAudioSpeed()
    {
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);
        audioCoroutine = StartCoroutine(DecreaseSpeed());
    }

    public void IncreaseAudioSpeed()
    {
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);
        audioCoroutine = StartCoroutine(IncreaseSpeed());
    }
    
    private void Start()
    {
        popupParent.SetActive(true);
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(true);
        }

        audioSpeed = 1;
        orbCounter.planetOrbMax = 5;
        
        level1Data.popUpIndex = 0;

        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;

        soundsChanged = false;
        mouseControl.EnableMouse();
        gameManager.DisableShield();
    }

    private void PlayGameAudio()
    {
        if (!sounds[1].isPlaying)
        {
            sounds[0].Pause();
            sounds[1].Play();
        }
    }
    

    private void Update()
    {
        popUpIndex = level1Data.popUpIndex;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].pitch = audioSpeed;
        }

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
        }
        else if (popUpIndex == 1)
        {
            //show second screen
            enemySpawning.ResetSpawning();
        }
        else if (popUpIndex == 2)
        {
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            //activate level objects
            uiManager.SetLevelObjectsToActive();
            uiManager.SetAtmosphereObjectToActive();
            
            //play the game audio
            PlayGameAudio();
            
            enemySpawning.StartSpawningAllTypesOfEnemies();
        }
       
        
        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }
}