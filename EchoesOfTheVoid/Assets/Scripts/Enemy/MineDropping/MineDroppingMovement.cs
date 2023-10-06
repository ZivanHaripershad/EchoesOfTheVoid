using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineDroppingMovement : MonoBehaviour
{
    private Vector3[] waypoints;
    private GameObject[] waypointObjects;
    private Rigidbody2D rb;
    private int lastWaypoint;
    private bool isFlyingToWaypoint;
    private int currentWaypoint;
    [SerializeField] private float thresholdMagnitude;
    [SerializeField] private float driftForce;
    [SerializeField] private float velReductionFactor;
    [SerializeField] private double centerAvoidRadius;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationOffset;
    [SerializeField] private int waypointVariation;
    [SerializeField] private GameObject mine;
    [SerializeField] private float mineWaitDuration;

    private int randomNum;

    private float currentWaitDuration;
    [SerializeField] private float avoidForce;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastWaypoint = 0;
        currentWaypoint = 0; 
        isFlyingToWaypoint = true;
        randomNum = 0;

        waypointObjects = GameObject.FindGameObjectsWithTag("MineEnemyWayPoints");
        waypoints = new Vector3[waypointObjects.Length];
        currentWaitDuration = mineWaitDuration;

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform.position;
        }
    }

    int InBounds(int number)
    {
        if (number > waypoints.Length - 1)
            return number - (waypoints.Length - 1);

        if (number < 0)
            return number + waypoints.Length;

        return number;
    }

    int GetNewWaypoint()
    {
        int waypoint = InBounds(lastWaypoint + Random.Range(-waypointVariation, waypointVariation));

        while (waypoint == lastWaypoint)
        {
            waypoint = InBounds(lastWaypoint + Random.Range(-waypointVariation, waypointVariation));
        }

        lastWaypoint = waypoint;
        return waypoint;
    }

    public void UpdateWaypoint()
    {
        currentWaypoint = GetNewWaypoint();
    }

    //todo: add time dialation, enemy speed control
    private void Update()
    {
        currentWaitDuration -= Time.deltaTime;
        
        if (isFlyingToWaypoint)
        {
            Vector3 targetDirection = GetDirection(waypoints[currentWaypoint]);
            Vector3 vel = new Vector3(
                (targetDirection.normalized).x,
                (targetDirection.normalized).y,
                (targetDirection.normalized).z);

            // rb.velocity = vel;
            rb.AddForce(vel * driftForce* Time.deltaTime, ForceMode2D.Impulse) ;
            if (rb.velocity.magnitude > thresholdMagnitude)
                rb.velocity = new Vector2(rb.velocity.x * velReductionFactor, rb.velocity.y * velReductionFactor);

            float distanceFromCenter = (Vector3.zero - transform.position).magnitude;

            if (distanceFromCenter < centerAvoidRadius)
            {
                float relativeAvoidForce = avoidForce / (distanceFromCenter * distanceFromCenter);
                
                Debug.Log(relativeAvoidForce);
                
                if (randomNum == 0)
                {
                    rb.AddForce((transform.position - Vector3.right) * relativeAvoidForce);
                }
                else
                {
                    rb.AddForce((transform.position - Vector3.left) * relativeAvoidForce);
                }
                
                rb.AddForce((transform.position - Vector3.zero) * relativeAvoidForce);
            }

            float targetAngle = Mathf.Atan2(vel.y, vel.x) + rotationOffset;

            // Calculate the current angle in radians
            float currentAngle = Mathf.Atan2(transform.right.y, transform.right.x);

            // Interpolate between the current rotation and the target rotation
            float angle = Mathf.LerpAngle(currentAngle * Mathf.Rad2Deg, targetAngle * Mathf.Rad2Deg, rotationSpeed * Time.deltaTime);

            // Convert the angle back to radians and create a quaternion rotation
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // Apply the rotation smoothly over times
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed);

            if ((waypoints[currentWaypoint] - transform.position).magnitude < thresholdMagnitude)
                isFlyingToWaypoint = false;
        }
        else
        {
            currentWaypoint = GetNewWaypoint();
            isFlyingToWaypoint = true;
            if (currentWaitDuration < 0)
            {
                randomNum = Random.Range(0, 1);
                Instantiate(mine, transform.position, new Quaternion(0, 0, 0, 0));
                currentWaitDuration = mineWaitDuration;
            }
        }

    }

    private Vector3 GetDirection(Vector3 point)
    {
        return (point - transform.position).normalized;
    }
}
