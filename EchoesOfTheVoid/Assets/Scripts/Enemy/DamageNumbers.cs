using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumbers : MonoBehaviour
{

    [SerializeField] private GameObject number;

    [SerializeField] private float foceBound;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float torqueBound;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomForce = new Vector3(Random.Range(-foceBound, foceBound), Random.Range(-foceBound, foceBound),
            Random.Range(-foceBound, foceBound));
        float randomTorque = Random.Range(-torqueBound, torqueBound);
        
        rb.AddForce(randomForce);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {   
       
    }
}
