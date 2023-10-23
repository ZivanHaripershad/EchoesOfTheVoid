using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DamageNumber : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    private readonly int maxForce = 2;
    private readonly int minForce = 1;
    private float forceDelay = 0.2f;
    private bool addedForce = false;


    private void AddForce()
    {
        
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

    public void AddBulletForce(Quaternion transformRotation)
    {
        //0 to 90 = top right 
        //90 to 180 = top left
        //180 to 270 = bottom left
        //270 to 360 = bottom right
        
        Random random = new Random();
        int radomForceX = random.Next(maxForce) + minForce;
        int radomForceY = random.Next(maxForce) + minForce;
        int randomSpin = random.Next(maxForce) + minForce;

        rb.AddTorque(randomSpin, ForceMode2D.Impulse);

        float angle = transformRotation.eulerAngles.z;
        
        float magnitudeX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float magnitudeY = Mathf.Sin(angle * Mathf.Deg2Rad);

        rb.AddForce(new Vector3(radomForceX * magnitudeX, radomForceY * magnitudeY, 0), ForceMode2D.Impulse);
        

    }
}
