using System;
using UnityEngine;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    [SerializeField] private Level1Data level1Data;
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawningLevel1 enemySpawning;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private HealthCount healthCount;
    [SerializeField] private BulletSpawnScript bulletSpawnScript;
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameObject popupParent;
    [SerializeField] private MouseControl mouseControl;
    [SerializeField] private HealthDeposit healthDeposit;
    [SerializeField] private Text planetHealthNum;
    [SerializeField] private Text orbsNumber;
    [SerializeField] private Text enemiesNumber;
    [SerializeField] private OrbCounter orbCounter;
    [SerializeField] private GlobalVariables variables;
    
    [SerializeField] private float normalAudioSpeed;
    [SerializeField] private float reducedAudioSpeed;
    [SerializeField] private float audioSpeedChangeRate;

    [SerializeField] private int numberOfEnemiesToKill;

    [SerializeField] private GameObject healthLowMessage;

    private Coroutine audioCoroutine;
    private int popupIndex;
    
    struct SceneManager
    {
        public float audioSpeed;
        public bool soundsChanged;
        public bool hasStartedSpawning;
    }

    private SceneManager sceneManager;
    
    private void Start()
    {
        
        AudioManager.Instance.ToggleMusicOff();
        AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.GamePlayMusic);
        
        popupParent.SetActive(true);
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(true);
        }

        //set up scene manager
        sceneManager.soundsChanged = false;
        sceneManager.hasStartedSpawning = false;

        //reset counters
        orbCounter.planetOrbMax = 10;
        level1Data.popUpIndex = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();

        healthCount.currentHealth = healthCount.maxHealth;
    }

    private bool CheckEndingCriteria()
    {
        //check planet orbs
        if (orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
            return false; //not enough orbs

        if (gameManagerData.numberOfEnemiesKilled < numberOfEnemiesToKill)
        {
            return false;
        }
        
        return true; //all ending criteria has been met
    }
    
    private void Update()
    {
        popupIndex = level1Data.popUpIndex;
        
        HandlePopups();
        
        switch (popupIndex)
        {
            case 0: //show mission brief
                enemySpawning.ResetSpawning();
                break;
            case 1: //gameplay
                SpawnNormalEnemies();
                if (healthCount.currentHealth == 0)
                {
                    //show retry screen
                    RemoveLevelObjects();
                    level1Data.popUpIndex = 2;
                }

                if (CheckEndingCriteria())
                {
                    AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.EndingMusic);
                    RemoveLevelObjects();
                    DisplayEndingScene();
                }

                break;
            case 2: //retry screen
                break;
        }
        
        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }
    
    private void SpawnNormalEnemies()
    {
        if (Time.timeScale != 0)
            mouseControl.DisableMouse();

        //activate level objects
        uiManager.SetLevelObjectsToActive();
        uiManager.SetAtmosphereObjectToActive();

        enemySpawning.StartSpawningEnemies(3, true);
    }
    
    public void DisplayEndingScene()
    {
        var healthPercentage = Math.Round((decimal)healthCount.currentHealth / healthCount.maxHealth * 100);
        planetHealthNum.text =  healthPercentage + "%";
        orbsNumber.text = gameManagerData.numberOfOrbsCollected.ToString();
        enemiesNumber.text = gameManagerData.numberOfEnemiesKilled.ToString();
        mouseControl.EnableMouse();
    }

    private void RemoveLevelObjects()
    {
        uiManager.DestroyRemainingOrbs();
        enemySpawning.DestroyActiveEnemies();
        enemySpawning.StopTheCoroutine();
        uiManager.SetLevelObjectsToInactive();
    }

    private void HandlePopups()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            
            if (i == popupIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
               popUps[i].SetActive(false);
            }
        }
    }
    
}