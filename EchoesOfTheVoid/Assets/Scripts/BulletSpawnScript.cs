using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnScript : MonoBehaviour
{

    [SerializeField] 
    private GameObject bullet;
    public float maxShootSpeed;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed > maxShootSpeed) 
            if (Input.GetMouseButtonDown(0))
            {
                timePassed = 0; 
                Instantiate(bullet, transform.position, transform.rotation);
            }
        timePassed += Time.deltaTime;
    }
}
