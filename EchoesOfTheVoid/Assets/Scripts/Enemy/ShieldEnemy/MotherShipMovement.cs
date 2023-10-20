using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MotherShipMovement : MonoBehaviour
{
    public float xRadius;
    public float yRadius;

    private float timer;
    private bool isMovingForward;

    private bool isCoolingDown;

    private SpriteRenderer sp;

    private bool isShaking;

    private float currentShake;

    [SerializeField] private float coolDowntimer;
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float nearEnemyThreshold;
    [SerializeField] private float flightPathRotationSpeed;
    [SerializeField] private float enterSpeedModifier;
    [SerializeField] private SpriteRenderer healthWindowSpriteRenderer;

    private EnemySpeedControl enemySpeedControl;
    private Vector3 lastPositionInOval; 
    private bool isReturning;
    private bool hasEnteredScene;
    private int flightEntrancePathNumber;

    private void Start()
    {
        isMovingForward = true;
        isCoolingDown = false;
        isShaking = false;
        currentShake = 0;
        sp = GetComponent<SpriteRenderer>();
        isReturning = false;
        hasEnteredScene = false;
        flightEntrancePathNumber = 0; 
        gameObject.transform.position = new Vector3(2.39f, -5.86f, 0f);
        enemySpeedControl = GameObject.FindWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
    }

    void MoveInOval()
    {
        if (isMovingForward)
        {
            timer += Time.deltaTime * enemySpeedControl.GetMotherShipOrbitSpeed();
            sp.flipX = false;
            sp.flipY = false;
            healthWindowSpriteRenderer.flipX = false;
            healthWindowSpriteRenderer.flipY = false;
        }
        else
        {
            timer -= Time.deltaTime * enemySpeedControl.GetMotherShipOrbitSpeed();
            sp.flipX = true;
            sp.flipY = true;
            
            //todo: position correctly
            healthWindowSpriteRenderer.flipX = true;
            healthWindowSpriteRenderer.flipY = true;
        }
    
        // Calculate the new position of the GameObject on the oval path
        float x = Mathf.Cos(timer) * xRadius;
        float y = Mathf.Sin(timer) * yRadius;
    
        Vector3 moveDirection = new Vector3(-yRadius * Mathf.Sin(timer), xRadius * Mathf.Cos(timer), 0f).normalized;
        transform.position = new Vector3(x, y, 0f);
    
        if (isShaking)
        {
            currentShake = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            //get current direction 
            transform.position += new Vector3(currentShake, currentShake, 0f);
        }
        else
        {
            currentShake = 0; 
        }
   
        // Rotate to face the direction of movement in the z-axis
         float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
         Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, flightPathRotationSpeed * Time.deltaTime);
    }
    
    private bool EnterScene(float yThreshold, Vector3 direction)
    {
        transform.position += enemySpeedControl.GetMotherShipMoveSpeed() * Time.deltaTime * direction * enterSpeedModifier;
        
        if (gameObject.transform.position.y > yThreshold)
            return true;
        
        return false;
    }

    private void Update()
    {
        if (!hasEnteredScene)
            switch (flightEntrancePathNumber)
            {
                case 0:
                    //move over the bottom right corner
                    if (EnterScene(1.62f, new Vector3(1f, 1f, 0f)))
                    {
                        //set position to just left of screen
                        gameObject.transform.position = new Vector3(-9.7f, 0.71f, 0f);
                        flightEntrancePathNumber++;
                    }
                    break;
                case 1:
                    //move over the top left corner
                    if (EnterScene(5.65f, new Vector3(1f, 1f, 0f)))
                    {
                        //set position
                        gameObject.transform.position = new Vector3(5.19f, -6.24f, 0f);
                        flightEntrancePathNumber++;
                        gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                    }
                    break;
                case 2: 
                    //move to starting position of rotation
                    if (EnterScene(0, new Vector3(0f, 1f, 0f)))
                    {
                        hasEnteredScene = true;
                    }
                    break;
            }
        else
            HelpEnemies();
        
    }

    private void HelpEnemies()
    {
        //Find the nearest enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject toFlyTo = null;

        float minDistance = 1000f;

        //get nearest enemy
        foreach (var enemy in enemies)
        {
            ActivateShield activateShield = enemy.GetComponentInChildren<ActivateShield>();

            if (activateShield != null)
            {
                float tryMe = Vector3.Distance(enemy.transform.position, gameObject.transform.position);
                if (tryMe < minDistance && !activateShield.IsActive() && activateShield.CanBeActivated())
                {
                    minDistance = tryMe;
                    toFlyTo = enemy;
                }
            }
        }

        if (toFlyTo != null)
        {
            MoveToNearestEnemy(toFlyTo.transform.position, true);
            isReturning = true;
        }
        else
        {
            if (isReturning)
            {
                MoveToNearestEnemy(lastPositionInOval, false);
                if ((transform.position - lastPositionInOval).magnitude < nearEnemyThreshold)
                    isReturning = false;
            }
            else
            {
                lastPositionInOval = transform.position;
                MoveInOval();
            }
        }
    }

    private void MoveToNearestEnemy(Vector3 targetPosition, bool isGoingTo)
    {
        // Calculate the new position to move towards.
        Vector3 lookDirection = Vector3.left - transform.position;

        if (lookDirection != Vector3.left)
        {
            // Calculate the angle (in degrees) between the lookDirection and the forward direction of the sprite.
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            // //todo: modify
            //  if (isGoingTo)
            //      angle -= 90;
            //  else angle += 90;

             angle += 90;

            // Rotate the sprite around the Z-axis to face the target position. 
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Interpolate between the current position and the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, 
            enemySpeedControl.GetMotherShipMoveSpeed() * Time.deltaTime * 
            5/(transform.position - targetPosition).magnitude);
    }

    IEnumerator Shake()
    {
        isShaking = true;
        yield return new WaitForSeconds(shakeTime);
        isShaking = false;
    }

    public void StartShaking()
    {
        StartCoroutine(Shake());
    }

    IEnumerator CoolDown()
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(coolDowntimer);
        isCoolingDown = false;
    }

    public void DodgeBackwards()
    {
        if (!isCoolingDown)
        {
            isMovingForward = false;
            StartCoroutine(CoolDown());
        }
    }

    public void DodgeForwards()
    {
        if (!isCoolingDown)
        {
            isMovingForward = true;
            StartCoroutine(CoolDown());
        }
    }
}