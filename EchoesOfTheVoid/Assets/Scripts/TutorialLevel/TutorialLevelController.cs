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

    private int popUpIndex;
    private bool soundsChanged;

    [SerializeField]
    private float normalAudioSpeed;
    [SerializeField]
    private float reducedAudioSpeed;

    [SerializeField] 
    private float audioSpeedChangeRate;
    
    private float audioSpeed;

    private IEnumerator DecreaseSpeed()
    {
        Debug.Log("Decrease");
        while (audioSpeed > reducedAudioSpeed)
        {
            audioSpeed -= audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }

        audioSpeed = reducedAudioSpeed;
    }

    private IEnumerator IncreaseSpeed()
    {
        while (audioSpeed < normalAudioSpeed)
        {
            audioSpeed += audioSpeedChangeRate * Time.deltaTime;
            yield return null;
        }
        audioSpeed = normalAudioSpeed;
    }

    public void ReduceAudioSpeed()
    {
        StartCoroutine(DecreaseSpeed());
    }

    public void IncreaseAudioSpeed()
    {
        StartCoroutine(IncreaseSpeed());
    }
    
    private void Start()
    {
        popupParent.SetActive(true);
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].SetActive(true);
        }

        audioSpeed = 1;

        orbCounter.planetOrbMax = 5;
        tutorialData.popUpIndex = 0;
        
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;

        soundsChanged = false;

        gameManager.DisableShield();
        mouseControl.EnableMouse();
    }

    private void PlayGameAudio()
    {
        sounds[1].Play();
    }
    

    private void Update()
    {
        popUpIndex = tutorialData.popUpIndex;

        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].pitch = audioSpeed;
        }

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
            
            //todo: remove comment
            //uiManager.DisableAtmosphereObject();
            
            //todo: remove
            uiManager.SetAtmosphereObjectToActive();
            
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
            mouseControl.EnableMouse();
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