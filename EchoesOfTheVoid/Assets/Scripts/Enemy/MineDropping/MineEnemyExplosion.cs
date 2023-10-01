using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEnemyExplosion : MonoBehaviour
{

    [SerializeField] private float delay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
            Destroy(gameObject);
    }
}
