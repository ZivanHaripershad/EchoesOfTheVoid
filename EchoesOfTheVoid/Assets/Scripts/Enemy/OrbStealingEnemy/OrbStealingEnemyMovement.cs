using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbStealingEnemyMovement : MonoBehaviour
{
    [SerializeField] private List<Vector3> waypoints;
    [SerializeField] private float moveSpeed;
    private bool isMovingTowardsOrb;
    [SerializeField] private float thresholdDistance;
    [SerializeField] private float detectionDistance;
    private int currentWaypoint;
    private GameObject nextOrb;

    // Start is called before the first frame update
    void Start()
    {
        isMovingTowardsOrb = false;
        currentWaypoint = 0;
        GameObject[] waypointsObjects = GameObject.FindGameObjectsWithTag("OrbStealingWaypoint");
        
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
            
            Debug.Log("Move towards....");
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextOrb.transform.position.x, nextOrb.transform.position.y, 0), moveSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, nextOrb.transform.position) < thresholdDistance)
            {
                Destroy(nextOrb);
                isMovingTowardsOrb = false;
                return;
            }

            return;
        }
        
        
        //if no orb, move between waypoints;
        Vector3 nextWaypoint = waypoints[currentWaypoint];

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextWaypoint.x, nextWaypoint.y), moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextWaypoint) < thresholdDistance)
        {
            GetNextWaypoints();
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

            return minDistOrb;
        }

        return null;
    }
}