using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : Deposit
{
    override 
    public void RenderSprites(){
        if (!gameManager.IsShieldEnabled())
        {
            spriteRenderer.sprite = disabledFactorySprite;
            return;
        }
        
        if (gameManagerData.level.Equals( GameManagerData.Level.Tutorial))
        {
            if (!tutorialData.depositShield)
            {
                spriteRenderer.sprite = disabledFactorySprite;
                return;
            }
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.shieldCost && !gameManagerData.isShieldUp){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
