using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel1 : EnemySpawning {
    
    [SerializeField] private GameObject enemy;

    private Vector3[] zigzagEnemySpawnPoints =
    {
        new Vector3(-9.3f, 0, 0), 
        new Vector3(9.3f, 0, 0)
    };
    
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

            for (int i = 0; i < enemiesToSpawn ; i++)
            {
                //adds a variation to when the enemy spawns in the wave
                float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                Instantiate(enemy, Vector3.zero, quaternion.identity);
            }

            //waves
            yield return new WaitForSeconds(gameManagerData.timeTillNextWave);
        }

    }
}

