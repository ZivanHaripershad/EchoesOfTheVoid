using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField] private HealthCount healthCount;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        healthCount.currentHealth = healthCount.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[healthCount.currentHealth];
    }
}
