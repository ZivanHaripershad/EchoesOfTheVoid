using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialLevelController : MonoBehaviour
{
    [SerializeField]
    private TutorialData tutorialData;
    [SerializeField]
    private GameManagerData gameManagerData;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private EnemySpawning enemySpawning;
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

        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;

        gameManager.DisableShield();
        mouseControl.EnableMouse();

        wPressed = false;
        aPressed = false;
        sPressed = false;
        dPressed = false;
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
                gameManagerData.tutorialWaitTime = 5;
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 1)
        {
            uiManager.DisableAtmosphereObject();
            uiManager.SetLevelObjectsToActive();
            
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
                gameManagerData.tutorialWaitTime = 5;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //assignment 2 screen
            
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                enemySpawning.ResetSpawning();
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
            enemySpawning.StartSpawningEnemies(4, false);
            gameManagerData.expireOrbs = false;
            gameManagerData.tutorialActive = true;
            
            if (gameManagerData.numberOfEnemiesKilled == 4)
            {
                gameManagerData.tutorialWaitTime = 5;
                tutorialData.popUpIndex++;
            }
            
        }
        else if (popUpIndex == 5)
        {
            //assignment 3 screen
            
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
            
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 7)
        {
            //transition to collection mode screen
            
            if (Input.GetKey(KeyCode.Space))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            //movement in collection mode screen
            
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
            
            if (gameManagerData.numberOfOrbsCollected == 4)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 10)
        {
            //congrats for collecting orbs screen
            
            if (Input.GetKey(KeyCode.Space))
            {
                gameManagerData.tutorialWaitTime = 5;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 11)
        {
            //assignment 4 screen

            if (gameManagerData.tutorialWaitTime <= 0)
            {
                tutorialData.popUpIndex++;
            }
            gameManagerData.tutorialWaitTime -= Time.deltaTime;
        }
        else if (popUpIndex == 12)
        {
            //bring up orb menu screen
            
            uiManager.SetAtmosphereObjectToActive();
            
            if (Input.GetKey(KeyCode.S))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 13)
        {
            //deposit to planet network
            
            if (Input.GetKey(KeyCode.J))
            {
                gameManagerData.tutorialWaitTime = 10;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 14)
        {
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
            
            if (Input.GetKey(KeyCode.I))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 16)
        {
            //deposit to planet health
            
            if (Input.GetKey(KeyCode.K))
            {
                gameManagerData.tutorialWaitTime = 10;
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 17)
        {
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
        }

        //if game is paused
        if (Time.timeScale == 0)
            mouseControl.EnableMouse();
    }
}