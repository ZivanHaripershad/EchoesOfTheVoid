using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOrbStealingEnemy : DestroyEnemy
{

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioManager.Instance.PlaySFX("DestroyEnemy");
            Destroy(collision.gameObject);
            DestroyGameObject(collision, true, collision.gameObject.transform);
        }
    }
}
