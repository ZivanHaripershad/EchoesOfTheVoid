using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Sprite[] healthSprites;

    private SpriteRenderer sp;
    private int health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        sp = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        health--;
        if (health >= 0 && health < healthSprites.Length)
            sp.sprite = healthSprites[health];
        else
            Debug.Log("Invalid mothership health");
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}
