using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotherShipMovement : MonoBehaviour
{
    public float speed;
    public float xRadius;
    public float yRadius;
    public float rotationSpeed;
    public float moveSpeed;

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

    private Vector3 lastPositionInOval; 
    
    private bool isReturning;

    private float returnTimer; 
    
    

    private void Start()
    {
        isMovingForward = true;
        isCoolingDown = false;
        isShaking = false;
        currentShake = 0;
        sp = GetComponent<SpriteRenderer>();
        isReturning = false;
    }

    void MoveInOval()
    {
        
        if (isMovingForward)
        {
            timer += Time.deltaTime * speed;
            sp.flipX = false;
            sp.flipY = false;
        }
        else
        {
            timer -= Time.deltaTime * speed;
            sp.flipX = true;
            sp.flipY = true;
        }


        // Calculate the new position of the GameObject on the oval path
        float x = Mathf.Cos(timer) * xRadius;
        float y = Mathf.Sin(timer) * yRadius;
        
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

        // Calculate the direction the GameObject is moving
        Vector3 moveDirection = new Vector3(Mathf.Cos(timer), Mathf.Sin(timer), 0f);

        // If the GameObject is moving, rotate it to face the movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(Vector3.forward, moveDirection) * Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Update()
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
            MoveToNearestEnemy(toFlyTo.transform.position);
            returnTimer = 0;
            isReturning = true;
        }
        else 
        {
            if (isReturning)
            {
                MoveToNearestEnemy(lastPositionInOval);
                if ((transform.position - lastPositionInOval).magnitude < nearEnemyThreshold)
                    isReturning = false;
            }
            else
            {
                lastPositionInOval = transform.position;
                MoveInOval();
            }
        }

        if (isShaking)
        {
            currentShake = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            //get current direction 
            transform.position += new Vector3(currentShake, currentShake, 0f);
        }
    }

    private void MoveToNearestEnemy(Vector3 targetPosition)
    {
        // Calculate the new position to move towards.
        Vector3 lookDirection = Vector3.left - transform.position;

        if (lookDirection != Vector3.left)
        {
            // Calculate the angle (in degrees) between the lookDirection and the forward direction of the sprite.
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg + 90;

            // Rotate the sprite around the Z-axis to face the target position. 
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        // Interpolate between the current position and the target position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
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