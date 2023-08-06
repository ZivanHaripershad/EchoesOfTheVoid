using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public HealthCount healthCount;

    private SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = disabledFactorySprite;
    }

    void Update(){
        if(orbCounter.orbsCollected >= factoryCosts.healthCost && healthCount.currentHealth < healthCount.maxHealth){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }

    public HealthCount.HealthStatus GetHealthStatus()
    {
        var lowHealthBounds = Math.Ceiling(healthCount.maxHealth * 0.25);
        var mediumHealthBounds = Math.Ceiling(healthCount.maxHealth * 0.5);
        var highHealthBounds = Math.Ceiling(healthCount.maxHealth * 0.75);
        
        if (healthCount.currentHealth > 0 && healthCount.currentHealth <= lowHealthBounds)
        {
            return HealthCount.HealthStatus.LOW;
        }
        if (healthCount.currentHealth > lowHealthBounds && healthCount.currentHealth <= mediumHealthBounds)
        {
            return HealthCount.HealthStatus.MEDIUM;
        } 
        if (healthCount.currentHealth > mediumHealthBounds && healthCount.currentHealth <= highHealthBounds)
        {
            return HealthCount.HealthStatus.HIGH;
        }

        return HealthCount.HealthStatus.HIGH;
    }
}
