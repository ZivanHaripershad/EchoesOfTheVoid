using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes; //all the created routes

    private int routeToGoTo; //the current route to follow

    [SerializeField]
    private float rotationSpeed;

    private float tParam;

    [SerializeField]
    public float enemySpeed;

    [SerializeField]
    private GlobalVariables variables;

    private Vector2 enemyPosition;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGoTo = (int) Random.Range(0, routes.Length);
        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            Vector3 resetPosition = new Vector3 (0, 0, 0);
            gameObject.transform.position = resetPosition;

            Console.WriteLine("Starting core routin");
            StartCoroutine(GoByRoute(routeToGoTo));
        }
    }

    private IEnumerator GoByRoute(int routeNumber)
    {
        //don't start new follow until this one is over
        coroutineAllowed = false;

        //store the positions of the control points
        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        //reset to start point
        tParam = 0f;

        while (tParam < 1)
        {
            //smooth the movement
            tParam += Time.deltaTime * enemySpeed;

            //calculate the position
            enemyPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = enemyPosition;

            Vector3 toTarget = p3 - transform.position;
            float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            //only render 1 per frame 
            yield return new WaitForEndOfFrame();
        }

        int prevPrev = variables.getPrevPrevEnemySpawned();
        int prev = variables.getPrevEnemySpawned();

        routeToGoTo = (int) Random.Range(0, routes.Length);

        while (routeToGoTo == prev || routeToGoTo == prevPrev)
            routeToGoTo = (int) Random.Range(0, routes.Length);

        //set prev and prevprev
        variables.setPrevPrevEnemySpawned(variables.getPrevEnemySpawned());
        variables.setPrevEnemySpawned(routeToGoTo);

        //after routine is over
        coroutineAllowed = true;

    }

}
