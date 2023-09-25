using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel1 : MonoBehaviour {

    [SerializeField] private float spawnRate;
    [SerializeField] private int minEnemiesToSpawn;
    [SerializeField] private int maxEnemiesToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimerVariation;
    [SerializeField] private GameObject enemy;
    private Coroutine currentCoroutine;

    private bool hasStarted;

    private int numberSpawned;
    private int maxSpawned;

    public void ResetSpawning()
    {
        hasStarted = false;
    }

    public void StartSpawningEnemies(int numToSpawn, bool continueSpawning)
    {
        numberSpawned = 0;
        maxSpawned = numToSpawn;

        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnEnemiesCoroutine(numToSpawn, continueSpawning));
        }
    }
    
    public void StopTheCoroutine()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
    }

    private IEnumerator SpawnEnemiesCoroutine(int enemiesToSpawn, bool continueSpawning)
    {
        while (true)
        {

            if (continueSpawning)
            {
                enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
            }

            for (int i = 0; i < enemiesToSpawn || continueSpawning; i++)
            {
                //enemies in a wave
                float randomNumber = Random.Range(0f, spawnTimerVariation);
                yield return new WaitForSeconds(spawnInterval + randomNumber);
                Instantiate(enemy, Vector3.zero, quaternion.identity);
                numberSpawned++;
                //
                // if (numberSpawned >= maxSpawned)
                //     break;
            }

            //waves
            yield return new WaitForSeconds(spawnRate);

            if (!continueSpawning)
                break;
        }

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

