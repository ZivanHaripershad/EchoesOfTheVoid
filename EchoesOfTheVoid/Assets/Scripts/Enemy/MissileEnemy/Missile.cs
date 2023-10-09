using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;

    private EnemySpeedControl enemySpeedControl;
    
    [SerializeField] private float rotationSpeed;

    private float flightTime;
    [SerializeField] private float flightTimeSpeedEffect;

    private void Start()
    {
        enemySpeedControl = GameObject.FindWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
        flightTime = 0; 
    }

    private void FixedUpdate()
    {
        flightTime += Time.deltaTime;
        Vector2 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
        var step =  enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step + (flightTime * flightTimeSpeedEffect));
    }
}
