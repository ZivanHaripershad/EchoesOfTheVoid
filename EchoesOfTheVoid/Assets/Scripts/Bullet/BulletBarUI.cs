using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarUI : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;

    [SerializeField] Sprite[] goldSprites;

    public BulletCount bulletCount;

    private SpriteRenderer spriteRenderer;

    private bool isBurstShot;

    //number of bullets = sprite number

    // Start is called before the first frame update
    void Start()
    {
        isBurstShot = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBurstShot)
        {
            Debug.Log(bulletCount.currentBullets);
            if (bulletCount.currentBullets >= 0 && bulletCount.currentBullets < sprites.Length - 1)
                spriteRenderer.sprite = sprites[bulletCount.currentBullets];
        }
        else
        {
            if (bulletCount.currentBullets >= 0 && bulletCount.currentBullets < sprites.Length - 1)
                spriteRenderer.sprite = goldSprites[bulletCount.currentBullets];

            if (bulletCount.currentBullets == 0)
            {
                spriteRenderer.sprite = sprites[bulletCount.currentBullets];
                isBurstShot = false;
            }
        }
    }

    public void SetBurstShotSprites()
    {
        isBurstShot = true;
    }

    public void UnsetBurstSprites()
    {
        spriteRenderer.sprite = sprites[bulletCount.currentBullets];
        isBurstShot = false;
    }

}