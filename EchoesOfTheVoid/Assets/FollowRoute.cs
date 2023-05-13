using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes; //all the created routes

    private int routeToGoTo; //the current route to follow

    private float tParam;

    public float enemySpeed;

    private Vector2 enemyPosition;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        //randomize later
        routeToGoTo = 0;
        tParam = 0f;
        coroutineAllowed = true;
        enemySpeed = 0.5f;
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
        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

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

            //only render 1 per frame 
            yield return new WaitForEndOfFrame();
        }

        

        routeToGoTo += 1;

        //change to random that's not = current
        if (routeToGoTo > routes.Length - 1)
            routeToGoTo = 0;

        //after routine is over
        coroutineAllowed = true;

    }

}
