using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : Deposit
{
    public ShieldCounter shieldCounter;

    override 
    public void RenderSprites(){
        if (!gameManager.IsShieldEnabled())
        {
            spriteRenderer.sprite = disabledFactorySprite;
            return;
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.shieldCost && !shieldCounter.isShieldActive){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
