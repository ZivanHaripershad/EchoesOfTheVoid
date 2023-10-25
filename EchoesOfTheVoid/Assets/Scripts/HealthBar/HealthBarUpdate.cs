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
        
        if(GameStateManager.Instance.CurrentLevel.Equals(GameManagerData.Level.Tutorial))
        {
            healthCount.currentHealth = 6;

        }
        else
        {
            healthCount.currentHealth = healthCount.maxHealth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCount.currentHealth > 0 && healthCount.currentHealth <= sprites.Length)
            spriteRenderer.sprite = sprites[healthCount.currentHealth - 1];
    }
}
