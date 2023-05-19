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

            if (collision.gameObject.tag != "Earth")
                Instantiate(orb, transform.position, Quaternion.identity); //instantiate an orb

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