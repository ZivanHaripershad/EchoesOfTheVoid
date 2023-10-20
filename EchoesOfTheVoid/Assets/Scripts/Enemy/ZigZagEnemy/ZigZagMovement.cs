using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    [SerializeField] private float angrySpeed = 100f;
    
    private float speedMultiplier; 
    private EnemySpeedControl enemySpeedControl;
    [SerializeField] private Animator animator;
    
    [SerializeField] private float zigzagFrequency; // How quickly the object changes direction (zigzags per second)
    [SerializeField] private float zigzagAmplitude; // How far the object moves to each side during a zigzag
    private float currentZigzagOffset;
    [SerializeField] private float speedIncreaseFactor = 1.05f;
    private bool isAngry;
    [SerializeField] private float initialSpeed = 0.2f;
    [SerializeField] private float zigzagAmplitudeReductionFactor = 0.97f;
    [SerializeField] private float zigzagFrequencyIncreseFactor = 0.05f;
    [SerializeField] private float diveBombDistance = 3f;

    private void Start()
    {
        enemySpeedControl = GameObject.FindWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();
        speedMultiplier = initialSpeed;
        currentZigzagOffset = 0;
        isAngry = false;
    }

    private void FixedUpdate()
    {
        
        if (Vector3.Distance(transform.position, Vector3.zero) <= diveBombDistance)
        {
            Vector2 dir = targetPosition - transform.position;
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0f, 0f, ang - 90);
            transform.rotation = rot;
            
            var s =  enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime * speedMultiplier; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, s);
            
            return;
        }
        
        // Calculate zigzag offset based on time and frequency
        currentZigzagOffset = Mathf.Sin(Time.time * 2 * Mathf.PI * zigzagFrequency) * zigzagAmplitude;
        zigzagAmplitude *= zigzagAmplitudeReductionFactor;
        zigzagFrequency *= zigzagFrequencyIncreseFactor;

        Vector3 direction = (targetPosition - transform.position).normalized;
        Vector3 zigzagOffset = new Vector3(direction.y, -direction.x, 0) * currentZigzagOffset;

        // Calculate the new target position with zigzag offset
        Vector3 newTargetPosition = targetPosition + zigzagOffset;

        float angle = Mathf.Atan2(newTargetPosition.y - transform.position.y, newTargetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
        transform.rotation = rotation;

        speedMultiplier += speedIncreaseFactor * Time.deltaTime;
        
        var step = enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime * speedMultiplier; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, step);
        
        
    }

    // private void FixedUpdate()
    // {
    //     Vector2 direction = targetPosition - transform.position;
    //     float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //     Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
    //     transform.rotation = rotation;
    //     
    //     var step =  enemySpeedControl.GetShieldEnemySpeed() * Time.deltaTime * speedMultiplier; // calculate distance to move
    //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    // }
}