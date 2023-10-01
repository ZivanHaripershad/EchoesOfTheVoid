using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel3 : EnemySpawning {
    
    [SerializeField] private GameObject pathFollowingEnemy;
    [SerializeField] private GameObject shieldEnemy;
    [SerializeField] private Transform[] shieldEnemySpawnPoints; 
    

    public void StartSpawningAllTypesOfEnemies()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnAllTypesOfEnemiesCoroutine());
        }
    }

    public void StartSpawningLevel3Enemies()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnAllTypesOfEnemiesCoroutine());
        }
    }
    
    private IEnumerator SpawnAllTypesOfEnemiesCoroutine()
    {
        while (true)
        {
            int result = Random.Range(0, 2);
            
            if (result == 0)
            {
                int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
            
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    //enemies in a wave
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                    Instantiate(pathFollowingEnemy, Vector3.zero, quaternion.identity);
                }
            }
            else
            {
                float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                
                //get a random spawn point
                int point = Random.Range(0, shieldEnemySpawnPoints.Length - 1);
                Instantiate(shieldEnemy, new Vector3(
                        shieldEnemySpawnPoints[point].transform.position.x,
                        shieldEnemySpawnPoints[point].transform.position.y, 0),
                    quaternion.identity);
            }
            
            
        }
    }
}

