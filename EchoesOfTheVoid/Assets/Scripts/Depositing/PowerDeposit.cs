using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : Deposit
{
    override 
    public void RenderSprites(){
        if(orbCounter.orbsCollected >= factoryCosts.powerCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
