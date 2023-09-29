using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeposit : Deposit
{
    override 
    public void RenderSprites(){
        if (gameManagerData.level.Equals( GameManagerData.Level.Tutorial))
        {
            if (!tutorialData.depositAmmo)
            {
                spriteRenderer.sprite = disabledFactorySprite;
                return;
            }
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.bulletCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
