using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts;

    private SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = disabledFactorySprite;
    }

    void Update(){
        if(orbCounter.orbsCollected >= factoryCosts.powerCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
}
