using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour {
    
    [SerializeField] protected int minEnemiesToSpawn;
    [SerializeField] protected int maxEnemiesToSpawn;
    [SerializeField] protected GameManagerData gameManagerData;
    protected Coroutine currentCoroutine;
    protected bool hasStarted;

    public void ResetSpawning()
    {
        hasStarted = false;
    }
    
    
    public void StopSpawning()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }
    

    public void DestroyActiveEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}

