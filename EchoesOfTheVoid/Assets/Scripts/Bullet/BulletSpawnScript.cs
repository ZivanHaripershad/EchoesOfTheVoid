using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;
    public float maxShootSpeed;
    private float timePassed;

    
    private TextMeshPro reloadMessage;
    private TextMeshPro cannotFireMessage;
    private TextMeshPro purchaseAmmoMessage;

    public SpaceshipMode spaceshipMode;

    public OrbDepositingMode orbDepositingMode;

    public BulletCount bulletCount;

    [SerializeField]
    private AudioSource shootSoundEffect;

    public OrbCounter orbCounter;

    //for bullet reload timer
    [SerializeField]
    public float countDown;

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

    public GameManagerData gameManagerData;

    // Start is called before the first frame update
    void Start()    
    {
        reloadMessage = GameObject.FindGameObjectWithTag("ReloadMessage").gameObject.GetComponent<TextMeshPro>();
        cannotFireMessage = GameObject.FindGameObjectWithTag("CannotFireMessage").gameObject.GetComponent<TextMeshPro>();
        purchaseAmmoMessage = GameObject.FindGameObjectWithTag("PurchaseAmmoMessage").gameObject.GetComponent<TextMeshPro>();
        
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

                /*if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0 && orbCounter.orbsCollected < 2)
                    reloadMessage.enabled = true;
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0 && orbCounter.orbsCollected >= 2)
                    purchaseAmmoMessage.enabled = true;*/
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
}
