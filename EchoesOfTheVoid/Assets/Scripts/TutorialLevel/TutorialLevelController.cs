using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialLevelController : MonoBehaviour
{
    public GameObject[] popUps;

    public TutorialData tutorialData;
    public GameManagerData gameManagerData;
    public GameManager gameManager;

    public MouseControl mouseControl;

    public EnemySpawning enemySpawning;
    public BulletSpawnScript bulletSpawnScript;

    public UIManager uiManager;
    public HealthCount healthCount;

    public AudioSource[] sounds;
    
    public HealthDeposit healthDeposit;

    public Text planetHealthNum;
    public Text orbsNumber;
    public Text enemiesNumber;

    private int popUpIndex;

    private bool soundsChanged; 
    
    private GlobalVariables variables;

    [SerializeField] private OrbCounter orbCounter;

    private void Start()
    {
        orbCounter.planetOrbMax = 5;
        tutorialData.popUpIndex = 0;
        
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;

        soundsChanged = false;

        gameManager.DisableShield();
        
        mouseControl.EnableMouse();
        variables = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVariables>();
    }

    private void PlayGameAudio()
    {
        sounds[1].Play();
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
            //show first screen
        }
        else if (popUpIndex == 1)
        {
            //show second screen
            enemySpawning.ResetSpawning();
        }
        else if (popUpIndex == 2)
        {
            //show player how to move and wait for left and right arrow key input and shoot
            mouseControl.DisableMouse();
            uiManager.DisableAtmosphereObject();
            uiManager.SetLevelObjectsToActive();
            variables.mustPause = true;
            enemySpawning.StartSpawningEnemies(3, false);
            gameManagerData.expireOrbs = false;
            gameManagerData.tutorialActive = true;

            if (gameManagerData.numberOfEnemiesKilled == 3)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //show player how to collect orbs
            mouseControl.DisableMouse();
            if (gameManagerData.numberOfOrbsCollected == 3)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            //show player how to switch back to shooting mode
            mouseControl.DisableMouse();
            if (Input.GetKey(KeyCode.Space))
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            uiManager.SetAtmosphereObjectToActive();
            mouseControl.DisableMouse();
            enemySpawning.ResetSpawning();
            enemySpawning.StopTheCoroutine();
            
            if (orbCounter.orbsCollected < 3 && orbCounter.planetOrbsDeposited < 1)
            {
                Debug.Log("FAILED");
            }
            
            //show player how to spend orbs

            if (orbCounter.planetOrbsDeposited > 0)
            {
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            //let player play and win against enemies

            if (!soundsChanged)
            {
                soundsChanged = true;
                sounds[0].Stop();
                Invoke("PlayGameAudio", 5); 
            }

            mouseControl.DisableMouse();
            if (gameManagerData.tutorialWaitTime <= 0)
            {
                bulletSpawnScript.AutomaticallyReplenishAmmoForPlayer();
                variables.mustPause = false;
                popUps[popUpIndex].SetActive(false);
                gameManagerData.expireOrbs = true;
                gameManagerData.tutorialActive = false;
                enemySpawning.StartSpawningEnemies(3, true);
            }
            else
            {
                gameManagerData.tutorialWaitTime -= Time.deltaTime;
            }

            if (healthCount.currentHealth == 0)
            {
                //show retry screen
                uiManager.DestroyRemainingOrbs();
                enemySpawning.DestroyActiveEnemies();
                uiManager.SetLevelObjectsToInactive();
                tutorialData.popUpIndex = 8;
            }

            if (orbCounter.planetOrbsDeposited >= orbCounter.planetOrbMax && HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
            {
               healthDeposit.LowHealthStatus();
            }
            
            if (orbCounter.planetOrbsDeposited >= orbCounter.planetOrbMax && !HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
            {
                //player wins so show winning screen
                uiManager.DestroyRemainingOrbs();
                enemySpawning.DestroyActiveEnemies();
                enemySpawning.ResetSpawning();
                enemySpawning.StopTheCoroutine();
                uiManager.SetLevelObjectsToInactive();
                
                tutorialData.popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            //Level complete screen
            var healthPercentage = Math.Round((decimal)healthCount.currentHealth / healthCount.maxHealth * 100);
            planetHealthNum.text =  healthPercentage + "%";
            orbsNumber.text = gameManagerData.numberOfOrbsCollected.ToString();
            enemiesNumber.text = gameManagerData.numberOfEnemiesKilled.ToString();
        }
        else if (popUpIndex == 8)
        {
            //retry screen
            mouseControl.EnableMouse();
            enemySpawning.ResetSpawning();
            enemySpawning.StopTheCoroutine();
        }
    }
}
