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

    public GameObject canvasUI;
    
    private Text reloadMessage;
    private Text cannotFireMessage;
    private Text purchaseAmmoMessage;

    // Start is called before the first frame update
    void Start()
    {
        
        reloadMessage = canvasUI.transform.Find("ReloadMessage").GetComponent<Text>();
        cannotFireMessage = canvasUI.transform.Find("CannotFireMessage").GetComponent<Text>();
        purchaseAmmoMessage = canvasUI.transform.Find("PurchaseAmmoMessage").GetComponent<Text>();
        
        timePassed = 0;
        currReloadTime = 0;
        cannotFireMessage.enabled = false;
        bulletCount.currentBullets = bulletCount.maxBullets;
        bulletCount.generateBullets = false;
        
        purchaseAmmoMessage.enabled = false;
        progressBarInner = GameObject.FindGameObjectsWithTag("progressBarInner")[0];
        progressBar = progressBarInner.GetComponent<SpriteRenderer>();
        progressBar.size = new Vector2(0, 1);
    
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
                reloadMessage.enabled = true;
                ReplenishAmmo();
            }

            Shoot();
        }
        else
        {
            if ((bulletCount.currentBullets == 0 && orbCounter.orbsCollected <= 1) && bulletCount.generateBullets == false)
            {
                reloadMessage.enabled = true;
                ReplenishAmmo();
            }
            
            if (orbCounter.orbsCollected >= 2 && bulletCount.currentBullets == 0)
            {
                reloadMessage.enabled = false;
                purchaseAmmoMessage.enabled = true;
            }

            Shoot();
        }
        
    }

    private void CheckBulletStatus()
    {

        if (bulletCount.currentBullets > 0)
        {
            purchaseAmmoMessage.enabled = false;
            reloadMessage.enabled = false;
        }

        if (bulletCount.generateBullets && bulletCount.currentBullets < bulletCount.maxBullets)
        {
            reloadMessage.enabled = false;
            purchaseAmmoMessage.enabled = false;
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
            purchaseAmmoMessage.enabled = false;
            reloadMessage.enabled = false;
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
                        cannotFireMessage.enabled = false;
                    }
                    else
                    {
                        cannotFireMessage.enabled = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0)
                {
                    cannotFireSoundEffect.Play();
                    reloadMessage.enabled = false;
                    purchaseAmmoMessage.enabled = true;
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
