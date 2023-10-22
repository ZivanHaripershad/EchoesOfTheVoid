using UnityEngine;

public class PowerDeposit : Deposit
{
    
    override 
    public void RenderSprites(){
        if (GameStateManager.Instance.CurrentLevel.Equals( GameManagerData.Level.Tutorial))
        {
            if (!tutorialData.depositPower)
            {
                spriteRenderer.sprite = disabledFactorySprite;
                return;
            }
            
            spriteRenderer.sprite = enabledFactorySprite;
        }

        if (GameStateManager.Instance.CoolDownTime >= GameStateManager.Instance.MaxDepositCoolDown 
            && !GameStateManager.Instance.IsCooledDown)
        {
            GameStateManager.Instance.CoolDownTime = 0f;
            GameStateManager.Instance.IsCooledDown = true;
        }
        
        if (!GameStateManager.Instance.IsCooledDown)
        {
            spriteRenderer.sprite = disabledFactorySprite;
            return;
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.powerCost && orbCounter.planetOrbsDeposited < orbCounter.planetOrbMax){
            GameStateManager.Instance.CoolDownTime = 0f;
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
