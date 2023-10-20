using System;
using System.Collections.Generic;
using UnityEngine;


public class Level2EnemyObjectiveCircle : ObjectiveCircle
{

    [SerializeField] private Level2Data level2Data;

    public void Update()
    {
        int currSprite = (int) Mathf.Round((level2Data.mothershipDamageTaken * 1.0f / level2Data.maxMothershipHealth) * (sprites.Length - 1));

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
