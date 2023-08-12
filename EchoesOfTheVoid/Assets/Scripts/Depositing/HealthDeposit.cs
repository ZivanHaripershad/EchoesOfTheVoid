using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public HealthCount healthCount;

    public GameObject canvasUI;

    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;
    private SpriteRenderer spriteRenderer;
    

    private Text healthLowMessage;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enabledFactorySprite;
        healthLowMessage = canvasUI.transform.Find("HealthLowMessage").GetComponent<Text>();
        healthLowMessage.enabled = false;
    }

    public void RenderSprites(){
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

        if (healthCount.currentHealth > 0 && healthCount.currentHealth <= lowHealthBounds)
        {
            return HealthCount.HealthStatus.LOW;
        }
        if (healthCount.currentHealth > lowHealthBounds && healthCount.currentHealth <= mediumHealthBounds)
        {
            return HealthCount.HealthStatus.MEDIUM;
        } 
        if (healthCount.currentHealth > mediumHealthBounds && healthCount.currentHealth <= healthCount.maxHealth)
        {
            return HealthCount.HealthStatus.HIGH;
        }

        return HealthCount.HealthStatus.HIGH;
    }

    public void LowHealthStatus()
    {
        healthLowMessage.enabled = true;
    }
}
