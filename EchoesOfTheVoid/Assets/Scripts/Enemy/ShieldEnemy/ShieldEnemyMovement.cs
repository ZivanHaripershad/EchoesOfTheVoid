using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;

    private EnemySpeedControl enemySpeedControl;
    private void Start()
    {
        enemySpeedControl = GameObject.FindWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = rotation;
        
        var step =  enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }
}