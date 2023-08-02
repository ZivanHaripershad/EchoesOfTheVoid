using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EarthTakeDamage : MonoBehaviour
{

    public HealthCount healthCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int amount)
    {
        if (healthCount.currentHealth > 0)
        {
            healthCount.currentHealth -= amount;
        }
        else
        {
            Debug.Log("DIE");
        }
    }
}
