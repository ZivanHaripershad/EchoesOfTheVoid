using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel1 : EnemySpawning {
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject zigZagEnemy;
    [SerializeField] private int maxEnemiesBeforeZig;
    [SerializeField] private int minEnemiesAfterZig;

    private int currentZigZagSpawnPoint = 0;

    private int currentEnemySpawnCount = 0; 

    private Vector3[] zigzagEnemySpawnPoints =
    {
        new Vector3(-9.3f, 3.57f, 0), 
        new Vector3(9.3f, 0, 0)
    };

    [SerializeField] private int zigZagSpawnOdds;

    public void StartSpawningEnemies()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        while (true)
        {
            int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {

                int result = Random.Range(0, 10);

                if (result == 0)
                {
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                    Instantiate(zigZagEnemy, zigzagEnemySpawnPoints[currentZigZagSpawnPoint++], Quaternion.identity);
                    if (currentZigZagSpawnPoint > zigzagEnemySpawnPoints.Length - 1)
                        currentZigZagSpawnPoint = 0;
                }
                else
                {
                    //purple enemies in a wave
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                    Instantiate(enemy, Vector3.zero, quaternion.identity);
                }
            }
            
            //waves
            yield return new WaitForSeconds(gameManagerData.timeTillNextWave);
        }

    }
}

