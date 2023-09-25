using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour
{

    [SerializeField] private Animator shieldAnimator;

    private int shieldCount;

    private void Start()
    {
        shieldCount = 0;
    }

    public bool DestroyShield(Vector3 enemyTransform)
    {
        if (shieldCount > 0)
        {
            //rotate the game object
            Vector3 direction = enemyTransform - Vector3.zero;
            float angleRadians = Mathf.Atan2(direction.y, direction.x);
            float angleDegrees = angleRadians * Mathf.Rad2Deg;
            
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
            
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

    public bool CanAddShields()
    {
        return shieldCount < 3;
    }
}
