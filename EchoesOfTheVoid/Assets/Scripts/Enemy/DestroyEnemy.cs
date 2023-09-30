using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyEnemy : MonoBehaviour
{

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject orb;
    
    public ShieldCounter shieldCounter;
    public GameManagerData gameManagerData;
    
    [SerializeField] private AudioSource destroyEnemySoundEffect;
    [SerializeField] private AudioSource crashIntoPlanetSoundEffect;
    [SerializeField] private HealthCount healthCount;
    [SerializeField] private float bulletSoundDelay;
    [SerializeField] private GameObject graphics;
    private ObjectiveManager objectiveManager;

    private Animator earthDamageAnimator; 

    private ActivateShield activateShield; 
    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        earthDamageAnimator = GameObject.FindGameObjectWithTag("EarthDamage").GetComponent<Animator>();
        activateShield = GetComponentInChildren<ActivateShield>();
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    void checkDamage()
    {
        Debug.Log("shaking camera"); 
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>().Shake();
        
        if (healthCount.currentHealth < healthCount.maxHealth * 0.8) //20% damage
            earthDamageAnimator.SetBool("damage1", true);
            
        if (healthCount.currentHealth < healthCount.maxHealth * 0.6) //40% damage
            earthDamageAnimator.SetBool("damage2", true);
        
        if (healthCount.currentHealth < healthCount.maxHealth * 0.4) //60% damage
            earthDamageAnimator.SetBool("damage3", true);
        
        if (healthCount.currentHealth < healthCount.maxHealth * 0.2) //80% damage
            earthDamageAnimator.SetBool("damage4", true);
        
        if (healthCount.currentHealth < healthCount.maxHealth * 0.1) //90% damage
            earthDamageAnimator.SetBool("damage5", true);
    }

    public void DestroyGameObject(Collider2D collision, bool musSpawnOrb, Transform orbSpawnPoint)
    {
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        graphics.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        
        AudioManager.Instance.PlaySFX("DestroyEnemy");
        Instantiate(explosion, orbSpawnPoint.position, orbSpawnPoint.rotation);
        
        if (musSpawnOrb)
            SpawnOrb(collision);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool planetDamage = false;
        
        if (collision.gameObject.CompareTag("EarthSoundTrigger"))
            crashIntoPlanetSoundEffect.Play();
            
        if (collision.gameObject.CompareTag("Earth") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("DoubleDamageBullet"))
        {
            if(collision.gameObject.CompareTag("Earth") && shieldCounter.isShieldActive)
            {
                Debug.Log("shield damage");
                shieldCounter.currentShieldAmount--;
            }

            if (collision.gameObject.CompareTag("Earth") && !shieldCounter.isShieldActive)
            {
                healthCount.currentHealth--;
                checkDamage();
                DestroyGameObject(collision, false, gameObject.transform);
            }
            
            if (collision.gameObject.CompareTag("Bullet"))
            {
                if (activateShield != null && activateShield.IsActive()) //has shield
                {
                }
                else { //no shield
                    DestroyGameObject(collision, true, collision.gameObject.transform);
                }

                //destroy the bullet
                Destroy(collision.gameObject);
            }
            
            if (collision.gameObject.CompareTag("DoubleDamageBullet"))
            {
                
                DestroyGameObject(collision, true, collision.gameObject.transform);

                //destroy the bullet
                Destroy(collision.gameObject);
            }
                
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            if (collision.gameObject.GetComponent<ShieldLogic>().DestroyShield(gameObject.transform.position))
            {
                DestroyGameObject(collision, false, collision.gameObject.transform);
                AudioManager.Instance.PlaySFX("EarthShieldDestroyed");
            }
        }
    }

    private void SpawnOrb(Collider2D collision)
    {
        gameManagerData.numberOfEnemiesKilled++;
        destroyEnemySoundEffect.Play();

        if (gameManagerData.level.Equals(GameManagerData.Level.Level1))
        {
            objectiveManager.UpdateEnemiesDestroyedBanner();
        }

        GameObject myOrb = Instantiate(orb, transform.position, Quaternion.identity); //instantiate an orb
        Rigidbody2D rb = myOrb.GetComponent<Rigidbody2D>();

        //get the collision movement direction
        Vector2 vel = collision.gameObject.GetComponent<Rigidbody2D>().GetRelativeVector(Vector3.right);

        float jitterX;
        if (vel.x > 0)
            jitterX = Random.Range(0.1f, 2f);
        else
            jitterX = Random.Range(-2f, -0.1f);

        float jitterY;
        if (vel.y > 0)
            jitterY = Random.Range(0.1f, 2f);
        else
            jitterY = Random.Range(-2f, -0.1f);

        Vector2 withJitter = new Vector2((vel.x + jitterX) * 100, (vel.y + jitterY) * 100);

        rb.AddForce(withJitter);
    }
}