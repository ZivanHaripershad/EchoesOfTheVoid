using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DirectEnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float angrySpeed;
    
    private float speedMultiplier; 
    private EnemySpeedControl enemySpeedControl;
    [SerializeField] private Animator animator;
    
    // How far the object moves to each side during a zigzag
    private float currentZigzagOffset;
    private bool isAngry;
    [SerializeField] private float initialSpeed;

    private void Start()
    {
        enemySpeedControl = GameObject.FindWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
        speedMultiplier = initialSpeed;
        currentZigzagOffset = 0;
        isAngry = false;
    }

    public void SpeedUp()
    {
        isAngry = true;
        speedMultiplier = angrySpeed;
        animator.SetTrigger("isAngry");
    }

    private void FixedUpdate()
    {
        Vector2 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.rotation = rotation;
        
        var step =  enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime * speedMultiplier; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}
