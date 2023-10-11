using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialLevelController : MonoBehaviour
{
    [SerializeField]
    private TutorialData tutorialData;
    [SerializeField]
    private GameManagerData gameManagerData;
    [SerializeField]
    private GameManager gameManager;
    [FormerlySerializedAs("enemySpawning")] [SerializeField]
    private TutorialEnemySpawning tutorialEnemySpawning;
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private HealthCount healthCount;
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
    
    private bool wPressed;
    private bool aPressed;
    private bool sPressed;
    private bool dPressed;

    private void Start()
    {
        AudioManager.Instance.ToggleMusicOff();
        AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.TutorialLevelMusic);
        
        popupParent.SetActive(true);
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(true);
        }

        orbCounter.planetOrbMax = 1;
        orbCounter.planetOrbsDeposited = 0;
        orbCounter.orbsCollected = 0;

        tutorialData.popUpIndex = 0;
        tutorialData.depositPower = false;
        tutorialData.depositAmmo = false;
        tutorialData.depositHealth = false;
        tutorialData.depositShield = false;

        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 5f;
        gameManagerData.hasResetAmmo = true;
        gameManagerData.level = GameManagerData.Level.Tutorial;
        gameManagerData.spawnInterval = 3;
        gameManagerData.spawnTimerVariation = 2;
        gameManagerData.timeTillNextWave = 4;

        healthCount.currentHealth = healthCount.maxHealth - 1;


        gameManager.DisableShield();
        mouseControl.EnableMouse();

        wPressed = false;
        aPressed = false;
        sPressed = false;
        dPressed = false;
        
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
        
        if (SelectedUpgradeLevel3.Instance != null &&
            SelectedUpgradeLevel3.Instance.GetUpgrade() != null)
        {
            SelectedUpgradeLevel3.Instance.SetUpgrade(null);
        }
    }

    private void Update()
    {
        popUpIndex = tutorialData.popUpIndex;

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
            //intro screen
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                gameManagerData.tutorialWaitTime = 3;
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 1)
        {
            uiManager.DisableAtmosphereObject();
            uiManager.SetLevelObjectsToActive();
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            //assignment 1 screen
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 2)
        {
            //a and d screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            if (Input.GetKey(KeyCode.A))
            {
                aPressed = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                dPressed = true;
            }

            if (aPressed && dPressed)
            {
                gameManagerData.tutorialWaitTime = 3;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //assignment 2 screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialEnemySpawning.ResetSpawning();
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 4)
        {
            // shoot enemies screen
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            variables.mustPause = true;
            tutorialEnemySpawning.StartSpawningEnemies(4, false);
            gameManagerData.expireOrbs = false;
            gameManagerData.tutorialActive = true;
            
            if (gameManagerData.numberOfEnemiesKilled == 4)
            {
                gameManagerData.tutorialWaitTime = 3;
                tutorialData.popUpIndex++;
            }
            
        }
        else if (popUpIndex == 5)
        {
            //assignment 3 screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                gameManagerData.tutorialWaitTime = 10;
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 6)
        {
            //explaining orbs screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 7)
        {
            //transition to collection mode screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (Input.GetKey(KeyCode.Space))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            //movement in collection mode screen
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (Input.GetKey(KeyCode.W))
            {
                wPressed = true;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                aPressed = true;
            }

            if (Input.GetKey(KeyCode.S))
            {
                sPressed = true;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                dPressed = true;
            }

            if (wPressed && aPressed && sPressed && dPressed)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 9)
        {
            //collecting orbs screen
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (gameManagerData.numberOfOrbsCollected == 4)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 10)
        {
            //congrats for collecting orbs screen
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            if (Input.GetKey(KeyCode.Space))
            {
                gameManagerData.tutorialWaitTime = 3;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 11)
        {
            //assignment 4 screen
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 12)
        {
            //bring up orb menu screen
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();
            
            uiManager.SetAtmosphereObjectToActive();
            tutorialData.depositPower = true;

            if (Input.GetKey(KeyCode.Tab))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 13)
        {
            //deposit to planet network
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            if (Input.GetKey(KeyCode.J))
            {
                gameManagerData.tutorialWaitTime = 10;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 14)
        {
            tutorialData.depositPower = false;
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            //planet network explanation
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 15)
        {
            //deposit to replenish ammo
            tutorialData.depositAmmo = true;
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            if (Input.GetKey(KeyCode.I))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 16)
        {
            //deposit to planet health
            tutorialData.depositAmmo = false;
            tutorialData.depositHealth = true;
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            if (Input.GetKey(KeyCode.K))
            {
                gameManagerData.tutorialWaitTime = 10;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 17)
        {
            tutorialData.depositHealth = false;
            
            if (Time.timeScale != 0)
                mouseControl.DisableMouse();

            //congrats player screen
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                uiManager.SetLevelObjectsToInactive();
                tutorialData.popUpIndex++;
                AudioManager.Instance.PlayMusic(AudioManager.MusicFileNames.EndingMusic);
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 18)
        {
            //Level complete screen
            var healthPercentage = Math.Round((decimal)healthCount.currentHealth / healthCount.maxHealth * 100);
            planetHealthNum.text =  healthPercentage + "%";
            orbsNumber.text = gameManagerData.numberOfOrbsCollected.ToString();
            enemiesNumber.text = gameManagerData.numberOfEnemiesKilled.ToString();
            mouseControl.EnableMouse();
            AchievementsManager.Instance.SetScholarAchievementStatus(true);
        }

        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }
}