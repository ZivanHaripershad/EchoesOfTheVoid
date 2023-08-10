using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public ShieldCounter shieldCounter;

    public GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = disabledFactorySprite;
    }

    void Update(){
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
