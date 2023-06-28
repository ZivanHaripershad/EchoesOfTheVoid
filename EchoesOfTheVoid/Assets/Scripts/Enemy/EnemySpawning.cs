using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    public GameObject enemy;

    private float timer;
    private float randomNum;

    [SerializeField]
    private float spawnTimerVariation; 

    [SerializeField]
    public float spawnRate;

    private bool isSpawning; 

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        randomNum = Random.Range(0f, spawnTimerVariation);
        isSpawning = false;
    }

    void spawnEnemies(int numEnemies)
    {
        isSpawning = true;



        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate + randomNum){
            randomNum = Random.Range(0f, 7f);
            timer = 0;

            int enemiesToSpawn = Random.Range(1, 3);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Instantiate(enemy, transform.position, transform.rotation);

            }

        }
    }
}
