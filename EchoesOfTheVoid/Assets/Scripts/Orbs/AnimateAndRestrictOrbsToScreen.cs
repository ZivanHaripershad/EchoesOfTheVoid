using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndRestrictOrbsToScreen : MonoBehaviour
{

    float MAX_Y = 4.45f;
    float MAX_X = 8.4f;

    [SerializeField]
    private float floatSpeed;

    [SerializeField]
    private float driftTimeInOneDirection;
    private float currentDriftTime;

    [SerializeField]
    private float randomForceMagnitude;

    private Rigidbody2D rb;

    private float randomYForce;
    private float randomXForce;

    private float spawnX;
    private float spawnY;
    
    [SerializeField] private float bounds;
    [SerializeField] private float orbMagnetForce;
    [SerializeField] private float orbMagnetRadius;

    public bool mustAttract;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        randomXForce = Random.Range(-1f, 1f);
        randomYForce = Random.Range(-1f, 1f);
        currentDriftTime = 0;

        spawnX = gameObject.transform.position.x;
        spawnY = gameObject.transform.position.y;

        player = GameObject.FindGameObjectWithTag("Player");
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

        //restrict orbs to area of instantiation
        if (transform.position.x > spawnX + bounds)
            rb.AddForce(new Vector3(-floatSpeed, 0, 0));
        if (transform.position.x < spawnX - bounds)
            rb.AddForce(new Vector3(floatSpeed, 0, 0));
        if (transform.position.y > spawnY + bounds)
            rb.AddForce(new Vector3(0, -floatSpeed, 0));
        if (transform.position.y < spawnY - bounds)
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
            randomXForce = Random.Range(-randomForceMagnitude, randomForceMagnitude);
            randomYForce = Random.Range(-randomForceMagnitude, randomForceMagnitude);
            currentDriftTime = 0; 
        }
        
        //add attraction force to player
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance < orbMagnetRadius && mustAttract)
        {
            Vector3 attraction = direction.normalized * orbMagnetForce;
            rb.AddForce(attraction);
        }
    }
}
