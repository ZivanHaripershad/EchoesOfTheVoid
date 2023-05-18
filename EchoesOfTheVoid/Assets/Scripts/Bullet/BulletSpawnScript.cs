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

    // Start is called before the first frame update
    void Start()
    {
        bulletCount.currentBullets = bulletCount.maxBullets;
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(spaceshipMode.collectionMode == false && orbDepositingMode.depositingMode == false){
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
