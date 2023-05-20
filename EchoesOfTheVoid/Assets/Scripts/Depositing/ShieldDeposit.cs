using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public ShieldCounter shieldCounter;

    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.shieldCost && !shieldCounter.isShieldActive){
            orbCounter.orbsCollected = orbCounter.orbsCollected - factoryCosts.shieldCost;
            OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);

            shieldCounter.isShieldActive = true;
            shieldCounter.currentShieldAmount = 3;
        }
    }
}
