using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isActive;
    private bool canBeActivated; 

    private void Start()
    {
        animator.SetBool("shieldActive", false);
        isActive = false;
        canBeActivated = true;
    }

    public void Activate()
    {
        canBeActivated = false;
        if (animator)
        {
            isActive = true;
            animator.SetBool("shieldActive", true);
        }
    }

    public void Deactivate()
    {
        isActive = false;
        animator.SetBool("shieldActive", false);
        Destroy(gameObject);
    }

    public bool CanBeActivated()
    {
        return canBeActivated;
    }

    public bool IsActive()
    {
        return isActive;
    }
}
