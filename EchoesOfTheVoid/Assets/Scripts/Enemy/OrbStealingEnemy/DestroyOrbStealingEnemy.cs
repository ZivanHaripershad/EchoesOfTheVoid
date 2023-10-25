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
            health -= collision.gameObject.CompareTag("Bullet") ? 1 : 2;
            GameObject minus = null;

            Transform pos = gameObject.transform;
            if (collision.gameObject.CompareTag("Bullet"))
            {
                minus = Instantiate(minusOne, pos.position, pos.rotation);
            }
            else
            {
                minus = Instantiate(minusTwo, pos.position, pos.rotation);
            }
            
            Destroy(collision.gameObject);
            
            minus.GetComponent<DamageNumber>().AddBulletForce(collision.gameObject.transform.rotation);

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
}
