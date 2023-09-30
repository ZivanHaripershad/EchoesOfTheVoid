﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Level3Controller : MonoBehaviour
{
    [SerializeField] private Level3Data level3Data;
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private EnemySpawningLevel3 enemySpawning;
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
    [SerializeField] private GameObject mineEnemy;

    [SerializeField] private GameObject healthLowMessage;
    [SerializeField] private GameObject primaryTargetNotEliminated;

    private Coroutine audioCoroutine;
    private GameObject mineEnemyInstance;
    private int popupIndex;

    private float popUpWaitTime;
    
    [SerializeField] private MissionObjectiveBanner missionObjectiveBanner;
    [SerializeField] private GameObject missionObjectiveCanvas;
    
    
    private Text missionObjectiveText;
    private float missionBannerWaitTime;

    struct SceneManager
    {
        public float audioSpeed;
        public bool soundsChanged;
        public bool hasStartedSpawning;
        public bool mineEnemyIntroScene;
        public bool mineEnemyShipHasEntered;
        public float bossTimer;
        public bool displayedEnding;
    }

    private Level3Controller.SceneManager sceneManager;
    
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
        sceneManager.mineEnemyIntroScene = false;
        sceneManager.mineEnemyShipHasEntered = false;
        sceneManager.displayedEnding = false;
        sceneManager.bossTimer = 10f;
        
        //reset counters
        orbCounter.planetOrbMax = 15;
        orbCounter.planetOrbsDeposited = 0;
        orbCounter.orbsCollected = 0;
        level3Data.popUpIndex = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;
        gameManagerData.expireOrbs = true;
        gameManagerData.level = GameManagerData.Level.Level3;
        gameManagerData.isShieldUp = false;
        
        gameManagerData.spawnInterval = 3;
        gameManagerData.spawnTimerVariation = 2;
        gameManagerData.timeTillNextWave = 4;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();
        
        Debug.Log("Is Level 3 Shield Enabled: " + gameManager.IsShieldEnabled());

        healthCount.currentHealth = healthCount.maxHealth;
        
        //set mothership instance to null to check if it's spawned
        mineEnemyInstance = null;

        popUpWaitTime = 0f;
        
        missionObjectiveText = missionObjectiveCanvas.transform.Find("Objective").GetComponent<Text>();

        //remove upgrades from other levels
        if (SelectedUpgradeLevel1.Instance != null &&
            SelectedUpgradeLevel1.Instance.GetUpgrade() != null)
        {
            SelectedUpgradeLevel1.Instance.SetUpgrade(null);
        }

        if (SelectedUpgradeLevel2.Instance != null &&
            SelectedUpgradeLevel2.Instance.GetUpgrade() != null)
        {
            SelectedUpgradeLevel2.Instance.SetUpgrade(null);
        }
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
        if (!sceneManager.mineEnemyShipHasEntered)
        {
            primaryTargetNotEliminated.GetComponent<UrgentMessage>().Show();
            return false;
        }
        
        //mother has entered, but has not been killed
        if (mineEnemyInstance != null) 
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
        popupIndex = level3Data.popUpIndex;
        
        HandlePopups();

        switch (popupIndex)
        {
            case 0: //show mission brief
                
                enemySpawning.ResetSpawning();
                popUpWaitTime = 10;
                break;
            case 1: //gameplay intro
                
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                if (popUpWaitTime <= 0)
                {
                    level3Data.popUpIndex++;
                }
                popUpWaitTime -= Time.deltaTime;
                break;
            
            case 2: //gameplay
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                popUpWaitTime = 10f;

                break;
            case 3: // mine enemy intro
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                if (popUpWaitTime <= 0)
                {
                    level3Data.popUpIndex++;
                }
                popUpWaitTime -= Time.deltaTime;
                break;
                
            case 4: //gameplay
                HandleMissionUpdates();
                SpawnNormalEnemies();
                if (CheckEndingCriteria())
                {
                    level3Data.popUpIndex = 5;
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

            case 5: //endscreen
                if (healthCount.currentHealth == healthCount.maxHealth)
                {
                    if (AchievementsManager.Instance.CheckLevelGodModeCompleted(GameManagerData.Level.Level3))
                    {
                        AchievementsManager.Instance.UpdateLevelCompletedDictionary(GameManagerData.Level.Level3, true);
                    }
                }
                break;
            
            case 6: //retry screen
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
            level3Data.popUpIndex = 6;
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
            enemySpawning.StartSpawningLevel3Enemies();
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
        if (!sceneManager.mineEnemyIntroScene)
        {
            enemySpawning.StopSpawning();
            enemySpawning.ResetSpawning();
            
            //give boss time to start circling
            if (sceneManager.bossTimer > 0)
                sceneManager.bossTimer -= Time.deltaTime;
            else
                sceneManager.mineEnemyIntroScene = true;
        }
        else
        {
            enemySpawning.StartSpawningAllTypesOfEnemies();
        }

        if (!sceneManager.mineEnemyShipHasEntered)
        {
            level3Data.popUpIndex++;
            popUpWaitTime = 10;
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.BossMusic);
            sceneManager.mineEnemyShipHasEntered = true;
            mineEnemyInstance = Instantiate(mineEnemy, new Vector3(-7.84f, -3.1f, 0), Quaternion.Euler(0, 0, -45));
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
            Debug.Log("Updating Banner:" + missionUpdate);

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