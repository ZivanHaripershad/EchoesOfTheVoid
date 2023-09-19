using System;
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
    
    //level1
    [SerializeField] private GameObject primaryTargetMessage; 

    private MouseControl mouseControl;

    private bool levelLayersAreActive; 
    
    void Start()
    {
        atmosphereReaction.SetActive(false);
        earth.SetActive(false);
        spaceship.SetActive(false);
        
        bulletUi.SetActive(false);
        healthUi.SetActive(false);
        orbUi.SetActive(false);
        orbText.SetActive(false);
        
        reloadMessage.SetActive(false);
        cannotFireMessage.SetActive(false);
        purchaseAmmoMessage.SetActive(false);
        healthLowMessage.SetActive(false);
        
        bulletFactory.SetActive(false);
        powerFactory.SetActive(false);
        shieldFactory.SetActive(false);
        healthFactory.SetActive(false);
        
        //todo: level1 uiManager
        if (primaryTargetMessage)
            primaryTargetMessage.SetActive(false);

        levelLayersAreActive = false;
        mouseControl = GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>();
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
        spaceship.SetActive(true);
        bulletUi.SetActive(true);
        healthUi.SetActive(true);
        orbUi.SetActive(true);
        orbText.SetActive(true);
        levelLayersAreActive = true;
    }

    public void SetLevelObjectsToInactive()
    {
        earth.SetActive(false);
        spaceship.SetActive(false);
        bulletUi.SetActive(false);
        healthUi.SetActive(false);
        orbUi.SetActive(false);
        orbText.SetActive(false);
        reloadMessage.SetActive(false);
        cannotFireMessage.SetActive(false);
        purchaseAmmoMessage.SetActive(false);
        healthLowMessage.SetActive(false);
        
        //todo: level1
        if (primaryTargetMessage)
            primaryTargetMessage.SetActive(false);
    }

    public void DestroyRemainingOrbs()
    {
        var orbs = GameObject.FindGameObjectsWithTag("Orb");

        for (int i = 0; i < orbs.Length; i++)
        {
            Destroy(orbs[i]);
        }
    }

    private void Update()
    {
        //check when the user presses exit
        if (Input.GetKeyDown(KeyCode.Escape) && levelLayersAreActive)
        {
            //puase the game
            pauseMenu.SetActive(true);
            
            if (mouseControl == null) 
                mouseControl = GameObject.FindGameObjectWithTag("MouseControl").GetComponent<MouseControl>();
            
            mouseControl.EnableMouse();
            
            Time.timeScale = 0; 
        }
    }

    public void DestoryRemainingEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
