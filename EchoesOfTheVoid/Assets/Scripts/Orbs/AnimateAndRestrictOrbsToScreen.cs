using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndRestrictOrbsToScreen : MonoBehaviour
{

    const float MAX_Y = 4.45f;
    const float MAX_X = 8.4f;

    [SerializeField]
    private float floatSpeed; 

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > MAX_X)
            rb.AddForce(new Vector3(-floatSpeed, 0, 0));
        if (transform.position.x < -MAX_X)
            rb.AddForce(new Vector3(floatSpeed, 0, 0));

        if (transform.position.y > MAX_Y)
            rb.AddForce(new Vector3(0, -floatSpeed, 0));
        if (transform.position.y < -MAX_Y)
            rb.AddForce(new Vector3(0, floatSpeed, 0));
    }
}
