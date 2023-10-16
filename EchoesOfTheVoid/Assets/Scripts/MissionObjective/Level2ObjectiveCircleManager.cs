﻿using System;
using System.Collections.Generic;
using UnityEngine;


public class Level2ObjectiveCircleManager : MonoBehaviour
{
    private NetworkObjectiveCircle networkObjective;
    private HealthObjectiveCircle healthObjective;
    private Level2EnemyObjectiveCircle enemyObjective;

    private void Start()
    {
        networkObjective = GameObject.FindWithTag("NetworkObjectiveCircle").GetComponent<NetworkObjectiveCircle>();
        healthObjective = GameObject.FindWithTag("HealthObjectiveCircle").GetComponent<HealthObjectiveCircle>();
        enemyObjective = GameObject.FindWithTag("EnemyObjectiveCircle").GetComponent<Level2EnemyObjectiveCircle>();
    }

    private void Update()
    {
        if (networkObjective)
        {
            networkObjective.Update();
        }

        if (healthObjective)
        {
            healthObjective.Update();
        }

        if (enemyObjective)
        {
            enemyObjective.Update();
        }
    }
}
