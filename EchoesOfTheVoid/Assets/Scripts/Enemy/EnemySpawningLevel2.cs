using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel2 : EnemySpawning {
    
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

    public void StartSpawningLevel2Enemies()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnAllTypesOfEnemiesCoroutine());
        }
    }
    
    public void StartSpawningShieldians()
    {
        if (!hasStarted)
        {
            hasStarted = true;
            currentCoroutine = StartCoroutine(SpawnShieldianEnemiesCoroutine());
        }
    }
    
    private IEnumerator SpawnAllTypesOfEnemiesCoroutine()
    {
        while (true)
        {
            
            int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
        
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int result = Random.Range(0, 3);

                if (result == 0)
                {
                    //shield enemies
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
            
                    //get a random spawn point
                    int point = Random.Range(0, shieldEnemySpawnPoints.Length - 1);
                    Instantiate(shieldEnemy, new Vector3(
                            shieldEnemySpawnPoints[point].transform.position.x,
                            shieldEnemySpawnPoints[point].transform.position.y, 0),
                        quaternion.identity);
                }
                else
                {
                    //purple enemies in a wave
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                    Instantiate(pathFollowingEnemy, Vector3.zero, quaternion.identity);
                    
                }
              
            }
        }
    }
    
    private IEnumerator SpawnShieldianEnemiesCoroutine()
    {
        while (true)
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

