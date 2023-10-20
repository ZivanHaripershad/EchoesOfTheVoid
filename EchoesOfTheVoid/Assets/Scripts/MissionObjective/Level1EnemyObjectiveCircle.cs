using System;
using System.Collections.Generic;
using UnityEngine;


public class Level1EnemyObjectiveCircle : ObjectiveCircle
{
    public void Update()
    {
        int currSprite = (int) Mathf.Round((gameManagerData.numberOfEnemiesKilled * 1.0f / 20) * (sprites.Length - 1));

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
