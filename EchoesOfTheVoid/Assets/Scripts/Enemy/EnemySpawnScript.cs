using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnRate;
    private float time;
    [SerializeField] 
    private GameObject enemy;

    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnRate)
        {
            time = 0f;

            Instantiate(enemy);

        }
    }
}
