using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes; //all the created routes

    [SerializeField]
    private float rotationSpeed;

    private float tParam;

    [SerializeField]
    public float enemySpeed;

    private GlobalVariables variables;

    [SerializeField]
    private GameObject enemy;

    [SerializeField] private float radius;

    private SpriteRenderer sp;

    private Vector2 enemyPosition;

    private bool coroutineAllowed;

    private Camera mainCamera;

    private Vector3 worldCenterPosition;

    private bool firstUpdate;
    // Start is called before the first frame update
    void Start()
    {       
        tParam = 0f;
        coroutineAllowed = true;

        sp = enemy.GetComponent<SpriteRenderer>();
        sp.material.color = new Color(1f, 1f, 1f, 0);

        variables = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVariables>();

        firstUpdate = true;
        mainCamera = Camera.main;

        worldCenterPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            Vector3 resetPosition = new Vector3 (0, 0, 0);
            gameObject.transform.position = resetPosition;

            StartCoroutine(GoByRoute());
        }
    }

    private IEnumerator GoByRoute()
    {
        int prevPrev = variables.prevPrevEnemySpawned;
        int prev = variables.prevEnemySpawned;

        int routeToGoTo = Random.Range(0, routes.Length);

        while (routeToGoTo == prev || routeToGoTo == prevPrev)
            routeToGoTo = Random.Range(0, routes.Length);

        //set prev and prevprev
        variables.prevPrevEnemySpawned = prev;
        variables.prevEnemySpawned = routeToGoTo;

        //don't start new follow until this one is over
        coroutineAllowed = false;

        //store the positions of the control points
        Vector3 p0 = routes[routeToGoTo].GetChild(0).position;
        Vector3 p1 = routes[routeToGoTo].GetChild(1).position;
        Vector3 p2 = routes[routeToGoTo].GetChild(2).position;
        Vector3 p3 = routes[routeToGoTo].GetChild(3).position;

        //reset to start point
        tParam = 0f;

        while (tParam < 1)
        {
            if (variables.mustPause && Vector3.Distance(transform.position, worldCenterPosition) < radius && !firstUpdate)
            {
                yield return null;
                continue;
            }
            
            tParam += Time.deltaTime * enemySpeed;
            
            //calculate the position
            enemyPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = enemyPosition;
            
            firstUpdate = false;

            sp.material.color = new Color(1f, 1f, 1f, 1f);

            Vector3 toTarget = p3 - transform.position;
            float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            //only render 1 per frame 
            yield return new WaitForEndOfFrame();
        }

        //after routine is over
        coroutineAllowed = true;

    }

}
