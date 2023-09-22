using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : Deposit
{

    public OrbCounter orbCounter;
    override 
    public void RenderSprites(){
        if(orbCounter.orbsCollected >= factoryCosts.powerCost && orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
