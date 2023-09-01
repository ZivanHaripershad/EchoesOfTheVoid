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

    void SetShieldActive()
    {
        isActive = true;
    }

    public void Activate()
    {
        animator.SetBool("shieldActive", true);
        Invoke("SetShieldActive", 1.5f);
    }

    public bool IsActive()
    {
        return isActive;
    }
}
