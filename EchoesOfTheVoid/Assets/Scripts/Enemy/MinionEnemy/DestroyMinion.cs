using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroyMinion : MonoBehaviour
{

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject orb;
    
    [SerializeField]
    GameObject healthOrb;
    
    public GameManagerData gameManagerData;
    private ObjectiveManager objectiveManager;

    [SerializeField] protected GameObject minusOne;
    [SerializeField] protected GameObject minusTwo;
    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }
  

    public void DestroyGameObject(Collider2D collision, bool musSpawnOrb, Transform orbSpawnPoint)
    {
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        Instantiate(explosion, orbSpawnPoint.position, orbSpawnPoint.rotation);

        if (musSpawnOrb)
        {
            SpawnOrb(collision);
            
            if (!GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
            {
                if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level3) && GameStateManager.Instance.IsLevel2Completed)
                {
                    if (SelectedUpgradeLevel2.Instance != null && SelectedUpgradeLevel2.Instance.GetUpgrade() != null
                        && SelectedUpgradeLevel2.Instance.GetUpgrade().GetName().Equals("HealthUpgrade"))
                    {
                        int result = Random.Range(0, 5);

                        if (result == 1)
                        {
                            SpawnHealthOrb(collision);
                        }
                    }
                }
                else if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level2))
                {
                    if (SelectedUpgradeLevel2.Instance != null && SelectedUpgradeLevel2.Instance.GetUpgrade() != null
                                                               && SelectedUpgradeLevel2.Instance.GetUpgrade().GetName().Equals("HealthUpgrade"))
                    {
                        int result = Random.Range(0, 5);

                        if (result == 1)
                        {
                            SpawnHealthOrb(collision);
                        }
                    }
                }
            }
        }
        
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
            
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("DoubleDamageBullet"))
        {
            
            if (collision.gameObject.CompareTag("Bullet"))
            {
                GameObject damage = Instantiate(minusOne, gameObject.transform.position, gameObject.transform.rotation);
                damage.GetComponent<DamageNumber>().AddBulletForce(collision.gameObject.transform.rotation);
             
                AudioManager.Instance.PlaySFX("DestroyEnemy");
                DestroyGameObject(collision, true, collision.gameObject.transform);
                
                //destroy the bullet
                Destroy(collision.gameObject);
            }
            
            if (collision.gameObject.CompareTag("DoubleDamageBullet"))
            {
                GameObject damage = Instantiate(minusTwo, gameObject.transform.position, gameObject.transform.rotation);
                damage.GetComponent<DamageNumber>().AddBulletForce(collision.gameObject.transform.rotation);
                DestroyGameObject(collision, true, collision.gameObject.transform);
                AudioManager.Instance.PlaySFX("DestroyEnemy");
                //destroy the bullet
                Destroy(collision.gameObject);
            }
        }
    }

    private void SpawnOrb(Collider2D collision)
    {
        gameManagerData.numberOfEnemiesKilled++;
        AchievementsManager.Instance.IncrementNumOfEnemiesKilled();
        AudioManager.Instance.PlaySFX("DestroyEnemy");

        if (GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Level1))
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
    
    private void SpawnHealthOrb(Collider2D collision)
    {
        GameObject myHealthOrb = Instantiate(healthOrb, transform.position, Quaternion.identity); //instantiate an orb
        Rigidbody2D rb = myHealthOrb.GetComponent<Rigidbody2D>();

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