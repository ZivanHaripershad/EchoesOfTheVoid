using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;

    // Start is called before the first frame update
    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.bulletCost){
            orbCounter.orbsCollected = orbCounter.orbsCollected - factoryCosts.bulletCost;
            OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);
        }
    }
}
