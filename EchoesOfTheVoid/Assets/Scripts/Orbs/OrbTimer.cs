using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTimer : MonoBehaviour
{
    [SerializeField]
    float fateOutTimer;

    [SerializeField]
    float keepAliveTimer;

    [SerializeField]
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        keepAliveTimer -= Time.deltaTime;
                    
        if (keepAliveTimer < 5) {
            if (animator != null)
            {
                animator.SetBool("lowTime", true);
            }
            else
                Debug.LogWarning("animator is null");
            
        }

        if (keepAliveTimer < 0)
            Destroy(gameObject);
        

    }
}
