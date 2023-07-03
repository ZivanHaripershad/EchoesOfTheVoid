using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    public GameObject enemy;

    private float timer;

    [SerializeField]
    private float spawnTimerVariation; 

    [SerializeField]
    public float spawnRate;

    [SerializeField]
    private float spawnIntevalBetweenSuccessiveEnemies;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    void instantiate()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate){

            int enemiesToSpawn = Random.Range(1, 3);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Invoke("instantiate", (i * spawnIntevalBetweenSuccessiveEnemies) + Random.Range(0f, spawnTimerVariation));
            }

            timer = 0;
        }
    }
}
