using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class MotherShipHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int lowHealth;
    [SerializeField] private Animator animator;

    [SerializeField] private Level2Data level2Data;
    
    private ObjectiveManager objectiveManager;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        objectiveManager = GameObject.FindWithTag("ObjectiveManager").GetComponent<ObjectiveManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        level2Data.mothershipDamageTaken += damage;
        
        float healthPercentage = 0f;
        
        if (health <= 0)
        {
            healthPercentage = 100;
        }
        else
        {
            healthPercentage = ((maxHealth - health) / maxHealth) * 100; 
        }
        
        if (healthPercentage % 20 == 0)
        {
            objectiveManager.UpdatePrimaryTargetHealthBanner(healthPercentage);
        }
        
        if (health <= lowHealth)
            animator.SetBool("isLowHealth", true);
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
