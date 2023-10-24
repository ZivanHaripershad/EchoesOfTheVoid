using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletSpawnScript : MonoBehaviour
{
    public GameManagerData gameManagerData;
    public OrbCounter orbCounter;
    public SpaceshipMode spaceshipMode;
    public OrbDepositingMode orbDepositingMode;
    public BulletCount bulletCount;
    //for bullet reload timer
    [SerializeField]
    public float countDown;
    private float timePassed;
    private float downTime, pressTime = 0, burstPressTime = 0, burstDownTime;
    private bool ready = false;
    private bool burstReady = false;
    [SerializeField]
    private float reloadTimePerBullet;
    private GameObject progressBarInner;
    private float currReloadTime;
    private SpriteRenderer progressBar;
    private const float MAX_PROGRESS_BAR_SIZE = 38.36f;
    private bool fade = false;
    private float fadeColor = 0;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject doubleDamageBullet;
    [SerializeField] private GameObject burstBullet;
    [SerializeField] private BurstUpgradeState burstUpgradeState;
    [SerializeField] private Animator burstUpgradeAnimator;
    [SerializeField] private BulletBarUI bulletBarUi;
    
    [SerializeField] public float burstInitialHoldTime;
    [SerializeField] private float maxBurstHold;
    [SerializeField] private float timeBetweenBurstShots;


    private GameObject canvasUI;
    private GameObject reloadMessage;
    private GameObject cannotFireMessage;
    private GameObject purchaseAmmoMessage;
    
    private float maxShootSpeed;
    private const float shootSpeedL1 = 0.5f;
    private const float shootSpeedL2 = 0.4f;
    private const float shootSpeedL3 = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        canvasUI = GameObject.FindGameObjectWithTag("UI");
        reloadMessage = GameObject.FindGameObjectWithTag("Reload");
        cannotFireMessage = GameObject.FindGameObjectWithTag("CannotFire");
        purchaseAmmoMessage = GameObject.FindGameObjectWithTag("PurchaseAmmo");
        
        timePassed = 0;
        currReloadTime = 0;
        
        bulletCount.currentBullets = bulletCount.maxBullets;
        bulletCount.generateBullets = false;
        
        progressBarInner = GameObject.FindGameObjectsWithTag("progressBarInner")[0];
        progressBar = progressBarInner.GetComponent<SpriteRenderer>();
        progressBar.size = new Vector2(0, 1);

        switch (GameStateManager.Instance.CurrentLevel)
        {
            case GameManagerData.Level.Tutorial:
                maxShootSpeed = shootSpeedL1; 
                break;
            case GameManagerData.Level.Level1:
                maxShootSpeed = shootSpeedL1; 
                break;
            case GameManagerData.Level.Level2:
                maxShootSpeed = shootSpeedL2; 
                break; 
            case GameManagerData.Level.Level3:
                maxShootSpeed = shootSpeedL3; 
                break;
        }
        
        burstUpgradeState.isBurstUpgradeReady = false;
        burstUpgradeState.isBurstUpgradeReplenishing = false;
        burstUpgradeState.isBurstUpgradeCoolingDown = false;
    
    }

    void EnableMessage(GameObject message, bool toEnable)
    {
        reloadMessage.GetComponent<UrgentMessage>().Hide();
        cannotFireMessage.GetComponent<UrgentMessage>().Hide();
        purchaseAmmoMessage.GetComponent<UrgentMessage>().Hide();
        
        if (toEnable)
            message.GetComponent<UrgentMessage>().Show();
        else
            message.GetComponent<UrgentMessage>().Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            progressBar.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 1 - fadeColor);
            fadeColor += Time.deltaTime;
            if (fadeColor > 1)
            {
                fadeColor = 0;
                fade = false;
            }
        }
        
        CheckBulletStatus();
        
        InitiateBurstUpgradeReplenish();
        
        ManageBurstUpgradeStates();

        if (gameManagerData.tutorialActive)
        {
            if (bulletCount.currentBullets == 0 && bulletCount.generateBullets == false)
            {
                EnableMessage(reloadMessage, true);
            }
            
            if (bulletCount.generateBullets == false)
            {
                ReplenishAmmo();
            }

            Shoot();
        }
        else
        {
            if (burstUpgradeState.isBurstUpgradeReady)
            {
                Shoot();
            }
            
            if ((bulletCount.currentBullets == 0 && orbCounter.orbsCollected <= 1) && bulletCount.generateBullets == false)
            {
                EnableMessage(reloadMessage, true);
            }
            
            if (bulletCount.generateBullets == false)
            {
                ReplenishAmmo();
            }
            
            if (orbCounter.orbsCollected >= 2 && bulletCount.currentBullets == 0)
            {
                EnableMessage(purchaseAmmoMessage, true);
                ReplenishAmmo();
            }
            
            Shoot();
        }
        
    }

    private void ManageBurstUpgradeStates()
    {
        if (burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).IsName("BurstUpgradeReplenish") && burstUpgradeState.isBurstUpgradeReplenishing)
        {
            Debug.Log("Burst Is Ready to use");
            AudioManager.Instance.PlaySFX("BurstUpgradeReady");
            burstUpgradeState.isBurstUpgradeReady = true;
            burstUpgradeState.isBurstUpgradeReplenishing = false;
        }

        if (burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).IsName("BurstUpgradeCoolDown") && burstUpgradeState.isBurstUpgradeCoolingDown)
        {
            Debug.Log("Burst Is Not Ready to use");
            burstUpgradeState.isBurstUpgradeCoolingDown = false;
            burstUpgradeState.isBurstUpgradeReady = false;
        }
    }

    private void InitiateBurstUpgradeReplenish()
    {
        if (!burstUpgradeState.isBurstUpgradeReady &&
            !burstUpgradeState.isBurstUpgradeReplenishing)
        {
            ReplenishBurstUpgrade();
            burstUpgradeState.isBurstUpgradeReplenishing = true;
        }
    }

    private void ReplenishBurstUpgrade()
    {
        burstUpgradeAnimator.SetTrigger("replenish");
    }
    
    private void CoolDownBurstUpgrade()
    {
        burstUpgradeAnimator.SetTrigger("cooldown");
    }

    private void CheckBulletStatus()
    {

        if (bulletCount.currentBullets > 0)
        {
            EnableMessage(purchaseAmmoMessage, false);
        }

        if (bulletCount.generateBullets && bulletCount.currentBullets < bulletCount.maxBullets)
        {
            EnableMessage(reloadMessage, false);
            if (currReloadTime < reloadTimePerBullet)
                currReloadTime += Time.deltaTime;
            else
            {
                currReloadTime = 0;
                bulletCount.currentBullets += 1;
            }
        }

        if (bulletCount.generateBullets && bulletCount.currentBullets == bulletCount.maxBullets)
        {
            EnableMessage(reloadMessage, false);
            bulletCount.generateBullets = false;
        }
    }

    private void Shoot()
    {
        if (spaceshipMode.collectionMode == false && orbDepositingMode.depositingMode == false &&
            spaceshipMode.canRotateAroundPlanet)
        {
            if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
            {
                if ((GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2) ||
                     GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3)) && GameStateManager.Instance.IsLevel1Completed)
                {
                    if (SelectedUpgradeLevel1.Instance != null && 
                        SelectedUpgradeLevel1.Instance.GetUpgrade() != null && 
                        SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                    {
                        ActivateBurstUpgrade();
                    }
                }
                else if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level1))
                {
                    if (SelectedUpgradeLevel1.Instance != null && 
                        SelectedUpgradeLevel1.Instance.GetUpgrade() != null && 
                        SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                    {
                        ActivateBurstUpgrade();
                    }
                }
            }

            if (timePassed > maxShootSpeed)
            {
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets > 0)
                {
                    AudioManager.Instance.PlaySFX("LaserShot");
                    timePassed = 0;
                    if (bulletCount.currentBullets % 3 != 0)
                    {
                        Instantiate(bullet, transform.position, transform.rotation);
                    }
                    else
                    {
                        if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial) || !burstUpgradeState.isBurstUpgradeReady)
                        {
                            if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3) &&
                                GameStateManager.Instance.IsLevel2Completed)
                            {
                                if (SelectedUpgradeLevel2.Instance != null &&
                                    SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
                                    SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "DoubleDamageUpgrade")
                                {
                                    Instantiate(doubleDamageBullet, transform.position, transform.rotation);
                                }
                                else
                                {
                                    Instantiate(bullet, transform.position, transform.rotation);
                                }
                            }
                            else if(GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2))
                            {
                                if (SelectedUpgradeLevel2.Instance != null &&
                                    SelectedUpgradeLevel2.Instance.GetUpgrade() != null &&
                                    SelectedUpgradeLevel2.Instance.GetUpgrade().GetName() == "DoubleDamageUpgrade")
                                {
                                    Instantiate(doubleDamageBullet, transform.position, transform.rotation);
                                }
                                else
                                {
                                    Instantiate(bullet, transform.position, transform.rotation);
                                }
                            }
                            else
                            {
                                Instantiate(bullet, transform.position, transform.rotation);
                            }
                        }
                        else
                        {
                            Instantiate(bullet, transform.position, transform.rotation);
                        }
                    }
            
                    bulletCount.currentBullets -= 1;
                    EnableMessage(cannotFireMessage, false);
                
                }
                else if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0)
                {
                    AudioManager.Instance.PlaySFX("CannotFire");
                }
            }
            
            timePassed += Time.deltaTime;
        
        }
    }

    private void ActivateBurstUpgrade()
    {
        if (burstUpgradeState.isBurstUpgradeReady)
        {
            if (Input.GetKeyDown(KeyCode.Return) && burstReady == false)
            {
                burstDownTime = Time.time;
                burstPressTime = burstDownTime + burstInitialHoldTime;
                burstReady = true;
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                burstReady = false;
            }

            if (Time.time >= burstPressTime && burstReady)
            {
                StartBurst();
                burstPressTime = 0;
                if (!burstUpgradeState.isBurstUpgradeCoolingDown)
                {
                    Debug.Log("starting cool down");
                    CoolDownBurstUpgrade();
                    burstUpgradeState.isBurstUpgradeCoolingDown = true;
                }

                burstReady = false;
            }
        }
    }

    private void StartBurst()
    {
        bulletCount.currentBullets = bulletCount.maxBullets;
        bulletBarUi.SetBurstShotSprites();

        for (int i = 0; i < bulletCount.maxBullets; i++)
        {
            Invoke("ShootSingleBullet", i * timeBetweenBurstShots);
        }
    }

    private void ShootSingleBullet()
    {
        AudioManager.Instance.PlaySFX("LaserShot");
        Instantiate(burstBullet, transform.position, transform.rotation);
        bulletCount.currentBullets--;
        
        if (bulletCount.currentBullets == 0)
        {
            bulletBarUi.UnsetBurstSprites();
            bulletCount.generateBullets = true;
        }
    }

    private void ReplenishAmmo()
    {
        if (spaceshipMode.collectionMode)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.R) && ready == false)
        {
            downTime = Time.time;
            pressTime = downTime + countDown;
            ready = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            ready = false;
        }

        if (Time.time < pressTime && ready)
        {
            float progress = (Time.time - downTime) / countDown;

            progressBar.GetComponent<SpriteRenderer>().color = new Color(1 - progress, progress, 1 - progress);
            progressBar.size = new Vector2(progress * MAX_PROGRESS_BAR_SIZE, 1);
        }

        if (Time.time >= pressTime && ready)
        {
            ready = false;
            //reload 
            bulletCount.generateBullets = true;
            fade = true;
            fadeColor = 0;
        }
    }

    public void AutomaticallyReplenishAmmoForPlayer()
    {
        if (gameManagerData.hasResetAmmo)
        {
            bulletCount.currentBullets = bulletCount.maxBullets;
            gameManagerData.hasResetAmmo = false;
        }
    }
}
