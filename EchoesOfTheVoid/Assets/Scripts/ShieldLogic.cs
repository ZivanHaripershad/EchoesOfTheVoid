using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour
{

    [SerializeField] private Animator shieldAnimator;

    private int shieldCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool DestroyShield()
    {
        if (shieldCount > 0)
        {
            shieldAnimator.SetInteger("shieldCount", --shieldCount);
            return true;
        }

        return false;
    }

    public bool AddShield()
    {
        if (shieldCount < 3)
        {
            shieldAnimator.SetInteger("shieldCount", ++shieldCount);
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Debug.Log("Destoy shield...");
        }
    }
}
