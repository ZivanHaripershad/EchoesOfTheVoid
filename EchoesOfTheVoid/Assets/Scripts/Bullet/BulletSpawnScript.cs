using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    [SerializeField] 
    private GameObject bullet;
    public float maxShootSpeed;
    private float timePassed;

    public SpaceshipMode spaceshipMode;

    public OrbDepositingMode orbDepositingMode;

    public BulletCount bulletCount;

    [SerializeField]
    private AudioSource shootSoundEffect;

    public OrbCounter orbCounter;

    // Start is called before the first frame update
    void Start()
    {
        bulletCount.currentBullets = bulletCount.maxBullets;
        bulletCount.generateBullets = false;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if((bulletCount.currentBullets == 0 && orbCounter.orbsCollected <= 1) && bulletCount.generateBullets == false){
            bulletCount.generateBullets = true;
        }

        if(bulletCount.generateBullets && bulletCount.currentBullets < 5){
            bulletCount.currentBullets = bulletCount.currentBullets + 1;
            BulletCounterUI.instance.UpdateBullets(bulletCount.currentBullets);
        }

        if(bulletCount.generateBullets && bulletCount.currentBullets == 5){
            bulletCount.generateBullets = false;
        }

        if(spaceshipMode.collectionMode == false && orbDepositingMode.depositingMode == false && spaceshipMode.canRotateAroundPlanet == true){
            if (timePassed > maxShootSpeed) 
                if (Input.GetKeyDown(KeyCode.Return) && bulletCount.currentBullets > 0)
                {
                    shootSoundEffect.Play();
                    timePassed = 0; 
                    Instantiate(bullet, transform.position, transform.rotation);
                    bulletCount.currentBullets = bulletCount.currentBullets -1;
                    BulletCounterUI.instance.UpdateBullets(bulletCount.currentBullets);
                }
            timePassed += Time.deltaTime;
        }
    }
}
