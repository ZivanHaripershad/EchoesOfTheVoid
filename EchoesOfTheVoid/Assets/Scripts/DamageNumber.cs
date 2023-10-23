using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DamageNumber : MonoBehaviour
{

    private Rigidbody2D rb;
    private readonly int maxForce = 2;
    private readonly int minForce = 1;
    private float forceDelay = 0.2f;
    private bool addedForce = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void AddForce()
    {
        Random random = new Random();
        int randomBearing = random.Next(360);
        int radomForceX = random.Next(maxForce) + minForce;
        int radomForceY = random.Next(maxForce) + minForce;
        int randomSpin = random.Next(maxForce) + minForce;

        rb.AddTorque(randomSpin, ForceMode2D.Impulse);
        rb.AddForce(new Vector3(radomForceX, radomForceY, 0), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        forceDelay -= Time.deltaTime;
        if (forceDelay <= 0 && !addedForce)
        {
            addedForce = true;
            AddForce();
        }
    }
}
