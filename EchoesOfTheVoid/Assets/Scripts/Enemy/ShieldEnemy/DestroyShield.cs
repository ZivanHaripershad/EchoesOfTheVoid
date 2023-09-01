using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyShield : MonoBehaviour
{

    [SerializeField] private AudioSource powerDownEffect;

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            //destroy the shield
            powerDownEffect.Play();
            Destroy(other.gameObject);
            Invoke("DestroyGameObject", 0.7f);
        }
    }
}
