using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TutorialEnemySpawning : EnemySpawning {
    
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimerVariation;
    [SerializeField] private GameObject enemy;
    
    public void StartSpawningEnemies(int numToSpawn, bool continueSpawning)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnEnemiesCoroutine(numToSpawn, continueSpawning));
        }
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
            }

            //waves
            yield return new WaitForSeconds(spawnRate);

            if (!continueSpawning)
                break;
        }

    }
}

