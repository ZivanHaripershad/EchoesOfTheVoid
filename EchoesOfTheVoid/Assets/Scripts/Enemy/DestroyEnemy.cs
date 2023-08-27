using System.Collections;
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
    private bool canBeDestroyed;

    [SerializeField] private float bulletSoundDelay;

    [SerializeField] private GameObject graphics;

    private Animator earthDamageAnimator;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        canBeDestroyed = true;
        earthDamageAnimator = GameObject.FindGameObjectWithTag("EarthDamage").GetComponent<Animator>();
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void checkDamage()
    {
        if (healthCount.currentHealth < healthCount.maxHealth * 0.8) //20% damage
            earthDamageAnimator.SetTrigger("damage1");
        if (healthCount.currentHealth < healthCount.maxHealth * 0.6) //40% damage
            earthDamageAnimator.SetTrigger("damage2");
        if (healthCount.currentHealth < healthCount.maxHealth * 0.4) //60% damage
            earthDamageAnimator.SetTrigger("damage3");
        if (healthCount.currentHealth < healthCount.maxHealth * 0.2) //80% damage
            earthDamageAnimator.SetTrigger("damage4");
        if (healthCount.currentHealth < healthCount.maxHealth * 0.1) //90% damage
            earthDamageAnimator.SetTrigger("damage5");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        bool planetDamage = false;
        
        if (canBeDestroyed && collision.gameObject.CompareTag("EarthSoundTrigger"))
            crashIntoPlanetSoundEffect.Play();
            
        if (canBeDestroyed && (collision.gameObject.CompareTag("Earth") || collision.gameObject.CompareTag("Bullet")))
        {
            canBeDestroyed = false;

            if(collision.gameObject.CompareTag("Earth") && shieldCounter.isShieldActive)
            {
                Debug.Log("shield damage");
                shieldCounter.currentShieldAmount--;
            }

            if (collision.gameObject.CompareTag("Earth") && !shieldCounter.isShieldActive)
            {
                healthCount.currentHealth--;
                checkDamage();
            }
            
            if (collision.gameObject.CompareTag("Bullet"))
            {
                gameManagerData.numberOfEnemiesKilled++;
                destroyEnemySoundEffect.Play();
                
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
                
                //destroy the bullet
                Destroy(collision.gameObject);
            }

            //Instantiate the explosion
            Instantiate(explosion, transform.position, Quaternion.identity);
            
            //Set the opacity to 0
            graphics.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            
            Invoke("Destroy", bulletSoundDelay);

        }
    }
}