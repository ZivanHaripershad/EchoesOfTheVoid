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
    public float maxShootSpeed;
    public SpaceshipMode spaceshipMode;
    public OrbDepositingMode orbDepositingMode;
    public BulletCount bulletCount;
    //for bullet reload timer
    [SerializeField]
    public float countDown;
    
    private float timePassed;
    [SerializeField]
    private AudioSource shootSoundEffect;
    [SerializeField] private AudioSource cannotFireSoundEffect;
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
    [SerializeField]
    private GameObject bullet;

    private GameObject canvasUI;
    
    private GameObject reloadMessage;
    private GameObject cannotFireMessage;
    private GameObject purchaseAmmoMessage;

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

        if (SelectedUpgradeLevel1.Instance != null && SelectedUpgradeLevel1.Instance.GetUpgrade() != null && SelectedUpgradeLevel1.Instance.GetUpgrade().GetName() == "BulletFireRateUpgrade")
        {
            var upgradeSpeed = SelectedUpgradeLevel1.Instance.GetUpgrade().GetValue();
            maxShootSpeed += (maxShootSpeed * upgradeSpeed);
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
                ReplenishAmmo();
            }

            Shoot();
        }
        else
        {
            if ((bulletCount.currentBullets == 0 && orbCounter.orbsCollected <= 1) && bulletCount.generateBullets == false)
            {
                EnableMessage(reloadMessage, true);
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
            spaceshipMode.canRotateAroundPlanet == true)
        {
            if (timePassed > maxShootSpeed)
            {
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets > 0)
                {
                    if (bulletCount.generateBullets == false)
                    {
                        shootSoundEffect.Play();
                        timePassed = 0;
                        Instantiate(bullet, transform.position, transform.rotation);
                        bulletCount.currentBullets = bulletCount.currentBullets - 1;
                        EnableMessage(cannotFireMessage, false);
                    }
                    else
                    {
                        EnableMessage(cannotFireMessage, true);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0)
                {
                    cannotFireSoundEffect.Play();
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
            Debug.Log("Generating bullets");
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
