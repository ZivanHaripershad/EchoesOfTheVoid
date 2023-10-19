using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOrbStealingEnemy : DestroyEnemy
{

    [SerializeField] private int health;

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);

            if (--health == 0)
            {
                AudioManager.Instance.PlaySFX("DestroyOrbStealingEnemy");
                DestroyGameObject(collision, true, collision.gameObject.transform);
            }
            else
            {
                //damage 
                AudioManager.Instance.PlaySFX("OrbStealingEnemyDamage");
                
            }
        }
    }
}
