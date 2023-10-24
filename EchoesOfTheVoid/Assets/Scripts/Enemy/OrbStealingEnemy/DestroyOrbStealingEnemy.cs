using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOrbStealingEnemy : DestroyEnemy
{

    [SerializeField] private int health;
    [SerializeField] private int lowHealth;
    [SerializeField] private GameObject smoke;

    void Start()
    {
        smoke.SetActive(false);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("DoubleDamageBullet"))
        {
            Destroy(collision.gameObject);

            if (collision.gameObject.CompareTag("Bullet"))
            {
                SpawnDamageNumber(collision, minusOne);
                health--;
            }
            else
            {
                SpawnDamageNumber(collision, minusTwo);
                health -= 2;
            }

            if (health <= 0)
            {
                AudioManager.Instance.PlaySFX("DestroyOrbStealingEnemy");
                DestroyGameObject(collision, false, collision.gameObject.transform);
            }
            else
            {
                //damage 
                AudioManager.Instance.PlaySFX("DamageOrbStealingEnemy");
                if (health == lowHealth)
                    smoke.SetActive(true);
            }
            
            Debug.Log(health);
        }
    }

    private void SpawnDamageNumber(Collider2D collision, GameObject toSpawn)
    {
        GameObject min = Instantiate(toSpawn, gameObject.transform.position, gameObject.transform.rotation);
        min.GetComponent<DamageNumber>().AddBulletForce(collision.gameObject.transform.rotation);
    }
}
