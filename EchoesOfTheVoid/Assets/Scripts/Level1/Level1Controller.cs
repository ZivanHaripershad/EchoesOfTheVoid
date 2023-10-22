using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

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
    [SerializeField] private MissionObjectiveBanner missionObjectiveBanner;
    [SerializeField] private GameObject missionObjectiveCanvas;
    [SerializeField] private GameObject orbStealingEnemy;

    private Text missionObjectiveText;
    private Coroutine audioCoroutine;
    private int popupIndex;
    private float missionBannerWaitTime;
    
    struct SceneManager
    {
        public float audioSpeed;
        public bool soundsChanged;
        public bool hasStartedSpawning;
        public bool displayedEnding;
        public int numOrbStealingSpawned;
    }

    private SceneManager sceneManager;
    
    private void Awake()
    {
        GameStateManager.Instance.CurrentLevel = GameManagerData.Level.Level1;
    }

    private void Start()
    {
        GameStateManager.Instance.SetMaxOrbCapacity(4);

        OrbCounterUI.GetInstance().UpdateOrbText();

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
        sceneManager.displayedEnding = false;

        //reset counters
        orbCounter.planetOrbMax = 10;
        orbCounter.planetOrbsDeposited = 0;
        orbCounter.orbsCollected = 0;
        level1Data.popUpIndex = 0;

        //set up game manager
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.hasResetAmmo = true;
        gameManagerData.expireOrbs = true;
        gameManagerData.expireHealthOrbs = true;
        gameManagerData.numberOfEnemiesToKill = numberOfEnemiesToKill;
        gameManagerData.level = GameManagerData.Level.Level1;
        gameManagerData.isShieldUp = false;
        gameManagerData.spawnInterval = 3;
        gameManagerData.spawnTimerVariation = 2;
        gameManagerData.timeTillNextWave = 4;

        //set up shield and mouse
        mouseControl.EnableMouse();
        gameManager.EnableShield();
        
        Debug.Log("Is Level 1 Shield Enabled: " + gameManager.IsShieldEnabled());

        healthCount.currentHealth = healthCount.maxHealth;
        
        missionObjectiveText = missionObjectiveCanvas.transform.Find("Objective").GetComponent<Text>();
        
        GameStateManager.Instance.CoolDownTime = 0f;
        GameStateManager.Instance.IsCooledDown = true;
    }

    private bool CheckEndingCriteria()
    {
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
                break;
            case 1:
                enemySpawning.ResetSpawning();
                break;
            case 2: //gameplay
                SpawnNormalEnemies();
                HandleMissionUpdates();
                SpawnOrbStealingEnemy();

                if (CheckEndingCriteria())
                {
                    level1Data.popUpIndex = 3;
                    AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.EndingMusic);
                    RemoveLevelObjects();
                    DisplayEndingScene();
                    return;
                }

                if (healthCount.currentHealth <= 0)
                {
                    //show retry screen
                    RemoveLevelObjects();
                    level1Data.popUpIndex = 4;
                }
                
                if (orbCounter.planetOrbsDeposited >= orbCounter.planetOrbMax && HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
                {
                    healthDeposit.LowHealthStatus();
                }

                break;
            case 3: //ending screen
                if (healthCount.currentHealth == healthCount.maxHealth)
                {
                    if (!AchievementsManager.Instance.CheckLevelGodModeCompleted(GameManagerData.Level.Level1))
                    {
                        AchievementsManager.Instance.UpdateLevelCompletedDictionary(GameManagerData.Level.Level1, true);
                    }
                    
                    if (!AchievementsManager.Instance.GetProtectorCompletionStatus())
                    {
                        AchievementsManager.Instance.SetProtectorCompletionStatus(true);
                    }
                }
                
                break;
            case 4: //retry screen
                mouseControl.EnableMouse();
                break;
        }
        
        //if game is paused
        if (Time.timeScale == 0)
        {
            mouseControl.EnableMouse();
        }
    }

    private void SpawnOrbStealingEnemy()
    {
        void Spawn()
        {
            InstantiateOrbStealingEnemy();
            sceneManager.numOrbStealingSpawned++;
        }

        //get the current fill level of the energy bar
        if (orbCounter.planetOrbsDeposited > orbCounter.planetOrbMax / 4 && sceneManager.numOrbStealingSpawned < 1)
        { 
            Spawn();
        }
        
        if (orbCounter.planetOrbsDeposited > orbCounter.planetOrbMax / 2 && sceneManager.numOrbStealingSpawned < 2)
        {
            Spawn();
        }
        
        if (orbCounter.planetOrbsDeposited > (orbCounter.planetOrbMax * 3) / 4 && sceneManager.numOrbStealingSpawned < 2)
        {
            Spawn();
        }
    }

    private void InstantiateOrbStealingEnemy()
    {
        //choose a spawn point
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("OrbStealingWaypoint");
        Random random = new Random();
        Vector3 position = waypoints[random.Next(waypoints.Length)].gameObject.transform.position;

        Instantiate(orbStealingEnemy, position, Quaternion.identity);
    }

    private void SpawnNormalEnemies()
    {
        if (Time.timeScale != 0)
        {
            mouseControl.DisableMouse();
        }

        //activate level objects
        uiManager.SetLevelObjectsToActive();
        uiManager.SetAtmosphereObjectToActive();

        enemySpawning.StartSpawningEnemies();
        SpawnOrbStealingEnemy();
    }
    
    private void DisplayEndingScene()
    {
        if (!sceneManager.displayedEnding)
        {
            GameStateManager.Instance.IsLevel1Completed = true;
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