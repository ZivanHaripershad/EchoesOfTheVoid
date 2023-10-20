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
        int maxNumOfShield = 3;
        
        if ((gameManagerData.level.Equals(GameManagerData.Level.Level2) ||
             gameManagerData.level.Equals(GameManagerData.Level.Level3)) && GameStateManager.Instance.IsLevel1Completed)
        {
            if (SelectedUpgradeLevel1.Instance != null &&
                SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                SelectedUpgradeLevel1.Instance.GetUpgrade().GetName() == "ShieldUpgrade")
            {
                maxNumOfShield = 4;
            }
        }
        else if(gameManagerData.level.Equals(GameManagerData.Level.Level1))
        {
            if (SelectedUpgradeLevel1.Instance != null &&
                SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
                SelectedUpgradeLevel1.Instance.GetUpgrade().GetName() == "ShieldUpgrade")
            {
                maxNumOfShield = 4;
            }
        }
        
        for (int k = 1; k <= maxNumOfShield; k++)
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
