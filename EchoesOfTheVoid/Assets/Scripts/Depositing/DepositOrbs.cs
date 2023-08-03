using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositOrbs : MonoBehaviour
{

    public OrbDepositingMode orbDepositingMode;

    public SpaceshipMode spaceshipMode;

    [SerializeField]
    private AudioSource depositSoundEffect;

    [SerializeField]
    private AudioSource cannotDepositSoundEffect;

    public OrbCounter orbCounter;
    public BulletCount bulletCount;

    private GameObject bulletFactory;
    private GameObject powerFactory;
    private GameObject shieldFactory;
    private GameObject healthFactory;


    // Start is called before the first frame update
    void Start()
    {
        bulletFactory = GameObject.Find("BulletFactory");
        powerFactory = GameObject.Find("PowerFactory");
        shieldFactory = GameObject.Find("ShieldFactory");
        healthFactory = GameObject.Find("HealthFactory");
        orbDepositingMode.depositingMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(spaceshipMode.collectionMode == false){
            if(Input.GetKey(KeyCode.S)){
                orbDepositingMode.depositingMode = true;
                bool deposited = false;

                //Energy
                if (Input.GetKeyDown(KeyCode.J))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        Animator powerFactoryAnimator = powerFactory.GetComponent<Animator>();
                        powerFactoryAnimator.SetTrigger("isSelected");
                        orbCounter.planetOrbsDeposited++;
                        orbCounter.orbsCollected--;
                        deposited = true;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        Animator bulletFactoryAnimator = bulletFactory.GetComponent<Animator>();
                        bulletFactoryAnimator.SetTrigger("isSelected");
                        orbCounter.orbsCollected -= 2;
                        deposited = true;

                        bulletCount.currentBullets = bulletCount.maxBullets;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3)
                    {
                        Animator shieldFactoryAnimator = shieldFactory.GetComponent<Animator>();
                        shieldFactoryAnimator.SetTrigger("isSelected");
                        orbCounter.orbsCollected -= 3;
                        deposited = true;
                    }
                    else
                        cannotDepositSoundEffect.Play();
                
                //Health
                if (Input.GetKeyDown(KeyCode.K))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        Animator healthFactoryAnimator = healthFactory.GetComponent<Animator>();
                        healthFactoryAnimator.SetTrigger("isSelected");
                        orbCounter.orbsCollected -= 1;
                        deposited = true;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                if (deposited)
                {
                    //play the sound
                    depositSoundEffect.Play();

                    //update the HUD
                    Debug.Log("orbcounter orbs: " + orbCounter.orbsCollected);
                    OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);
                }
                
            }
            else
            {
                orbDepositingMode.depositingMode = false;
            }
        }
        
    }
}
