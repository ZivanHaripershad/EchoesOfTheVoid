using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public ShieldCounter shieldCounter;

    [SerializeField]
    private AudioSource depositSoundEffect;

    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.shieldCost && !shieldCounter.isShieldActive){
            orbCounter.orbsCollected = orbCounter.orbsCollected - factoryCosts.shieldCost;
            OrbCounterUI.instance.UpdateOrbs(orbCounter.orbsCollected);

            shieldCounter.isShieldActive = true;
            shieldCounter.currentShieldAmount = 3;

            depositSoundEffect.Play();
        }
    }

    public void OnMouseEnter()
    {
        transform.localScale = new Vector3(0.18f, 0.18f, 0f); //adjust these values as you see fit
    }


    public void OnMouseExit()
    {
        transform.localScale = new Vector3(0.15f, 0.15f, 1f);  // assuming you want it to return to its original size when your mouse leaves it.
    }
}
