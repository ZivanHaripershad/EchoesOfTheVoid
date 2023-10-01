using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDeposit : Deposit
{
    [SerializeField] private HealthCount healthCount;
    [SerializeField] private GameObject canvasUI;
    private Text healthLowMessage;

    void Start(){
        spriteRenderer.sprite = disabledFactorySprite;
        healthLowMessage = canvasUI.transform.Find("HealthLowMessage").GetComponent<Text>();
        healthLowMessage.enabled = false;
    }

    override 
    public void RenderSprites(){
        if (gameManagerData.level.Equals( GameManagerData.Level.Tutorial))
        {
            if (!tutorialData.depositHealth)
            {
                spriteRenderer.sprite = disabledFactorySprite;
                return;
            }
        }
        
        if(orbCounter.orbsCollected >= factoryCosts.healthCost && healthCount.currentHealth < healthCount.maxHealth){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }

    public HealthCount.HealthStatus GetHealthStatus()
    {
        var lowHealthBounds = Math.Ceiling(healthCount.maxHealth * 0.3);
        var mediumHealthBounds = Math.Ceiling(healthCount.maxHealth * 0.7);

        if (healthCount.currentHealth <= lowHealthBounds)
        {
            return HealthCount.HealthStatus.LOW;
        }
        if (healthCount.currentHealth > lowHealthBounds && healthCount.currentHealth <= mediumHealthBounds)
        {
            return HealthCount.HealthStatus.MEDIUM;
        } 
        
        return HealthCount.HealthStatus.HIGH;
    }

    public void LowHealthStatus()
    {
       healthLowMessage.enabled = true;
    }
}
