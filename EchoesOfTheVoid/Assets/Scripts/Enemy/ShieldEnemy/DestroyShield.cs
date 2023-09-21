using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{

    [SerializeField] private AudioSource powerDownEffect;
    [SerializeField] private ActivateShield activateShield;
    [SerializeField] private DestroyEnemy destroyEnemy;

    void DeactivateShield()
    {
        activateShield.Deactivate();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (activateShield.IsActive())
            {
                //destroy the shield
                powerDownEffect.Play();
                
                Destroy(other.gameObject);
                Invoke("DeactivateShield", 0.5f);
            }
        } 
    }
}
