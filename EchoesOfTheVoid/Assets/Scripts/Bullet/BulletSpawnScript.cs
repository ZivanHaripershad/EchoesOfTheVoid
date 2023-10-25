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
    [SerializeField] private float timeBetweenBurstShots;

    [SerializeField] private Sprite[] burstActivateSprites;
    private SpriteRenderer burstActivateSp;
    [SerializeField] private GameObject burstUpgradeUi;
    private Text holdBurstUpgradeText;
    private GameObject canvasUI;
    private GameObject reloadMessage;
    private GameObject cannotFireMessage;
    private GameObject purchaseAmmoMessage;
    
    private float maxShootSpeed;
    private const float shootSpeedL1 = 0.5f;
    private const float shootSpeedL2 = 0.4f;
    private const float shootSpeedL3 = 0.3f;
    private bool readySoundEffectPlayed;

    //check if upgrade was chosen
    private bool burstUpgradeChosen = false;
    private bool clicking = false;
    private float totalDownTime  = 0; 

    // Start is called before the first frame update
    void Start()
    {
        canvasUI = GameObject.FindGameObjectWithTag("UI");
        reloadMessage = GameObject.FindGameObjectWithTag("Reload");
        cannotFireMessage = GameObject.FindGameObjectWithTag("CannotFire");
        purchaseAmmoMessage = GameObject.FindGameObjectWithTag("PurchaseAmmo");
        
        
        if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
        {
            if ((GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2) ||
                 GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3)) &&
                GameStateManager.Instance.IsLevel1Completed)
            {
                if (SelectedUpgradeLevel1.Instance != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                    burstActivateSp = GameObject.FindGameObjectWithTag("BurstUpgradeHold").GetComponent<SpriteRenderer>();
                }
            }
            else if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level1))
            {
                if (SelectedUpgradeLevel1.Instance != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                    burstActivateSp = GameObject.FindGameObjectWithTag("BurstUpgradeHold").GetComponent<SpriteRenderer>();
                }
            }
        }
        
        
        timePassed = 0;
        currReloadTime = 0;
        readySoundEffectPlayed = false;
        
        if(GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
        {
            bulletCount.currentBullets = 0;
        }
        else
        {
            bulletCount.currentBullets = bulletCount.maxBullets;
        }
        
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
        
        EnableBurst(false);
        
        burstUpgradeState.isBurstUpgradeReady = false;
        burstUpgradeState.isBurstUpgradeReplenishing = false;
        burstUpgradeState.isBurstUpgradeCoolingDown = false;
        
        holdBurstUpgradeText = burstUpgradeUi.transform.Find("HoldDownEnter").GetComponent<Text>();
        holdBurstUpgradeText.enabled = false;
        

    }

    private void EnableBurst(bool mustActivate)
    {
        if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
        {
            if ((GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2) ||
                 GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3)) &&
                GameStateManager.Instance.IsLevel1Completed)
            {
                if (SelectedUpgradeLevel1.Instance != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                    if (mustActivate)
                        ActivateBurstUpgrade();
                    else 
                        burstUpgradeChosen = true;
                }
            }
            else if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level1))
            {
                if (SelectedUpgradeLevel1.Instance != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                    SelectedUpgradeLevel1.Instance.GetUpgrade().GetName().Equals("BurstUpgrade"))
                {
                    if (mustActivate)
                        ActivateBurstUpgrade();
                    else 
                        burstUpgradeChosen = true;
                }
            }
        }
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
        
        if (burstUpgradeChosen)
            InitiateBurstUpgradeReplenish();

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
            if (IsBurstReady())
            {
                holdBurstUpgradeText.enabled = true;
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

    bool IsBurstState(String nm)
    {
        return burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).IsName(nm);
    }

    public bool IsBurstReady()
    {
        if (!burstUpgradeAnimator)
            return false;
        
        bool isReady = burstUpgradeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
                                      IsBurstState("BurstUpgradeReplenish");

        if (!isReady && readySoundEffectPlayed)
        {
            readySoundEffectPlayed = false;
        }
        
        if (isReady && !readySoundEffectPlayed)
        {
            readySoundEffectPlayed = true;
            AudioManager.Instance.PlaySFX("BurstUpgradeReady");
        }

        return isReady;
    }

    private void InitiateBurstUpgradeReplenish()
    {
        ReplenishBurstUpgrade();
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
            
            EnableBurst(true);
            
            
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
                        if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial) || !IsBurstReady())
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
        
        if (IsBurstReady())
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                totalDownTime = 0;
                clicking = true;
            }

            if (clicking && Input.GetKey(KeyCode.Return))
            {
                totalDownTime += Time.deltaTime;

                if (totalDownTime >= burstInitialHoldTime)
                {
                    holdBurstUpgradeText.enabled = false;
                    clicking = false; 
                    StartBurst();
                    CoolDownBurstUpgrade();
                }
                else
                {
                    int sprite = (int)(totalDownTime / burstInitialHoldTime * burstActivateSprites.Length);
                    if (sprite < burstActivateSprites.Length && sprite > -1)
                         burstActivateSp.sprite =burstActivateSprites[sprite];
                }
            }

            if (clicking && Input.GetKeyUp(KeyCode.Return))
            {
                burstActivateSp.sprite = burstActivateSprites[0];
                clicking = false;
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

            if (i == bulletCount.maxBullets - 1)
            {
                ReplenishBurstUpgrade();
            }
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
