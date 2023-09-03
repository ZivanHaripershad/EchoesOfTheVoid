using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontCollider : MonoBehaviour
{
    [SerializeField]
    private MotherShipMovement motherShipMovement;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //dodge forwards
            motherShipMovement.DodgeBackwards();
        }
    }
}
