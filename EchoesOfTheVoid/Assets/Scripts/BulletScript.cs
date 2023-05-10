using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletScript : MonoBehaviour
{

    public float speed;
    public float innacuracy;
    private float jitter;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        jitter = Random.Range(-innacuracy, innacuracy);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jitter > 0)  
            jitter += 0.001f;
        else
            jitter -= 0.001f;


      

        /*
        transform.Translate((Vector3.up * jitter)  * Time.deltaTime);
        transform.Translate(Vector3.right * speed * Time.deltaTime);*/
    }
}
