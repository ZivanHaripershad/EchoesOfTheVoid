using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemySpawningLevel3 : EnemySpawning {
    
    [SerializeField] private GameObject pathFollowingEnemy;
    [SerializeField] private GameObject shieldEnemy;
    [SerializeField] private GameObject zigZagEnemy;
    [SerializeField] private Transform[] shieldEnemySpawnPoints; 
    
    private int currentZigZagSpawnPoint = 0;
    
    private Vector3[] zigzagEnemySpawnPoints =
    {
        new Vector3(-9.3f, 3.57f, 0), 
        new Vector3(9.3f, 0, 0)
    };

    [SerializeField] private GameObject minionEnemy;

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
            int enemiesToSpawn = Random.Range(minEnemiesToSpawn, maxEnemiesToSpawn + 1);
        
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int result = Random.Range(0, 12);

                if (result == 1 || result == 2 || result == 3)
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
                else if(result == 4)
                {
                    float randomNumber = Random.Range(0f, gameManagerData.spawnTimerVariation);
                    yield return new WaitForSeconds(gameManagerData.spawnInterval + randomNumber);
                    Instantiate(zigZagEnemy, zigzagEnemySpawnPoints[currentZigZagSpawnPoint++], Quaternion.identity);
                    if (currentZigZagSpawnPoint > zigzagEnemySpawnPoints.Length - 1)
                        currentZigZagSpawnPoint = 0;
                }
                else if (result == 5 || result == 6)
                {
                    //Minion enemy
                    int corner = Random.Range(0, 4);

                    switch (corner)
                    {
                        case 0: //top
                            Instantiate(minionEnemy, new Vector3(0, 5.87f, 0), Quaternion.identity);
                            break;
                        case 1: //left
                            Instantiate(minionEnemy, new Vector3(-10.78f, 0, 0), Quaternion.identity);
                            break;
                        case 2: //bottom
                            Instantiate(minionEnemy, new Vector3(0, -5.87f, 0), Quaternion.identity);
                            break;
                        case 3: //right
                            Instantiate(minionEnemy, new Vector3(10.78f, 0, 0), Quaternion.identity);
                            break;
                    }
                    
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
}

