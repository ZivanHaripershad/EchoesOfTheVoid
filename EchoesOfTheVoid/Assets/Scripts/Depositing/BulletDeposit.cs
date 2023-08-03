using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeposit : MonoBehaviour
{
    public OrbCounter orbCounter;
    public FactoryCosts factoryCosts; 
    private SpriteRenderer spriteRenderer;
    public Sprite enabledFactorySprite;
    public Sprite disabledFactorySprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = disabledFactorySprite;
    }

    // Start is called before the first frame update
    void Update(){
        if(orbCounter.orbsCollected >= factoryCosts.bulletCost){
            spriteRenderer.sprite = enabledFactorySprite;
        }
        else{
            spriteRenderer.sprite = disabledFactorySprite;
        }
    }
 
}
