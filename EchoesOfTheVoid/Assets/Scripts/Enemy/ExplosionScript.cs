using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float duration = 8f;

    float trans; 

    void Start()
    {
        trans = 1f;
    }


    private void FixedUpdate()
    {
        if (trans > 0) { 
            gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, trans);
            trans -= 0.02f;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
