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

    public void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0f); //adjust these values as you see fit
    }


    public void OnMouseExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);  // assuming you want it to return to its original size when your mouse leaves it.
    }
}
