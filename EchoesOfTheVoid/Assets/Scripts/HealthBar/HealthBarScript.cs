using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField]
    Sprite[] sprites;

    public HealthCount healthCount;

    private SpriteRenderer spriteRenderer;
    //number of bullets = sprite number

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthCount.currentHealth = healthCount.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(healthCount.currentHealth);
        spriteRenderer.sprite = sprites[healthCount.currentHealth - 1];
    }
}
