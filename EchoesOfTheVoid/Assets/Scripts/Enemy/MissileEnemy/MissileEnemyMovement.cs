using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;
 
public class MissileEnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] routes; //all the created routes
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject missile;
    [SerializeField] private float dropTime;

    private bool dropped; 
    private float tParam;
    private GlobalVariables variables;
    private SpriteRenderer sp;
    private Vector2 enemyPosition;
    private bool coroutineAllowed;
    private float enemySpeed;
    private bool firstUpdate;
    private EnemySpeedControl enemySpeedControl;
    private BoxCollider2D collider;
    private Vector2 enemyPositionNext;

    // Start is called before the first frame update
    void Start()
    {       
        tParam = 0f;
        coroutineAllowed = true;
        firstUpdate = true;

        sp = enemy.GetComponent<SpriteRenderer>();
        sp.material.color = new Color(1f, 1f, 1f, 0);

        variables = GameObject.FindGameObjectWithTag("GlobalVars").GetComponent<GlobalVariables>();
        enemySpeedControl = GameObject.FindGameObjectWithTag("EnemySpeedControl").GetComponent<EnemySpeedControl>();

        collider = GetComponentInChildren<BoxCollider2D>();
        collider.enabled = false;
        
        dropped = false;
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
        
        enemySpeed = enemySpeedControl.GetPathFollowingEnemySpeed();
    }

    private IEnumerator GoByRoute()
    {
        int prevPrev = variables.prevPrevMissileEnemySpawned;
        int prev = variables.prevMissileEnemySpawned;

        int routeToGoTo = Random.Range(0, routes.Length);

        while (routeToGoTo == prev || routeToGoTo == prevPrev)
            routeToGoTo = Random.Range(0, routes.Length);

        //set prev and prevprev
        variables.prevPrevMissileEnemySpawned = prev;
        variables.prevMissileEnemySpawned = routeToGoTo;

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
         
            //calculate the position
            enemyPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                            3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                            3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                            Mathf.Pow(tParam, 3) * p3;
            
            tParam += Time.deltaTime * enemySpeed;

            collider.enabled = true;

            transform.position = enemyPosition;
            
            firstUpdate = false;

            sp.material.color = new Color(1f, 1f, 1f, 1f);
            
            enemyPositionNext = Mathf.Pow(1 - tParam, 3) * p0 +
                            3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                            3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                            Mathf.Pow(tParam, 3) * p3;

            Vector3 toTarget = enemyPositionNext - enemyPosition;
            float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            if (tParam > dropTime && !dropped)
            {
                dropped = true;
                Instantiate(missile, gameObject.transform.position, gameObject.transform.rotation);
            }

            //only render 1 per frame 
            yield return new WaitForEndOfFrame();
        }

        //after routine is over
        coroutineAllowed = true;

    }

}
