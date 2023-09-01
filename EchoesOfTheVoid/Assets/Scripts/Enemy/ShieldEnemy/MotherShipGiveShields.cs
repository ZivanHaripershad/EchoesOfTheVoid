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
            Debug.Log("Trigger entered of type enemy");
            ActivateShield activateShield = other.GetComponent<ActivateShield>();
            if (activateShield != null)
            {
                activateShield.Activate();
                animator.SetTrigger("isActive");
            }
        }
    }
}
