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

    private Animator bulletFactoryAnim;
    private Animator powerFactoryAnim;
    private Animator shieldFactoryAnim;
    private Animator healthFactoryAnim;


    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;

        bulletFactoryAnim = GameObject.Find("BulletFactory").GetComponent<Animator>();
        powerFactoryAnim = GameObject.Find("PowerFactory").GetComponent<Animator>();
        shieldFactoryAnim= GameObject.Find("ShieldFactory").GetComponent<Animator>();
        healthFactoryAnim = GameObject.Find("HealthFactory").GetComponent<Animator>();
    }
    enum OrbFactoryDeposited
    {
        POWER, 
        AMMO, 
        SHIELD, 
        HEALTH
    }
    
    private OrbFactoryDeposited factoryDeposited;

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
                        orbCounter.planetOrbsDeposited++;
                        orbCounter.orbsCollected--;
                        deposited = true;

                        factoryDeposited = OrbFactoryDeposited.POWER;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        orbCounter.orbsCollected -= 2;
                        deposited = true;
                        bulletCount.currentBullets = bulletCount.maxBullets;

                        factoryDeposited = OrbFactoryDeposited.AMMO;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3)
                    {
                        orbCounter.orbsCollected -= 3;
                        deposited = true;

                        factoryDeposited = OrbFactoryDeposited.SHIELD;
                    }
                    else
                        cannotDepositSoundEffect.Play();
                
                //Health
                if (Input.GetKeyDown(KeyCode.K))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        orbCounter.orbsCollected -= 1;
                        deposited = true;

                        factoryDeposited = OrbFactoryDeposited.HEALTH;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                if (deposited)
                {
                    //play the sound
                    depositSoundEffect.Play();

                    //update the HUD
                    OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);

                    switch (factoryDeposited)
                    {
                        case OrbFactoryDeposited.AMMO:
                            bulletFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.POWER:
                            powerFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.HEALTH:
                            healthFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.SHIELD:
                            shieldFactoryAnim.SetTrigger("isSelected");
                            break;
                    }
                }
                
            }
            else
            {
                orbDepositingMode.depositingMode = false;
            }
        }
        
    }
}
