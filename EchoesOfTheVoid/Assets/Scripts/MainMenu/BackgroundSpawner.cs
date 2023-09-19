using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy1;
    [SerializeField]
    private Transform target;



    [SerializeField]
    private float enemyInterval = 3.5f;
    /*[SerializeField]
    private float bigEnemyInterval = 10f;*/

    // Start is called before the first frame update
   

    void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, Enemy1));
        //Spawner2.SetActive(false);

    }


    public IEnumerator spawnEnemy(float interval, GameObject enemy)
     {
         yield return new WaitForSeconds(interval);

         GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-12f, 12f), Random.Range(-5f, 4f), 0), Quaternion.identity);
         StartCoroutine(spawnEnemy(interval, enemy));
     }
}
