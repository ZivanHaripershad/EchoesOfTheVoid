using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollection : MonoBehaviour
{
    public bool collectionMode;
    
    // the speed of object movement
    public float moveSpeed = 5;

    public SpaceshipMode spaceshipMode;

    public OrbDepositingMode orbDepositingMode;

    void Start()
    {
        //set the original position of the spaceship
        spaceshipMode.collectionMode = false;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceshipMode.collectionMode = !spaceshipMode.collectionMode;
        }
        

        if(spaceshipMode.collectionMode == true && orbDepositingMode.depositingMode == false){
            
            //boundary for top of screen
            float padding = 0.5f;
            Vector3 lowerLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 upperRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

            float minX = lowerLeft.x + padding;
            float maxX = upperRight.x - padding;
            float minY = lowerLeft.y + padding;
            float maxY = upperRight.y - padding;
            
            float horizontal = Input.GetAxis("Horizontal"); // get the horizontal input
            float vertical = Input.GetAxis("Vertical"); // get the vertical input
            Vector2 movement = new Vector2(horizontal, vertical);

            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg; // get the current angle

            Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // rotate the object to face the current angle
        }
        
    }
}
