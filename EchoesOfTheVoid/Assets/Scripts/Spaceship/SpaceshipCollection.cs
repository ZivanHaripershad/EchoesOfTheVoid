using System;
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

    public SpriteRenderer spriteRenderer;
    public Sprite collectionSprite;
    public Sprite shootingSprite;

    [SerializeField]
    private SpriteRenderer fireSpriteA;

    [SerializeField]
    private SpriteRenderer fireSpriteB;

    [SerializeField]
    private TrailRenderer trailRendererRight;
    [SerializeField]
    private TrailRenderer trailRendererLeft;

    [SerializeField]
    private float trailFadeAmount;

    void Start()
    {
        //set the original position of the spaceship
        spaceshipMode.collectionMode = false;
        spriteRenderer.sprite = shootingSprite;
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        spaceshipMode.currentPosition = transform.position;

        if(orbDepositingMode.depositingMode == false){

            //set the trail renderer opacity to 1 while flying
            trailRendererLeft.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
            trailRendererRight.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                spaceshipMode.collectionMode = !spaceshipMode.collectionMode;
                if(spaceshipMode.collectionMode){
                    spriteRenderer.sprite = collectionSprite;
                    spaceshipMode.canRotateAroundPlanet = false;
                }
                else{
                    spriteRenderer.sprite = shootingSprite;
                    spaceshipMode.returningToPlanet = true;
                }
            }


            if (spaceshipMode.collectionMode == true && orbDepositingMode.depositingMode == false)
            {

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

                //lower the opacity of the idle animation depending on the speed
                float difference = (newPosition - transform.position).magnitude;
                //square the difference
                difference = difference * 20;
                Debug.Log(difference);
                fireSpriteA.material.color = new Color(1f, 1f, 1f, 1 - difference);
                fireSpriteB.material.color = new Color(1f, 1f, 1f, 1 - difference);

                transform.position = newPosition;

                if (movement.x + movement.y != 0)
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // rotate the object to face the current angle
            } 
            else
            {
                fireSpriteA.material.color = new Color(1f, 1f, 1f, 1);
                fireSpriteB.material.color = new Color(1f, 1f, 1f, 1);
            }
        }
    }
}
