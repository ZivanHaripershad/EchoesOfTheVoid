using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndRestrictOrbsToScreen : MonoBehaviour
{

    const float MAX_Y = 4.45f;
    const float MAX_X = 8.4f;

    [SerializeField]
    private float floatSpeed;

    [SerializeField]
    private float driftTimeInOneDirection;
    private float currentDriftTime;

    private Rigidbody2D rb;

    private float randomYForce;
    private float randomXForce;


    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        randomXForce = Random.Range(-1f, 1f);
        randomYForce = Random.Range(-1f, 1f);
        currentDriftTime = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        //restrict orbs to screen
        if (transform.position.x > MAX_X)
            rb.AddForce(new Vector3(-floatSpeed, 0, 0));
        if (transform.position.x < -MAX_X)
            rb.AddForce(new Vector3(floatSpeed, 0, 0));

        if (transform.position.y > MAX_Y)
            rb.AddForce(new Vector3(0, -floatSpeed, 0));
        if (transform.position.y < -MAX_Y)
            rb.AddForce(new Vector3(0, floatSpeed, 0));


        //floating animation
        if (currentDriftTime < driftTimeInOneDirection)
        {
            //add the random force
            rb.AddForce(new Vector3(randomXForce, randomYForce, 0));
            currentDriftTime += Time.deltaTime;
        } 
        else
        {
            //create a different random force to float in another direction
            randomXForce = Random.Range(-1f, 1f);
            randomYForce = Random.Range(-1f, 1f);
            currentDriftTime = 0; 
        }
    }
}
