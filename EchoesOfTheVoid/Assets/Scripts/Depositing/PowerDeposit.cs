using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;

    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.powerCost){
            orbCounter.orbsCollected = orbCounter.orbsCollected - factoryCosts.powerCost;
            OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);

            orbCounter.planetOrbsDeposited = orbCounter.planetOrbsDeposited + 1;
        }
    }
}
