using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawning : MonoBehaviour {
    
    [SerializeField] private float spawnRate;
    [SerializeField] private int minEnemiesToSpawn;
    [SerializeField] private int maxEnemiesToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimerVariation;
    [SerializeField] private GameObject enemy;

    private bool hasStarted;
    

    public void StartSpawningEnemies()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                float randomNumber = Random.Range(0f, spawnTimerVariation);
                yield return new WaitForSeconds(spawnInterval + randomNumber);
                Instantiate(enemy, Vector3.zero, quaternion.identity);
            }

        }

    }

}

