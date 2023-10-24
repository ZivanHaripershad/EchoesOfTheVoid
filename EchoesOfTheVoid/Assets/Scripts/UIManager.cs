using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject earth;
    [SerializeField] private GameObject spaceship;
    [SerializeField] private GameObject atmosphereReaction;
    [SerializeField] private GameObject bulletFactory;
    [SerializeField] private GameObject powerFactory;
    [SerializeField] private GameObject shieldFactory;
    [SerializeField] private GameObject healthFactory;
    [SerializeField] private GameObject bulletUi;
    [SerializeField] private GameObject healthUi;
    [SerializeField] private GameObject reloadMessage;
    [SerializeField] private GameObject cannotFireMessage;
    [SerializeField] private GameObject purchaseAmmoMessage;
    [SerializeField] private GameObject healthLowMessage;
    [SerializeField] private GameObject orbUi;
    [SerializeField] private GameObject orbText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject missionObjectiveBanner;
    [SerializeField] private GameObject networkObjective;
    [SerializeField] private GameObject healthObjective;
    
    //level1
    [SerializeField] private GameObject level1EnemyObjective;
    
    //level2
    [SerializeField] private GameObject primaryTargetMessage; 
    [SerializeField] private GameObject level2EnemyObjective;

    //level3
    [SerializeField] private GameObject level3EnemyObjective;
    
    [SerializeField] private Level1Data level1Data;
    [SerializeField] private Level2Data level2Data;
    [SerializeField] private Level3Data level3Data;
    
    [SerializeField] private GameObject burstUpgradeIcon;
    [SerializeField] private GameObject burstHoldAnimation;

    private MouseControl mouseControl;

    private bool levelLayersAreActive;

    private bool atmosphereActiveBeforePause;
    
    void Start()
    {
        atmosphereReaction.SetActive(false);
        earth.SetActive(false);
        spaceship.SetActive(false);
        
        bulletUi.SetActive(false);
        healthUi.SetActive(false);
        orbUi.SetActive(false);
        orbText.SetActive(false);
       
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);
        shieldFactory.SetActive(false);
        healthFactory.SetActive(false);
        
        if (missionObjectiveBanner)
            missionObjectiveBanner.SetActive(false);
        
        if (networkObjective)
        {
            networkObjective.SetActive(false);
        }

        if (healthObjective)
        {
            healthObjective.SetActive(false);
        }
        
        //level1
        if (level1EnemyObjective)
        {
            level1EnemyObjective.SetActive(false);
        }

        //level2
        if (level2EnemyObjective)
        {
            level2EnemyObjective.SetActive(false);
        }
        
        //level3
        if (level3EnemyObjective)
        {
            level3EnemyObjective.SetActive(false);
        }
        
        //todo: level2 uiManager
        if (primaryTargetMessage)
            primaryTargetMessage.SetActive(false);
        
        levelLayersAreActive = false;
        mouseControl = GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>();

        atmosphereActiveBeforePause = false;
        
        burstUpgradeIcon.SetActive(false);
        
        burstHoldAnimation.SetActive(false);
    }

    public void SetAtmosphereObjectToActive()
    {
        atmosphereReaction.SetActive(true);
    }
    
    public void DisableAtmosphereObject()
    {
        atmosphereReaction.SetActive(false);
    }
    
    public void SetLevelObjectsToActive()
    {
        earth.SetActive(true);
        
        if (spaceship)
            spaceship.SetActive(true);
        bulletUi.SetActive(true);
        healthUi.SetActive(true);
        orbUi.SetActive(true);
        orbText.SetActive(true);
        levelLayersAreActive = true;
        
        if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
        {
            if ((GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2) ||
                 GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3)) && GameStateManager.Instance.IsLevel1Completed)
            {
                if (SelectedUpgradeLevel1.Instance != null && 
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null && 
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                   burstUpgradeIcon.SetActive(true);
                }
            }
            else if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level1))
            {
                if (SelectedUpgradeLevel1.Instance != null && 
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null && 
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                    burstUpgradeIcon.SetActive(true);
                }
            }
        }
    }

    public void SetLevelObjectsToInactive()
    {
        earth.SetActive(false);
         
        if (spaceship)
            spaceship.SetActive(false);
        bulletUi.SetActive(false);
        healthUi.SetActive(false);
        orbUi.SetActive(false);
        orbText.SetActive(false);
        
        reloadMessage.SetActive(false);
        cannotFireMessage.SetActive(false);
        purchaseAmmoMessage.SetActive(false);
        healthLowMessage.SetActive(false);
        
        //todo: level2
        if (primaryTargetMessage)
            primaryTargetMessage.SetActive(false);
        
        if (missionObjectiveBanner)
            missionObjectiveBanner.SetActive(false);
        
        burstUpgradeIcon.SetActive(false);
        burstHoldAnimation.SetActive(false);
    }

    public void DestroyRemainingOrbs()
    {
        var orbs = GameObject.FindGameObjectsWithTag("Orb");
        var healthOrbs = GameObject.FindGameObjectsWithTag("HealthOrb");

        for (int i = 0; i < orbs.Length; i++)
        {
            Destroy(orbs[i]);
        }
        
        for (int i = 0; i < healthOrbs.Length; i++)
        {
            Destroy(healthOrbs[i]);
        }
    }

    private void Update()
    {
        //check when the user presses exit
        if (Input.GetKeyDown(KeyCode.Escape) && levelLayersAreActive)
        {
            if (bulletFactory.activeSelf || powerFactory.activeSelf || healthFactory.activeSelf || shieldFactory.activeSelf)
            {
                ControlAtmosphereOnPauseMenu(false);
                atmosphereActiveBeforePause = true;
            }
            
            //pause the game
            if (level1Data.popUpIndex != 4 && level2Data.popUpIndex != 7 && level3Data.popUpIndex != 7)
            {
                pauseMenu.SetActive(true);
            }
            
            if (mouseControl == null) 
                mouseControl = GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>();
            
            mouseControl.EnableMouse();
            
            Time.timeScale = 0; 
        }

        if (Time.timeScale != 0 && atmosphereActiveBeforePause)
        {
            ControlAtmosphereOnPauseMenu(true);
            atmosphereActiveBeforePause = false;
        }
    }

    private void ControlAtmosphereOnPauseMenu(bool active)
    {
        SetActiveRecursively(atmosphereReaction, active);
        SetActiveRecursively(bulletFactory, active);
        SetActiveRecursively(powerFactory, active);
        SetActiveRecursively(shieldFactory, active);
        SetActiveRecursively(healthFactory, active);
        
        if (networkObjective)
        {
            SetActiveRecursively(networkObjective, active);
        }

        if (healthObjective)
        {
            SetActiveRecursively(healthObjective, active);
        }

        //level1
        if (level1EnemyObjective)
        {
            SetActiveRecursively(level1EnemyObjective, active);
        }

        //level2
        if (level2EnemyObjective)
        {
            SetActiveRecursively(level2EnemyObjective, active);
        }

        //level3
        if (level3EnemyObjective)
        {
            SetActiveRecursively(level3EnemyObjective, active);
        }
    }

    private static void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);
    		
        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }
}
