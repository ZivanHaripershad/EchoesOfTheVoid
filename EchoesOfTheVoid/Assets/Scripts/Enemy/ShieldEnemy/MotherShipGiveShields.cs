using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotherShipGiveShields : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ActivateShield activateShield = other.GetComponentInChildren<ActivateShield>();
            if (activateShield != null && activateShield.CanBeActivated())
            {
                activateShield.Activate();
                animator.SetTrigger("isActive");
            }
        }
    }
}
