using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;

    public SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start(){
        spriteRenderer.sprite = enabledFactorySprite;
    }

    public void OnMouseDown(){
        if(orbCounter.orbsCollected >= factoryCosts.powerCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
