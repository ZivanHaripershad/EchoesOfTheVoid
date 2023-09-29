using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField]
    private AudioSource minePlanted;

    [SerializeField]
    private float explodeDelay;

    [SerializeField] 
    private float explosionRadius;

    [SerializeField] private float detectionRadius;

    [SerializeField] private GameObject explosion;

    private GameObject player;
    private CameraShake cameraShake;

    private bool isCountingDown; 

    // Start is called before the first frame update
    void Start()
    {
        minePlanted.Play();
        isCountingDown = false;
        InvokeRepeating("CheckMine", 0f, 0.1f);
        player = GameObject.FindGameObjectWithTag("Player");
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void CheckMine()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (isCountingDown == false)
        {
            //player enters the detection radius of the mine, 
            //start counting down
            if (GetDistance() < detectionRadius)
            {
                AudioManager.Instance.PlaySFX("MineExplodes");
                isCountingDown = true;
            }
        }
        else
        {
            explodeDelay -= Time.deltaTime;
            
            if (explodeDelay <= 0)
            {
                AudioManager.Instance.PlaySFX("MineExplodesNoBeep");
                Explode();
            }
        }
        
    }

    private void Explode()
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);

        //player in the blast radius of the mine
        if (GetDistance() < explosionRadius)
        {
            //set off mine to explode
            cameraShake.Shake();
            player.GetComponent<SpaceshipCollection>().Stun();
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Explode();   
        }
    }

    private float GetDistance()
    {
        return Vector3.Distance(player.transform.position, gameObject.transform.position);
    }
    
}
