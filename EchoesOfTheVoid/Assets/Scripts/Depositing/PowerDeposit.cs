using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : Deposit
{

    override 
    public void RenderSprites(){
        if (gameManagerData.level.Equals( GameManagerData.Level.Tutorial))
        {
            if (!tutorialData.depositPower)
            {
                spriteRenderer.sprite = disabledFactorySprite;
                return;
            }
            
            spriteRenderer.sprite = enabledFactorySprite;
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.powerCost && orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
