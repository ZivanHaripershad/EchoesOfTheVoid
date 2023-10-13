using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OrbStealingEnemyMovement : MonoBehaviour
{
    private List<Vector3> waypoints;
    [SerializeField] private float moveSpeed;
    private bool isMovingTowardsOrb;
    [SerializeField] private float collectionDistance;
    [SerializeField] private float detectionDistance;
    private int currentWaypoint;
    private GameObject nextOrb;
    [SerializeField] private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isMovingTowardsOrb = false;
        currentWaypoint = 0;
        GameObject[] waypointsObjects = GameObject.FindGameObjectsWithTag("OrbStealingWaypoint");

        waypoints = new List<Vector3>();
        
        foreach (var obj in waypointsObjects)
        {
            waypoints.Add(obj.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isMovingTowardsOrb)
        {
            nextOrb = GetNearestOrb();
        
            if (nextOrb != null)
                isMovingTowardsOrb = true;
               
        }
        
        if (isMovingTowardsOrb)
        {
            if (nextOrb == null)
            {
                isMovingTowardsOrb = false;
                return;
            }
        
            
            
            Vector3 orbPos = new Vector3(nextOrb.transform.position.x, nextOrb.transform.position.y, 0);
            MoveToPoint(orbPos);
            
            if (Vector3.Distance(transform.position, nextOrb.transform.position) < collectionDistance)
            {
                Destroy(nextOrb);
                isMovingTowardsOrb = false;
                return;
            }
        
            return;
        }
        
        Vector2 nextWaypoint = waypoints[currentWaypoint];
        
        MoveToPoint(nextWaypoint);
        
        if (Vector2.Distance(transform.position, nextWaypoint) < collectionDistance)
        {
            GetNextWaypoints();
        }


    }

    private void MoveToPoint(Vector2 nextWaypoint)
    {
        transform.position = Vector2.MoveTowards(transform.position, nextWaypoint, moveSpeed * Time.deltaTime);
        Vector2 direction = nextWaypoint - (Vector2)transform.position;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90),
                rotationSpeed * Time.deltaTime);
        }
    }

    private void GetNextWaypoints()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            currentWaypoint -= 1;
            if (currentWaypoint < 0)
                currentWaypoint = waypoints.Count - 1;
            return;
        }

        currentWaypoint += 1;
        if (currentWaypoint > waypoints.Count - 1)
            currentWaypoint = 0;
    }

    private GameObject GetNearestOrb()
    {
        GameObject[] allOrbs = GameObject.FindGameObjectsWithTag("Orb");
        GameObject minDistOrb;
        
        if (allOrbs.Length > 0)
        {
            float minDist = Vector3.Distance(allOrbs[0].transform.position, transform.position);
            minDistOrb = allOrbs[0];

            foreach (var orb in allOrbs)
            {
                float currDist = Vector3.Distance(orb.transform.position, transform.position);
                if (currDist < minDist)
                {
                    minDist = currDist;
                    minDistOrb = orb;
                }
            }
            
            if (minDist < detectionDistance)
                return minDistOrb;
        }

        return null;
    }
}