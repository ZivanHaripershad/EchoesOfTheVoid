using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarUI : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites;

    public BulletCount bulletCount;

    private SpriteRenderer spriteRenderer;
    //number of bullets = sprite number

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = sprites[bulletCount.currentBullets];
    }
}
