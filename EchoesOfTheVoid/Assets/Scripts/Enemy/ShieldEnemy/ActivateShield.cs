using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator.SetBool("shieldActive", false);
    }

    public void Activate()
    {
        animator.SetBool("shieldActive", true);
    }

    public bool IsActive()
    {
        return animator.GetBool("shieldActive");
    }
}
