using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    [SerializeField]
    private GameObject bullet;


    [SerializeField]
    private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //spawn a bullet

        }
    }
}
