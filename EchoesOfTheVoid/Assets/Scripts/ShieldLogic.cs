using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldLogic : MonoBehaviour
{

    [SerializeField] private Animator shieldAnimator;
    [SerializeField] private GameManagerData gameManagerData;

    private int shieldCount;

    private void Start()
    {
        shieldCount = 0;
    }

    private void Update()
    {
        if (shieldCount == 0)
        {
            gameManagerData.isShieldUp = false;
            return;
        }

        gameManagerData.isShieldUp = true;
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

    public void AddShield()
    {
        for (int k = 1; k < 4; k++)
        {
            shieldCount = k;
            shieldAnimator.SetInteger("shieldCount", k);
        }
    }

    public bool CanAddShields()
    {
        return shieldCount == 0;
    }
}
