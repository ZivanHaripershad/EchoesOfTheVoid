using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int lowHealth;

    [SerializeField] private Animator animator;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= lowHealth)
            animator.SetBool("isLowHealth", true);
    }

    public bool IsDead()
    {
        if (health <= 0)
        {
            GetComponent<MotherShipDamage>().enabled = false;
            GetComponent<MotherShipMovement>().enabled = false;
            GetComponent<MotherShipGiveShields>().enabled = false;
            return true;
        }

        return false;
    }
}
