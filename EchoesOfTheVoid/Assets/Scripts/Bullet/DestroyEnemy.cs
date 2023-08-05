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
    
    [SerializeField] private AudioSource explosionSoundEffect;
    [SerializeField] private HealthCount healthCount;
    private bool canBeDestroyed;
    private GameObject earth;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        canBeDestroyed = true;
        earth = GameObject.FindGameObjectWithTag("Earth");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDestroyed && (collision.gameObject.CompareTag("Earth") || collision.gameObject.CompareTag("Bullet")))
        {
            canBeDestroyed = false;

            explosionSoundEffect.Play();

            if(collision.gameObject.CompareTag("Earth") && shieldCounter.isShieldActive)
            {
                Debug.Log("shield damage");
                shieldCounter.currentShieldAmount = shieldCounter.currentShieldAmount -1;
            }

            if (collision.gameObject.CompareTag("Earth") && !shieldCounter.isShieldActive)
            {
                healthCount.currentHealth--;
            }

            if (collision.gameObject.CompareTag("Bullet"))
            {
                gameManagerData.numberOfEnemiesKilled++;
            }

            if (!collision.gameObject.CompareTag("Earth"))
            {
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

            //Instantiate the explosion
            Instantiate(explosion, transform.position, Quaternion.identity);
            
            //destroy the enemy 
            Destroy(gameObject);

            //destroy the bullet
            if (collision.gameObject.CompareTag("Bullet"))
                Destroy(collision.gameObject);
        }
    }
}