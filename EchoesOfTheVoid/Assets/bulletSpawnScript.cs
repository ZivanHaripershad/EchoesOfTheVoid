using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
