using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    [SerializeField] 
    private GameObject bullet;
    public float maxShootSpeed;
    private float timePassed;

    [SerializeField]
    private TextMeshPro reloadMessage;
    [SerializeField]
    private TextMeshPro cannotFireMessage;
    [SerializeField]
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

    private float downTime, upTime, pressTime = 0;
    private bool ready = false;

    [SerializeField]
    private float reloadTimePerBullet;

    private float currReloadTime; 

    // Start is called before the first frame update
    void Start()
    {
        bulletCount.currentBullets = bulletCount.maxBullets;
        bulletCount.generateBullets = false;
        timePassed = 0;
        currReloadTime = 0;
        cannotFireMessage.enabled = false;
        reloadMessage.enabled = false;
        purchaseAmmoMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((bulletCount.currentBullets == 0 && orbCounter.orbsCollected <= 1) && bulletCount.generateBullets == false){
            

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
            if (Time.time >= pressTime && ready == true)
            {
                ready = false;
                //reload 
                bulletCount.generateBullets = true;
            }
        }

        if (bulletCount.currentBullets > 0)
            purchaseAmmoMessage.enabled = false;

        if(bulletCount.generateBullets && bulletCount.currentBullets < 14){
            reloadMessage.enabled = false;
            if (currReloadTime < reloadTimePerBullet)
                currReloadTime += Time.deltaTime;
            else
            {
                currReloadTime = 0; 
                bulletCount.currentBullets = bulletCount.currentBullets + 1;
                BulletCounterUI.instance.UpdateBullets(bulletCount.currentBullets);
            }
        }

        if(bulletCount.generateBullets && bulletCount.currentBullets == 14){
            bulletCount.generateBullets = false;
        }

        if(spaceshipMode.collectionMode == false && orbDepositingMode.depositingMode == false && spaceshipMode.canRotateAroundPlanet == true){
            if (timePassed > maxShootSpeed) {
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets > 0)
                {
                    if (bulletCount.generateBullets == false)
                    {
                        shootSoundEffect.Play();
                        timePassed = 0;
                        Instantiate(bullet, transform.position, transform.rotation);
                        bulletCount.currentBullets = bulletCount.currentBullets - 1;
                        BulletCounterUI.instance.UpdateBullets(bulletCount.currentBullets);
                        cannotFireMessage.enabled = false;
                    }
                    else
                    {
                        cannotFireMessage.enabled = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0 && orbCounter.orbsCollected < 2)
                    reloadMessage.enabled = true;
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets == 0 && orbCounter.orbsCollected >= 2)
                    purchaseAmmoMessage.enabled = true;
             }

            timePassed += Time.deltaTime;
        }
    }
}
