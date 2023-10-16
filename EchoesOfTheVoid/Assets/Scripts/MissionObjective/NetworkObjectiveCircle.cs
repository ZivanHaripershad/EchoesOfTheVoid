using System;
using System.Collections.Generic;
using UnityEngine;


public class NetworkObjectiveCircle : ObjectiveCircle
{
    [SerializeField] private OrbCounter counter;
    
    public void Update()
    {
        int currSprite = (int) Mathf.Round((counter.planetOrbsDeposited * 1.0f / counter.planetOrbMax) * (sprites.Length - 1));

        if (prevSprite > currSprite)
        {
            prevSprite = currSprite;
        }

        if (prevSprite < currSprite)
        {
            prevSprite++;
        }

        if (prevSprite > sprites.Length - 1)
            prevSprite = sprites.Length - 1;
        
        spriteRenderer.sprite = sprites[prevSprite];
    }
}
