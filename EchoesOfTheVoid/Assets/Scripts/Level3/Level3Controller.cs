using System;
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

    private void Awake()
    {
        GameStateManager.Instance.CurrentLevel = GameManagerData.Level.Level3;
        
        //reset counters
        if (GameStateManager.Instance.IsMarkingModeOn)
        {
            orbCounter.planetOrbMax = 10;
            level3Data.mineEnemyMaxHealth = 10;
        }
        else
        {
            orbCounter.planetOrbMax = 15;
            level3Data.mineEnemyMaxHealth = 20;
        }
    }

    private void Start()
    {
        GameStateManager.Instance.SetMaxOrbCapacity(4);

        if (GameStateManager.Instance.IsLevel2Completed)
        {
            if (SelectedUpgradeLevel2.Instance != null &&
                SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
                SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
            {
                GameStateManager.Instance.SetMaxOrbCapacity(GameStateManager.Instance.GetMaxOrbCapacity() + 2);
            }
        }
    
        if(GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3))
        {
           if (SelectedUpgradeLevel3.Instance != null &&
                     SelectedUpgradeLevel3.Instance.GetUpgrade() != null &&
                     SelectedUpgradeLevel3.Instance.GetUpgrade().GetName() == "OrbCapacityUpgrade")
            {
                GameStateManager.Instance.SetMaxOrbCapacity(GameStateManager.Instance.GetMaxOrbCapacity() + 2);
            }
        }
        
        OrbCounterUI.GetInstance().UpdateOrbText();

        AudioManager.Instance.ToggleMusicOff();
        AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.Level3Music);
        
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

        orbCounter.planetOrbsDeposited = 0;
        orbCounter.orbsCollected = 0;
        
        level3Data.popUpIndex = 0;
        level3Data.mineEnemyDamageTaken = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;
        gameManagerData.expireOrbs = true;
        gameManagerData.level = GameManagerData.Level.Level3;
        gameManagerData.isShieldUp = false;
        gameManagerData.expireHealthOrbs = true;
        gameManagerData.spawnInterval = 3;
        gameManagerData.spawnTimerVariation = 2;
        gameManagerData.timeTillNextWave = 4;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();

        healthCount.currentHealth = healthCount.maxHealth;
        
        //set mothership instance to null to check if it's spawned
        mineEnemyInstance = null;

        popUpWaitTime = 0f;
        
        missionObjectiveText = missionObjectiveCanvas.transform.Find("Objective").GetComponent<Text>();

        GameStateManager.Instance.CoolDownTime = 0f;
        GameStateManager.Instance.IsCooledDown = true;
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
        
        //check that mineEnemyShip has entered
        if (!sceneManager.mineEnemyShipHasEntered)
        {
            primaryTargetNotEliminated.GetComponent<UrgentMessage>().Show();
            return false;
        }
        
        //mine enemy has entered, but has not been killed
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
        {
            return false;
        }
        
        //check planet orbs
        if (orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
        {
            return false; //not enough orbs
        }
        
        return true; //all ending criteria has been met
    }
    
    private void Update()
    {
        popupIndex = level3Data.popUpIndex;
        
        HandlePopups();

        switch (popupIndex)
        {
            case 0: //show mission brief
                break;
            case 1: //show enemy intro cards
                enemySpawning.ResetSpawning();
                popUpWaitTime = 10;
                break;
            case 2: //gameplay intro
                
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                if (popUpWaitTime <= 0)
                {
                    level3Data.popUpIndex++;
                }
                popUpWaitTime -= Time.deltaTime;
                break;
            
            case 3: //gameplay
                SpawnNormalEnemies();
                HandleMissionUpdates();
                CheckHealth();
                //killed enough to proceed to boss, and kill the rest of the enemies on screen
                if (gameManagerData.numberOfEnemiesKilled >= numberOfEnemiesToKillToProceedToBoss)
                {
                    SpawnBoss();
                }
                popUpWaitTime = 10f;

                break;
            case 4: // mine enemy intro
                
                if (Time.timeScale != 0)
                    mouseControl.DisableMouse();
                
                SpawnBoss();
                HandleMissionUpdates();
                CheckHealth();
                if (popUpWaitTime <= 0)
                {
                    level3Data.popUpIndex++;
                }
                popUpWaitTime -= Time.deltaTime;
                break;
                
            case 5: //gameplay
                if (Time.timeScale != 0)
                    mouseControl.DisableMouse();
                
                HandleMissionUpdates();
                SpawnBoss();

                if (CheckEndingCriteria())
                {
                    Debug.Log("Level 3 completed");
                    level3Data.popUpIndex = 6;
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

            case 6: //endscreen
                if (healthCount.currentHealth == healthCount.maxHealth)
                {
                    if (!AchievementsManager.Instance.CheckLevelGodModeCompleted(GameManagerData.Level.Level3))
                    {
                        AchievementsManager.Instance.UpdateLevelCompletedDictionary(GameManagerData.Level.Level3, true);
                    }
                }
                break;
            
            case 7: //retry screen
                mouseControl.EnableMouse();
                break;
            
        }
        
        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }

    private void CheckHealth()
    {
        if (healthCount.currentHealth <= 0)
        {
            //show retry screen
            RemoveLevelObjects();
            level3Data.popUpIndex = 7;
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
            AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.GreenBossMusic);
            sceneManager.mineEnemyShipHasEntered = true;
            mineEnemyInstance = Instantiate(mineEnemy, new Vector3(-1.08f, 6.07f, 0), Quaternion.Euler(0, 0, -45));
        }
    }

    private void DisplayEndingScene()
    {
        if (!sceneManager.displayedEnding)
        {
            GameStateManager.Instance.IsLevel3Completed = true;
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