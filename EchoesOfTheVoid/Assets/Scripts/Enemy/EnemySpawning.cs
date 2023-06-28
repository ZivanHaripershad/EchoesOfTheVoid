using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        randomNum = Random.Range(0f, spawnTimerVariation);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate + randomNum){
            randomNum = Random.Range(0f, 7f);
            timer = 0;
            Instantiate(enemy, transform.position, transform.rotation);
        }
    }
}
