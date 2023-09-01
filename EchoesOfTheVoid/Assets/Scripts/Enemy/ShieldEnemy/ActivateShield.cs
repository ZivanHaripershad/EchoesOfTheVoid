using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool isActive;

    private void Start()
    {
        animator.SetBool("shieldActive", false);
        isActive = false;
    }

    public void Activate()
    {
        isActive = true;
        animator.SetBool("shieldActive", true);
    }

    public bool IsActive()
    {
        return isActive;
    }
}
