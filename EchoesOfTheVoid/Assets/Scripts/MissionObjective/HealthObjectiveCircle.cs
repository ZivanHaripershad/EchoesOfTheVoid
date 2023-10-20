using System;
using System.Collections.Generic;
using UnityEngine;


public class HealthObjectiveCircle : ObjectiveCircle
{
    [SerializeField] private HealthDeposit healthDeposit;

    public void Update()
    {
        if (!HealthCount.HealthStatus.LOW.Equals(healthDeposit.GetHealthStatus()))
        {
            prevSprite = sprites.Length - 1;
            spriteRenderer.sprite = sprites[prevSprite];

        }
        else
        {
            prevSprite = 0;
            spriteRenderer.sprite = sprites[prevSprite];
        }

    }
}
