using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    public GameObject pathFollowEnemy;

    [SerializeField]
    public GameObject splittingEnemy;

    private float timer;

    [SerializeField]
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate){
            timer = 0;
            Instantiate(pathFollowEnemy, transform.position, transform.rotation);
        }
    }
}
