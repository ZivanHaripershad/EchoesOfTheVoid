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

    [SerializeField]
    private float normalAudioSpeed;
    [SerializeField]
    private float reducedAudioSpeed;

    [SerializeField] 
    private float audioSpeedChangeRate;

    [SerializeField] 
    private int numberOfEnemiesToKillToProceedToBoss;

    [SerializeField] 
    private GameObject motherShip;

    private Coroutine audioCoroutine;
    
    struct SceneManager
    {
        public float audioSpeed;
        public bool soundsChanged;
        public bool hasStartedSpawning;
        public bool motherShipIntroScene;
        public bool motherShipHasEntered;
        public float bossTimer; 
    }

    private SceneManager sceneManager;
    
    enum TrackPlaying
    {
        INTRO,
        GAMEPLAY,
        BOSS
    }
    
    private IEnumerator DecreaseSpeed()
    {
        while (sceneManager.audioSpeed > reducedAudioSpeed)
        {
            sceneManager.audioSpeed -= audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }

        sceneManager.audioSpeed  = reducedAudioSpeed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (sceneManager.audioSpeed  < normalAudioSpeed)
        {
            sceneManager.audioSpeed  += audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }
        sceneManager.audioSpeed  = normalAudioSpeed;
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

        //set up scene manager
        sceneManager.audioSpeed = 1;
        sceneManager.soundsChanged = false;
        sceneManager.hasStartedSpawning = false; 
        sceneManager.motherShipIntroScene = false;
        sceneManager.motherShipHasEntered = false;
        sceneManager.bossTimer = 10f;
        
        //reset counters
        orbCounter.planetOrbMax = 5;
        level1Data.popUpIndex = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.DisableShield();
    }

    private void PlayAudio(TrackPlaying trackPlaying)
    {
        if (!sounds[(int)trackPlaying].isPlaying)
        {
            for (int i = 0; i < sounds.Length; i++)
                sounds[i].Pause();

            sounds[(int)trackPlaying].Play();
        }
    }
    
    private void Update()
    {
        HandleAudioSpeedChanges();
        HandlePopups();

        switch (level1Data.popUpIndex)
        {
            case 0: //show first screen
                break;
            case 1: //show second screen
                enemySpawning.ResetSpawning();
                break;
            case 2: //normal enemies
                SpawnNormalEnemies();
                break;
            case 3: 
                SpawnBoss();
                break;
        }
        
        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }

    private void SpawnBoss()
    {
        //play music for boss fight
        PlayAudio(TrackPlaying.BOSS);

        //intro scene has not finished yet
        if (!sceneManager.motherShipIntroScene)
        {
            enemySpawning.StopSpawning();
            enemySpawning.ResetSpawning();
            
            //give boss time to start circling
            if (sceneManager.bossTimer > 0)
                sceneManager.bossTimer -= Time.deltaTime;
            else
                sceneManager.motherShipIntroScene = true;
        }
        else
        {
            enemySpawning.StartSpawningAllTypesOfEnemies();
        }

        if (!sceneManager.motherShipHasEntered)
        {
            sceneManager.motherShipHasEntered = true;
            Instantiate(motherShip, new Vector3(2.41f, -6.48f, 0), Quaternion.Euler(0, 0, -45));
        }
    }

    private void SpawnNormalEnemies()
    {
        if (Time.timeScale != 0)
            mouseControl.DisableMouse();

        //activate level objects
        uiManager.SetLevelObjectsToActive();
        uiManager.SetAtmosphereObjectToActive();

        //play the game audio
        PlayAudio(TrackPlaying.GAMEPLAY);

        if (!sceneManager.hasStartedSpawning)
        {
            sceneManager.hasStartedSpawning = true;
            enemySpawning.StartSpawningAllTypesOfEnemies();
        }

        if (gameManagerData.numberOfEnemiesKilled >= numberOfEnemiesToKillToProceedToBoss)
        {
            enemySpawning.ResetSpawning();
            level1Data.popUpIndex++;
        }
    }

    private void HandlePopups()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == level1Data.popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
    }

    private void HandleAudioSpeedChanges()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].pitch = sceneManager.audioSpeed;
        }
    }
}