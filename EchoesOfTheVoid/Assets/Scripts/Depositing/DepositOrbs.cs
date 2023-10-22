using System;
using UnityEngine;
using UnityEngine.UI;

public class DepositOrbs : MonoBehaviour
{

    public OrbDepositingMode orbDepositingMode;

    public SpaceshipMode spaceshipMode;
    public HealthCount healthCount;
    
    public OrbCounter orbCounter;
    public BulletCount bulletCount;

    public FactoryCosts factoryCosts;
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator bulletFactoryAnim;
    [SerializeField] private Animator powerFactoryAnim;
    [SerializeField] private Animator shieldFactoryAnim;
    [SerializeField] private Animator healthFactoryAnim;
    [SerializeField] private ShieldLogic shieldLogic;

    [SerializeField]
    private EnemySpeedControl enemySpeedControl;
    
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private TutorialData tutorialData;

    public ObjectiveManager objectiveManager;

    [SerializeField] private Animator earthDamageAnimator; 

    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;
    }
    enum OrbFactoryDeposited
    {
        Power, 
        Ammo, 
        Shield, 
        Health
    }
    
    private OrbFactoryDeposited factoryDeposited;

    // Update is called once per frame
    void Update()
    {
        if(spaceshipMode.collectionMode == false){
            if(Input.GetKey(KeyCode.Tab))
            {
                
                bool deposited = false;

                orbDepositingMode.depositingMode = true;
                
                enemySpeedControl.SlowDown();

                //Energy
                if (Input.GetKeyDown(KeyCode.J))
                    if (orbCounter.orbsCollected >= 1 && orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
                    {
                        if (GameStateManager.Instance.CurrentLevel.Equals( GameManagerData.Level.Tutorial))
                        {
                            if (!tutorialData.depositPower)
                            {
                                AudioManager.Instance.PlaySFX("CannotDeposit");
                                return;
                            }
                        }

                        if (!GameStateManager.Instance.IsCooledDown)
                        {
                            AudioManager.Instance.PlaySFX("CannotDeposit");
                            return;
                        }

                        orbCounter.planetOrbsDeposited++;
                        objectiveManager.UpdatePlanetEnergyBanner();
                        GameStateManager.Instance.IsCooledDown = false;
                        deposited = true;
                        factoryDeposited = OrbFactoryDeposited.Power;
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX("CannotDeposit");
                    }
                
                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        if (GameStateManager.Instance.CurrentLevel.Equals( GameManagerData.Level.Tutorial))
                        {
                            if (!tutorialData.depositAmmo)
                            {
                                AudioManager.Instance.PlaySFX("CannotDeposit");

                                return;
                            }
                        }
                        
                        deposited = true;
                        bulletCount.currentBullets = bulletCount.maxBullets;
                        factoryDeposited = OrbFactoryDeposited.Ammo;
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX("CannotDeposit");
                    }

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3 && gameManager.IsShieldEnabled() && shieldLogic.CanAddShields())
                    {
                        if (GameStateManager.Instance.CurrentLevel.Equals( GameManagerData.Level.Tutorial))
                        {
                            if (!tutorialData.depositShield)
                            {
                                AudioManager.Instance.PlaySFX("CannotDeposit");
                                return;
                            }
                        }

                        deposited = true;
                        shieldLogic.AddShield();
                        AchievementsManager.Instance.IncrementNumOfShieldsUsed();

                        if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2))
                        {
                            gameManagerData.numLevel2ShieldsUsed++;
                        }
                        
                        factoryDeposited = OrbFactoryDeposited.Shield;
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX("CannotDeposit");
                    }

                //Health
                if (Input.GetKeyDown(KeyCode.K))
                    if (orbCounter.orbsCollected >= 1)
                    {
                        if (healthCount.currentHealth < healthCount.maxHealth)
                        {
                            if (GameStateManager.Instance.CurrentLevel.Equals( GameManagerData.Level.Tutorial))
                            {
                                if (!tutorialData.depositHealth)
                                {
                                    AudioManager.Instance.PlaySFX("CannotDeposit");
                                    return;
                                }
                            }
                            
                            healthCount.currentHealth++;
                            deposited = true;
                            factoryDeposited = OrbFactoryDeposited.Health;

                            CheckHealth(); 
                        }
                        
                    }
                    else
                    {
                        AudioManager.Instance.PlaySFX("CannotDeposit");
                    }

                if (deposited)
                {
                    //play the sound
                    AudioManager.Instance.PlaySFX("OrbDeposit");

                    switch (factoryDeposited)
                    {
                        case OrbFactoryDeposited.Ammo:
                             OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.bulletCost);
                             bulletFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.Power:
                             OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.powerCost);
                             powerFactoryAnim.SetTrigger("isSelected");
                             break;
                        case OrbFactoryDeposited.Health:
                             OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.healthCost);
                             healthFactoryAnim.SetTrigger("isSelected");
                            break;
                        case OrbFactoryDeposited.Shield:
                             OrbCounterUI.GetInstance().DecrementOrbs(factoryCosts.shieldCost);
                             shieldFactoryAnim.SetTrigger("isSelected");
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

    private void CheckHealth()
    {
        if (healthCount.currentHealth > healthCount.maxHealth * 0.1) //20% damage
            earthDamageAnimator.SetBool("damage1", false);
            
        if (healthCount.currentHealth > healthCount.maxHealth * 0.2) //40% damage
            earthDamageAnimator.SetBool("damage2", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.4) //60% damage
            earthDamageAnimator.SetBool("damage3", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.6) //80% damage
            earthDamageAnimator.SetBool("damage4", false);
        
        if (healthCount.currentHealth > healthCount.maxHealth * 0.8) //90% damage
            earthDamageAnimator.SetBool("damage5", false);
    }
}
