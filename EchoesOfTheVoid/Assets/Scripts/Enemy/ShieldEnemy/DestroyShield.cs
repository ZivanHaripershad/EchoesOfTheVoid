using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{
    
    [SerializeField] private ActivateShield activateShield;

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
                AudioManager.Instance.PlaySFX("EarthShieldDestroyed");
                
                Destroy(other.gameObject);
                Invoke("DeactivateShield", 0.5f);
            }
        } 
    }
}
