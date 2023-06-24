using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterEnemyMovement : MonoBehaviour
{
    
   
    private Vector3 target = new Vector3(0, 0, 0);

    [SerializeField]
    private float moveSpeed;

    //move towards 000
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        transform.up = target - transform.position;
    }
}
