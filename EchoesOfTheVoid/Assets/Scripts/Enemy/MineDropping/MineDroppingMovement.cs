using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineDroppingMovement : MonoBehaviour
{
    private Vector3[] waypoints;
    [SerializeField] private GameObject[] waypointObjects;
    private Rigidbody2D rb;
    private int lastWaypoint;
    private bool isFlyingToWaypoint;
    private int currentWaypoint;
    [SerializeField] private float thresholdMagnitude;
    [SerializeField] private float driftForce;
    [SerializeField] private float velReductionFactor;
    [SerializeField] private float avoidFoce;
    [SerializeField] private double centerAvoidRadius;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastWaypoint = 0;
        currentWaypoint = 0; 
        isFlyingToWaypoint = true;

        waypoints = new Vector3[waypointObjects.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = waypointObjects[i].transform.position;
        }
    }

    int GetNewWaypoint()
    {
        int waypoint = Random.Range(0, waypoints.Length);

        while (waypoint == lastWaypoint)
        {
            waypoint =  Random.Range(0, waypoints.Length);
        }

        lastWaypoint = waypoint;
        return waypoint;
    }

    private void Update()
    {
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

            if ((Vector3.zero - transform.position).magnitude < centerAvoidRadius)
            {
                rb.AddForce((transform.position - Vector3.zero));
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
        }

    }

    private Vector3 GetDirection(Vector3 point)
    {
        return (point - transform.position).normalized;
    }
}
