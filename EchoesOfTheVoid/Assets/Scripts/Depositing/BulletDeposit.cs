using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeposit : Deposit
{
    override 
    public void RenderSprites(){
        if(orbCounter.orbsCollected >= factoryCosts.bulletCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
