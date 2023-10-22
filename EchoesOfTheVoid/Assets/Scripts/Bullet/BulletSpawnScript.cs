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
    private float downTime, pressTime = 0;
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

        switch (gameManagerData.level)
        {
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
            if (timePassed > maxShootSpeed)
            {
                // if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets > 0)
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
                        if (!gameManagerData.level.Equals(GameManagerData.Level.Tutorial))
                        {
                            if (gameManagerData.level.Equals(GameManagerData.Level.Level3) &&
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
                            else if(gameManagerData.level.Equals(GameManagerData.Level.Level2))
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

    private void ReplenishAmmo()
    {
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
