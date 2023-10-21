using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollection : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private SpaceshipMode spaceshipMode;
    [SerializeField] private OrbDepositingMode orbDepositingMode;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite shootingSprite;

    //trail renderer
    [SerializeField]
    private SpriteRenderer fireSpriteA;
    [SerializeField]
    private SpriteRenderer fireSpriteB;
    [SerializeField]
    private TrailRenderer trailRendererRight;
    [SerializeField]
    private TrailRenderer trailRendererLeft;

    [SerializeField]
    Animator animator;

    //to eject the spaceship when you press space
    [SerializeField] private float ejectForce;
    [SerializeField] private Rigidbody2D rb;

    //to restrict the space ship to the screen
    private Camera mainCamera;
    public bool isEjecting;
    [SerializeField] private float stunWaitTime;
    private bool isStunned;

    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private float stunRotationMagnitude;
    [SerializeField] private float stunFrequency;

    void Start()
    {
        //set the original position of the spaceship
        spaceshipMode.collectionMode = false;
        spriteRenderer.sprite = shootingSprite;
        transform.position = new Vector3(0f, 0f, 0f);
        mainCamera = Camera.main;
        isEjecting = false;
        gameManagerData.timeSpentFlying = 0f;
        
       if(gameManagerData.level.Equals(GameManagerData.Level.Level3))
       {
           if (SelectedUpgradeLevel1.Instance != null &&
               SelectedUpgradeLevel1.Instance.GetUpgrade() != null &&
               SelectedUpgradeLevel1.Instance.GetUpgrade().GetName() == "ShipVelocityUpgrade")
           {
               moveSpeed = 7;
           }
       }

       if(gameManagerData.level.Equals(GameManagerData.Level.Level3))
        {
            if (SelectedUpgradeLevel3.Instance != null && SelectedUpgradeLevel3.Instance.GetUpgrade() != null &&
                SelectedUpgradeLevel3.Instance.GetUpgrade().GetName() == "ReduceStunUpgrade")
            {
                var reducedStunPercentage = SelectedUpgradeLevel3.Instance.GetUpgrade().GetValue();
                stunWaitTime -= (stunWaitTime * reducedStunPercentage);
            }
        }
        
    }

    private void SetEjectingToFalse()
    {
        isEjecting = false;
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isStunned)
            return;
        
        spaceshipMode.currentPosition = transform.position;

        if(orbDepositingMode.depositingMode == false){

            //set the trail renderer opacity to 1 while flying
            trailRendererLeft.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
            trailRendererRight.material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!spaceshipMode.collectionMode && !isEjecting)
                {
                    isEjecting = true;
                    
                    // Calculate the force direction based on the spaceship's current rotation
                    Vector2 ejectDirection = transform.right; // Use transform.up for 2D space

                    // Apply the ejectForce to the spaceship's Rigidbody2D
                    rb.AddForce(ejectDirection * ejectForce, ForceMode2D.Impulse);
                    
                    Invoke("SetEjectingToFalse", 0.1f);
                }

                spaceshipMode.collectionMode = !spaceshipMode.collectionMode;
                
                animator.SetBool("isCollectionMode", spaceshipMode.collectionMode);
                animator.SetBool("isOrbitingMode", !spaceshipMode.collectionMode);
                
                if(spaceshipMode.collectionMode){
                    // spriteRenderer.sprite = collectionSprite;
                    spaceshipMode.canRotateAroundPlanet = false;
                }
                else{
                    // spriteRenderer.sprite = shootingSprite;
                    spaceshipMode.returningToPlanet = true;
                }
            }


            if (spaceshipMode.collectionMode == true && orbDepositingMode.depositingMode == false && !isEjecting)
            {
                //boundary for top of screen
                float padding = 0.5f;
                Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
                Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

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
                float difference = (newPosition - transform.position).magnitude * 20;
                fireSpriteA.material.color = new Color(1f, 1f, 1f, 1 - difference);
                fireSpriteB.material.color = new Color(1f, 1f, 1f, 1 - difference);

                transform.position = newPosition;

                if (!AchievementsManager.Instance.GetCollectorCompletionStatus() && !gameManagerData.level.Equals(GameManagerData.Level.Tutorial))
                {
                    gameManagerData.timeSpentFlying += Time.deltaTime;
                }

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

    public void Stun()
    {
        StartCoroutine(StunPlayerCoroutine());
    }

    private IEnumerator StunPlayerCoroutine()
    {
        isStunned = true;
        float currentTime = stunWaitTime;
        while (currentTime > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, (float) Math.Sin(currentTime * stunFrequency) * stunRotationMagnitude);
            
            currentTime -= Time.deltaTime;
            yield return null;
        }
        isStunned = false;
    }
}
