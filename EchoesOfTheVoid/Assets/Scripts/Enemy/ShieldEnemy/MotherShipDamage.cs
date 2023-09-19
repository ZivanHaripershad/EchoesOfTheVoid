using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class MotherShipDamage : MonoBehaviour
{
    [SerializeField] private MotherShipMovement motherShipMovement;
    [SerializeField] private AudioSource hitBoss;
    [SerializeField] private AudioSource killMotherShip;
    [SerializeField] private MotherShipHealth motherShipHealth;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float delayAfterKillingMothership;
    [SerializeField] private SpriteRenderer healthWindowSp;
    [SerializeField] private MotherShipGiveShields motherShipGiveShields;
    
    private Level1Controller level1Controller;
    private SpriteRenderer sp;

    private void Start()
    {
        level1Controller = GameObject.FindGameObjectWithTag("Level1Manager").GetComponent<Level1Controller>();
        sp = GetComponent<SpriteRenderer>();
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage
            hitBoss.Play();
            motherShipHealth.TakeDamage();

            if (motherShipHealth.IsDead())
            {
                Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
                AudioManager.Instance.PlaySFX("KillMotherShip");
                Destroy(gameObject);
            }
            else 
                motherShipMovement.StartShaking();
        }
    }
}
