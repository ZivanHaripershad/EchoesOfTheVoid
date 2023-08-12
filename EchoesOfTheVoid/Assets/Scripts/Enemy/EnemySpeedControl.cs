using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedControl : MonoBehaviour
{
    
    [SerializeField] private float enemyNormalSpeed;
    [SerializeField] private float enemyReducedSpeed;

    private float currentSpeed;

    private void Start()
    {
        currentSpeed = enemyNormalSpeed;
    }

    public void SpeedUp()
    {
        currentSpeed = enemyNormalSpeed;
    }

    public void SlowDown()
    {
        currentSpeed = enemyReducedSpeed;
    }

    public float getSpeed()
    {
        return currentSpeed;
    }
}
