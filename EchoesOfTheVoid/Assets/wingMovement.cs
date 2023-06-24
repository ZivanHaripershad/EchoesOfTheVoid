using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float splitDelay;

    [SerializeField]
    private bool isRightWing;

    [SerializeField]
    private float movementSpeed;

    private float timePassed;
    
    void Start()
    {
        timePassed = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (timePassed > splitDelay)
        {
            if (isRightWing)
            {
                gameObject.transform.Translate(Vector3.left * movementSpeed *  Time.deltaTime);
            }
            else
            {
                gameObject.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
        }

    }
}
