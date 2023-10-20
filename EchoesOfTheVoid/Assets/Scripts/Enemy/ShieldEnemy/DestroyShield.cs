using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{
    
    [SerializeField] private ActivateShield activateShield;
    [SerializeField] private DirectEnemyMovement directEnemyMovement;
    [SerializeField] private Animator animator;

    void DeactivateShield()
    {
        activateShield.Deactivate();
    }

    void Angry()
    {
        AudioManager.Instance.PlaySFX("ShieldEnemyAngry");
        directEnemyMovement.SpeedUp();
        animator.SetTrigger("isAngry");
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (activateShield.IsActive())
            {
                //destroy the shield
                AudioManager.Instance.PlaySFX("ShieldEnemyShieldDestroyed");
                 Invoke("Angry", 0.3f);
                
                Destroy(other.gameObject);
                Invoke("DeactivateShield", 0.5f);
            }
        } 
    }
}
