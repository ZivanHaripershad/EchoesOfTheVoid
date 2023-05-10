using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollection : MonoBehaviour
{
    public bool collectionMode;
    // Start is called before the first frame update
    public float speed = 5.0f; // the speed of object movement

    void Start()
    {
        //set the original position of the spaceship
        collectionMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            collectionMode = !collectionMode;
        }

        if(collectionMode == true){
            float horizontal = Input.GetAxis("Horizontal"); // get the horizontal input
            float vertical = Input.GetAxis("Vertical"); // get the vertical input

            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg; // get the current angle

            // apply the input to the object's position
            transform.position = transform.position + new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // rotate the object to face the current angle
        }
        
    }
}
