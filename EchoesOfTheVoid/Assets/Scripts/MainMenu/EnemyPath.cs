using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyPath : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.5f;

    private GameObject player;
    

    [SerializeField]
    private GameObject Spawner1;

    [SerializeField]
    private GameObject Spawner2;
    private bool val;
    public bool flip;

    // Start is called before the first frame update
    void Awake()
    {
        Spawner1 = GameObject.Find("EnemySpawner");
        Spawner2 = GameObject.Find("EnemySpawner2");
        
        //val = false;
        if (Random.value < 0.5f)
        {
            val = true;
            flip = false;
            //target = Spawner1;

            //Debug.Log(val);
        }
        else
        {
            val = false;
            flip = true;

            //target = Spawner2;

            // Debug.Log(val);

        }


    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //Spawner2.SetActive(false);
        

    }
    
    // Update is called once per frame
    void Update()
    {
        Swarm();
        Vector3 scale = transform.localScale;

        if(player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 *(flip ? -1:1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }


    private void Swarm()
    {
        //Debug.Log(val);
        if (val == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Spawner1.transform.position, speed * Time.deltaTime);
            //Debug.Log("MOving to erth1");
            

        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, Spawner2.transform.position, speed * Time.deltaTime);
            

            //Debug.Log("MOving to erth2");
        }
        //Debug.Log("MOving to player");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(this);
            if (val == false)
            {
                val = true;
                //Debug.Log(val);


            }
            else
            {
                val = true;
            }
        }
    }


}
