using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Level2Controller : MonoBehaviour
{
    [SerializeField] private Level2Data level2Data;
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawningLevel2 enemySpawning;
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

    [SerializeField] private int numberOfEnemiesToKillToProceedToBoss;
    [SerializeField] private GameObject motherShip;

    [SerializeField] private GameObject healthLowMessage;
    [SerializeField] private GameObject primaryTargetNotEliminated;

    private Coroutine audioCoroutine;
    private GameObject motherShipInstance;
    public UpgradeScene1Manager upgradeScene1Manager;
    private int popupIndex;
    
    struct SceneManager
    {
        public float audioSpeed;
        public bool soundsChanged;
        public bool hasStartedSpawning;
        public bool motherShipIntroScene;
        public bool motherShipHasEntered;
        public float bossTimer;
        public bool displayedEnding;
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
        sceneManager.motherShipIntroScene = false;
        sceneManager.motherShipHasEntered = false;
        sceneManager.displayedEnding = false;
        sceneManager.bossTimer = 10f;
        
        //reset counters
        orbCounter.planetOrbMax = 10;
        orbCounter.planetOrbsDeposited = 0;
        orbCounter.orbsCollected = 0;
        level2Data.popUpIndex = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();
        
        Debug.Log("Is Level 2 Shield Enabled: " + gameManager.IsShieldEnabled());

        healthCount.currentHealth = healthCount.maxHealth;
        
        //set mothership instance to null to check if it's spawned
        motherShipInstance = null;
        
    }

    private bool CheckEndingCriteria()
    {
        //check planet orbs
        if (orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
            return false; //not enough orbs
        
        //check health status
        if (HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
        {
            //show health too low message
            healthLowMessage.GetComponent<UrgentMessage>().Show();
            return false;
        } 
        healthLowMessage.GetComponent<UrgentMessage>().Hide(); //has enough health
        
        //check that mothership has entered
        if (!sceneManager.motherShipHasEntered)
        {
            primaryTargetNotEliminated.GetComponent<UrgentMessage>().Show();
            return false;
        }
        
        //mother has entered, but has not been killed
        if (motherShipInstance != null) 
        {
            primaryTargetNotEliminated.GetComponent<UrgentMessage>().Show();
            return false;
        }
        
        primaryTargetNotEliminated.GetComponent<UrgentMessage>().Hide();

        //check that health is medium
        if (HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
        {
            healthLowMessage.GetComponent<UrgentMessage>().Show();
            return false;
        }
        healthLowMessage.GetComponent<UrgentMessage>().Hide();
        
        //check filling status, if power bar is still filling up, don't end level
        if (GameObject.FindGameObjectWithTag("PowerBar").GetComponent<FillPowerBar>().IsStillFilling())
            return false;
        
        return true; //all ending criteria has been met
    }
    
    private void Update()
    {
        popupIndex = level2Data.popUpIndex;
        
        HandlePopups();
        
        switch (popupIndex)
        {
            case 0: //show mission brief
                enemySpawning.ResetSpawning();
                break;
            case 1: //gameplay

                if (CheckEndingCriteria())
                {
                    level2Data.popUpIndex = 2;
                    AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.EndingMusic);
                    RemoveLevelObjects();
                    DisplayEndingScene();
                    return;
                }
                
                SpawnNormalEnemies();
                if (healthCount.currentHealth == 0)
                {
                    //show retry screen
                    RemoveLevelObjects();
                    level2Data.popUpIndex = 3;
                }
                
                if (orbCounter.planetOrbsDeposited >= orbCounter.planetOrbMax && HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
                {
                    healthDeposit.LowHealthStatus();
                }

                break;
            case 2: //retry screen
                break;
            case 3: //ending screen
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

        if (!sceneManager.hasStartedSpawning)
        {
            sceneManager.hasStartedSpawning = true;
            enemySpawning.StartSpawningLevel2Enemies();
        }

        //killed enough to proceed to boss, and kill the rest of the enemies on screen
        if (gameManagerData.numberOfEnemiesKilled >= numberOfEnemiesToKillToProceedToBoss && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            
            SpawnBoss();
        }
    }

    
    private void SpawnBoss()
    {
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
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.BossMusic);
            sceneManager.motherShipHasEntered = true;
            motherShipInstance = Instantiate(motherShip, new Vector3(2.41f, -6.48f, 0), Quaternion.Euler(0, 0, -45));
        }
    }

    private void DisplayEndingScene()
    {
        if (!sceneManager.displayedEnding)
        {
            sceneManager.displayedEnding = true;
            var healthPercentage = Math.Round((decimal)healthCount.currentHealth / healthCount.maxHealth * 100);
            planetHealthNum.text =  healthPercentage + "%";
            orbsNumber.text = gameManagerData.numberOfOrbsCollected.ToString();
            enemiesNumber.text = gameManagerData.numberOfEnemiesKilled.ToString();
            mouseControl.EnableMouse();
        }
    }

    private void RemoveLevelObjects()
    {
        uiManager.DestroyRemainingOrbs();
        enemySpawning.DestroyActiveEnemies();
        enemySpawning.StopSpawning();
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