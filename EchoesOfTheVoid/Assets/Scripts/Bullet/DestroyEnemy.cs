using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{

    [SerializeField]
    GameObject explosion;

    [SerializeField]
    GameObject orb;

    private bool canBeDestroyed = false;

    public ShieldCounter shieldCounter;

    [SerializeField]
    private AudioSource explosionSoundEffect;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        canBeDestroyed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeDestroyed && (collision.gameObject.tag == "Earth" || collision.gameObject.tag == "Bullet"))
        {
            canBeDestroyed = false;

            explosionSoundEffect.Play();

            if(collision.gameObject.tag == "Earth" && shieldCounter.isShieldActive){
                shieldCounter.currentShieldAmount = shieldCounter.currentShieldAmount -1;
            }

            if (collision.gameObject.tag != "Earth")
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
            if (collision.gameObject.tag == "Bullet")
                Destroy(collision.gameObject);
        }
    }
}