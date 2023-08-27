using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositOrbs : MonoBehaviour
{

    public OrbDepositingMode orbDepositingMode;

    public SpaceshipMode spaceshipMode;
    public HealthCount healthCount;

    [SerializeField]
    private AudioSource depositSoundEffect;

    [SerializeField]
    private AudioSource cannotDepositSoundEffect;

    public OrbCounter orbCounter;
    public BulletCount bulletCount;

    public FactoryCosts factoryCosts;
    private GameManager gameManager;

    private Animator bulletFactoryAnim;
    private Animator powerFactoryAnim;
    private Animator shieldFactoryAnim;
    private Animator healthFactoryAnim;

    private EnemySpeedControl enemySpeedControl;


    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;

        bulletFactoryAnim = GameObject.Find("BulletFactory").GetComponent<Animator>();
        powerFactoryAnim = GameObject.Find("PowerFactory").GetComponent<Animator>();
        shieldFactoryAnim= GameObject.Find("ShieldFactory").GetComponent<Animator>();
        healthFactoryAnim = GameObject.Find("HealthFactory").GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpeedControl = GameObject.FindGameObjectWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
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
            if(Input.GetKey(KeyCode.S))
            {
                
                bool deposited = false;

                orbDepositingMode.depositingMode = true;
                
                enemySpeedControl.SlowDown();

                //Energy
                if (Input.GetKeyDown(KeyCode.J))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        orbCounter.planetOrbsDeposited++;
                        deposited = true;

                        factoryDeposited = OrbFactoryDeposited.POWER;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        deposited = true;
                        bulletCount.currentBullets = bulletCount.maxBullets;

                        factoryDeposited = OrbFactoryDeposited.AMMO;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3 && gameManager.IsShieldEnabled())
                    {
                        deposited = true;

                        factoryDeposited = OrbFactoryDeposited.SHIELD;
                    }
                    else
                        cannotDepositSoundEffect.Play();
                
                //Health
                if (Input.GetKeyDown(KeyCode.K))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        if (healthCount.currentHealth < healthCount.maxHealth)
                        {
                            healthCount.currentHealth++;
                            deposited = true;
                            factoryDeposited = OrbFactoryDeposited.HEALTH;
                        }
                        
                    }
                    else
                        cannotDepositSoundEffect.Play();

                if (deposited)
                {
                    //play the sound
                    depositSoundEffect.Play();

                    switch (factoryDeposited)
                    {
                        case OrbFactoryDeposited.AMMO:
                            // OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.bulletCost);
                            // bulletFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.POWER:
                            // OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.powerCost);
                            // powerFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.HEALTH:
                            // OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.healthCost);
                            // healthFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.SHIELD:
                            // OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.shieldCost);
                            // shieldFactoryAnim.SetTrigger("isSelected");
                            break;
                    }
                }
                
            }
            else
            {
                orbDepositingMode.depositingMode = false;
                enemySpeedControl.SpeedUp();
            }
        }
        
    }
}
