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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (--health <= 0)
            {
                AudioManager.Instance.PlaySFX("");
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
