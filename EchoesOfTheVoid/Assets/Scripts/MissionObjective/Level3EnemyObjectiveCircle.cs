using System;
using System.Collections.Generic;
using UnityEngine;


public class Level3EnemyObjectiveCircle : ObjectiveCircle
{

    [SerializeField] private Level3Data level3Data;

    public void Update()
    {
        int currSprite = (int) Mathf.Round((level3Data.mineEnemyDamageTaken * 1.0f / level3Data.mineEnemyMaxHealth) * (sprites.Length - 1));

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
