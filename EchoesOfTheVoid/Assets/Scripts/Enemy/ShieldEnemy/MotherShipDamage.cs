using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MotherShipDamage : MonoBehaviour
{
    [SerializeField]
    private MotherShipMovement motherShipMovement;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //damage
            motherShipMovement.StartShaking();
        }
    }
}
