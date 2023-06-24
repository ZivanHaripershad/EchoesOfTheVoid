using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField]
    public GameObject pathFollowEnemy;

    [SerializeField]
    public GameObject splitterEnemy;

    private float timer;

    [SerializeField]
    public float spawnRate;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > spawnRate){
            timer = 0;

            //role a dice to see which enemy to instatiate...
            int enemyType = 2; //TODO: radomize

            if (enemyType == 1)
            {
                float topBottom = UnityEngine.Random.Range(-8.6f, 8.6f); 
                float leftRight = UnityEngine.Random.Range(-5.3f, 5.3f);

                //choose a random position offscreen

                Vector3 pos; 

                if (UnityEngine.Random.Range(0, 1) >= 5.0f) //top or bottom
                {
                    if (UnityEngine.Random.Range(0, 1) >= 5.0f) //top
                        pos = new Vector3(leftRight, 5.3f, 0);
                    else //bottom
                        pos = new Vector3(leftRight, -5.3f, 0);
                }
                else //left or right
                {
                    if (UnityEngine.Random.Range(0, 1) >= 5.0f) //left
                        pos = new Vector3(-8.6f, topBottom, 0);
                    else //right
                        pos = new Vector3(8.6f, topBottom, 0);
                }

                Instantiate(splitterEnemy, pos, transform.rotation);

            }

            if (enemyType == 2)
            {
                Instantiate(pathFollowEnemy, transform.position, transform.rotation);
            }
            
        }

        

        
    }
}
