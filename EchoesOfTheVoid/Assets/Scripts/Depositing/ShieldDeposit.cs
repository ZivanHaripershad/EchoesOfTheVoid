using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;
    public ShieldCounter shieldCounter;

    public SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start(){
        spriteRenderer.sprite = enabledFactorySprite;
    }

    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.shieldCost && !shieldCounter.isShieldActive){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
