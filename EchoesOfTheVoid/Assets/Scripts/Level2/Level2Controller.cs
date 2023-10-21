using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private GameObject popupParent;
    [SerializeField] private MouseControl mouseControl;
    [SerializeField] private HealthDeposit healthDeposit;
    [SerializeField] private Text planetHealthNum;
    [SerializeField] private Text orbsNumber;
    [SerializeField] private Text enemiesNumber;
    [SerializeField] private OrbCounter orbCounter;
    [SerializeField] private int numberOfEnemiesToKillToProceedToBoss;
    [SerializeField] private GameObject motherShip;

    [SerializeField] private GameObject healthLowMessage;
    [SerializeField] private GameObject primaryTargetNotEliminated;

    private Coroutine audioCoroutine;
    private GameObject motherShipInstance;
    private int popupIndex;

    private float popUpWaitTime;
    
    [SerializeField] private MissionObjectiveBanner missionObjectiveBanner;
    [SerializeField] private GameObject missionObjectiveCanvas;
    
    
    private Text missionObjectiveText;
    private float missionBannerWaitTime;
    
    private float completedLevelTime = 0f;

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
        GameStateManager.Instance.CurrentLevel = GameManagerData.Level.Level2;
        
        GameStateManager.Instance.SetMaxOrbCapacity(4);

        if(GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2))
        {
            if (SelectedUpgradeLevel2.Instance != null &&
                SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
                SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
            {
                Debug.Log("Setting Second Capacity");
                GameStateManager.Instance.SetMaxOrbCapacity(6);
            }
        }
        
        if (GameStateManager.Instance.IsLevel3Completed)
        {
            if (SelectedUpgradeLevel3.Instance != null &&
                     SelectedUpgradeLevel3.Instance.GetUpgrade() != null &&
                     SelectedUpgradeLevel3.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
            {
                Debug.Log("Setting Third Capacity");
                GameStateManager.Instance.SetMaxOrbCapacity(8);
            }
        }
        
        OrbCounterUI.GetInstance().UpdateOrbText();
        
        AudioManager.Instance.ToggleMusicOff();
        AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.Level2BackgroundMusic);
        
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
        level2Data.maxMothershipHealth = 15;
        level2Data.mothershipDamageTaken = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;
        gameManagerData.expireOrbs = true;
        gameManagerData.level = GameManagerData.Level.Level2;
        gameManagerData.isShieldUp = false;
        
        gameManagerData.spawnInterval = 3;
        gameManagerData.spawnTimerVariation = 2;
        gameManagerData.timeTillNextWave = 4;
        gameManagerData.numLevel2ShieldsUsed = 0;
        gameManagerData.level2TimeCompletion = 0f;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();
        
        Debug.Log("Is Level 2 Shield Enabled: " + gameManager.IsShieldEnabled());

        healthCount.currentHealth = healthCount.maxHealth;
        
        //set mothership instance to null to check if it's spawned
        motherShipInstance = null;

        popUpWaitTime = 0f;
        
        missionObjectiveText = missionObjectiveCanvas.transform.Find("Objective").GetComponent<Text>();

        completedLevelTime = 0f;
    }

    private bool CheckEndingCriteria()
    {
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
        
        //check planet orbs
        if (orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
        {
            return false; //not enough orbs
        }
        
        return true; //all ending criteria has been met
    }
    
    private void Update()
    {
        popupIndex = level2Data.popUpIndex;
        
        HandlePopups();

        gameManagerData.level2TimeCompletion += Time.deltaTime;
        
        switch (popupIndex)
        {
            case 0: //show mission brief
                
                enemySpawning.ResetSpawning();
                break;
            case 1: //initialize gameplay
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                popUpWaitTime = 5;
                level2Data.popUpIndex++;

                break;
            case 2: //Shieldians intro
                SpawnNormalEnemies();
                HandleMissionUpdates();
                if (popUpWaitTime <= 0)
                {
                    popUpWaitTime = 10;
                }
                popUpWaitTime -= Time.deltaTime;
                CheckHealth();
                break;
            case 3: //Mothership intro
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                if (popUpWaitTime <= 0)
                {
                    level2Data.popUpIndex++;
                }
                popUpWaitTime -= Time.deltaTime;
                break;
            case 4: //continue gameplay
                HandleMissionUpdates();
                SpawnNormalEnemies();
                if (CheckEndingCriteria())
                {
                    level2Data.popUpIndex = 5;
                    AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.EndingMusic);
                    RemoveLevelObjects();
                    DisplayEndingScene();
                    return;
                }

                CheckHealth();
                
                if (orbCounter.planetOrbsDeposited >= orbCounter.planetOrbMax && HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
                {
                    healthDeposit.LowHealthStatus();
                }

                break;
            //end screen
            case 5:
                if (healthCount.currentHealth == healthCount.maxHealth)
                {
                    if (!AchievementsManager.Instance.CheckLevelGodModeCompleted(GameManagerData.Level.Level2))
                    {
                        AchievementsManager.Instance.UpdateLevelCompletedDictionary(GameManagerData.Level.Level2, true);
                    }
                }

                if (gameManagerData.numLevel2ShieldsUsed == 0 && !AchievementsManager.Instance.GetRiskTakerCompletionStatus())
                {
                    AchievementsManager.Instance.SetRiskTakerCompletionStatus(true);
                }
                
                break;
            //retry screen
            case 6:
                mouseControl.EnableMouse();
                completedLevelTime = 0f;
                gameManagerData.level2TimeCompletion = 0f;
                break;
        }
        
        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }

    private void CheckHealth()
    {
        if (healthCount.currentHealth == 0)
        {
            //show retry screen
            RemoveLevelObjects();
            level2Data.popUpIndex = 6;
        }
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
            enemySpawning.StartSpawningShieldians();
        }

        //killed enough to proceed to boss, and kill the rest of the enemies on screen
        if (gameManagerData.numberOfEnemiesKilled >= numberOfEnemiesToKillToProceedToBoss)
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
            level2Data.popUpIndex++;
            popUpWaitTime = 10;
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.BossMusic);
            sceneManager.motherShipHasEntered = true;
            motherShipInstance = Instantiate(motherShip, new Vector3(2.41f, -6.48f, 0), Quaternion.Euler(0, 0, -45));
        }
    }

    private void DisplayEndingScene()
    {
        if (!sceneManager.displayedEnding)
        {
            if (completedLevelTime == 0f)
            {
                completedLevelTime = gameManagerData.level2TimeCompletion;

                if (completedLevelTime >= 120f && !AchievementsManager.Instance.GetSpeedRunnerCompletionStatus())
                {
                    AchievementsManager.Instance.SetSpeedRunnerAchievementStatus(true);
                }
            }
            
            GameStateManager.Instance.IsLevel2Completed = true;
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
    
    private void HandleMissionUpdates()
    {
        Queue<string> missionUpdates = missionObjectiveBanner.GetMissionUpdates();
        bool isBannerAvailable = missionObjectiveBanner.GetIsBannerAvailable();
        
        if (missionUpdates.Count > 0 && isBannerAvailable)
        {
            missionBannerWaitTime = missionObjectiveBanner.GetBannerWaitTime();
            missionObjectiveBanner.SetIsBannerAvailable(false);
            missionObjectiveBanner.gameObject.SetActive(true);
            var missionUpdate = missionUpdates.Dequeue();
            AudioManager.Instance.PlaySFX("ObjectiveInProgress");
            missionObjectiveText.text = missionUpdate;
        }
        
        if (missionBannerWaitTime <= 0)
        {
            missionObjectiveBanner.SetIsBannerAvailable(true);
            missionObjectiveBanner.gameObject.SetActive(false);
            missionObjectiveBanner.ResetBannerWaitTime();
        }
        
        missionBannerWaitTime -= Time.deltaTime;
    }
    
}