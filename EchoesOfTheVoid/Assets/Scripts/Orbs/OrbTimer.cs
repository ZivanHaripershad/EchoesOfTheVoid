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
    float blinkRate;

    private float CurrentOpacity;
    private SpriteRenderer SR;
    private bool increaseOpacity; 

    // Start is called before the first frame update
    void Start()
    {
        CurrentOpacity = fateOutTimer; 
        SR = GetComponent<SpriteRenderer>();
        increaseOpacity = false;
    }

    // Update is called once per frame
    void Update()
    {

        keepAliveTimer -= Time.deltaTime;
                    
        if (keepAliveTimer < 5) { 
            if (increaseOpacity)
            {
                CurrentOpacity += Time.deltaTime * blinkRate;
                if (CurrentOpacity > 1)
                {
                    CurrentOpacity = 1;
                    increaseOpacity = false;
                }
            } 
            else
            {
                CurrentOpacity -= Time.deltaTime * blinkRate;
                if (CurrentOpacity < 0.2)
                {
                    CurrentOpacity = 0.2f;
                    increaseOpacity = true;
                }
            }
            
            SR.color = new Color(1f, 1f, 1f, CurrentOpacity);
        }

        if (keepAliveTimer < 0)
            Destroy(gameObject);
        

    }
}
