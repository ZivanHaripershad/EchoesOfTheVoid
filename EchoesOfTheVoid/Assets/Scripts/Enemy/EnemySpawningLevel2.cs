using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawningLevel2 : MonoBehaviour {
    
    [SerializeField] private float spawnRate;
    [SerializeField] private int minEnemiesToSpawn;
    [SerializeField] private int maxEnemiesToSpawn;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnTimerVariation;
    [SerializeField] private GameObject pathFollowingEnemy;
    [SerializeField] private GameObject shieldEnemy;
    private Coroutine currentCoroutine;

    private bool hasStarted;

    private int numberSpawned;
    private int maxSpawned;

    [SerializeField] private Transform[] shieldEnemySpawnPoints; 

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
            currentCoroutine = StartCoroutine(SpawnLevelTwoEnemiesCoroutine());
        }
    }

    private IEnumerator SpawnLevelTwoEnemiesCoroutine()
    {
        
        Debug.Log("Spawning l2 enemies");
        yield return null;

    }
    
    public void StopSpawning()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
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
                    float randomNumber = Random.Range(0f, spawnTimerVariation);
                    yield return new WaitForSeconds(spawnInterval + randomNumber);
                    Instantiate(pathFollowingEnemy, Vector3.zero, quaternion.identity);
                }
            }
            else
            {
                float randomNumber = Random.Range(0f, spawnTimerVariation);
                yield return new WaitForSeconds(spawnInterval + randomNumber);
                
                //get a random spawn point
                int point = Random.Range(0, shieldEnemySpawnPoints.Length - 1);
                Instantiate(shieldEnemy, new Vector3(
                        shieldEnemySpawnPoints[point].transform.position.x,
                        shieldEnemySpawnPoints[point].transform.position.y, 0),
                    quaternion.identity);
            }
            
            
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
                Instantiate(pathFollowingEnemy, Vector3.zero, quaternion.identity);
                numberSpawned++;
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

