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
    private ShieldLogic shieldLogic; 

    [SerializeField]
    private EnemySpeedControl enemySpeedControl;


    // Start is called before the first frame update
    void Start()
    {
        orbDepositingMode.depositingMode = false;
    }

    private void OnEnable()
    {
        bulletFactoryAnim = GameObject.Find("BulletFactory").GetComponent<Animator>();
        powerFactoryAnim = GameObject.Find("PowerFactory").GetComponent<Animator>();
        shieldFactoryAnim= GameObject.Find("ShieldFactory").GetComponent<Animator>();
        healthFactoryAnim = GameObject.Find("HealthFactory").GetComponent<Animator>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        shieldLogic = GameObject.Find("Shield").GetComponent<ShieldLogic>();
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
            if(Input.GetKey(KeyCode.S))
            {
                
                bool deposited = false;

                orbDepositingMode.depositingMode = true;
                
                enemySpeedControl.SlowDown();

                //Energy
                if (Input.GetKeyDown(KeyCode.J))
                    if (orbCounter.orbsCollected >= 1 && orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax)
                    {
                        orbCounter.planetOrbsDeposited++;
                        deposited = true;
                        factoryDeposited = OrbFactoryDeposited.Power;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Ammo
                if (Input.GetKeyDown(KeyCode.I))
                    if (orbCounter.orbsCollected >= 2)
                    {
                        deposited = true;
                        bulletCount.currentBullets = bulletCount.maxBullets;
                        factoryDeposited = OrbFactoryDeposited.Ammo;
                    }
                    else
                        cannotDepositSoundEffect.Play();

                //Shield
                if (Input.GetKeyDown(KeyCode.L))
                    if (orbCounter.orbsCollected >= 3 && gameManager.IsShieldEnabled() && shieldLogic.CanAddShields())
                    {
                        deposited = true;
                        shieldLogic.AddShield();
                        factoryDeposited = OrbFactoryDeposited.Shield;
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
                            factoryDeposited = OrbFactoryDeposited.Health;
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
}
