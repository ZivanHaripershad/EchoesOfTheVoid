using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillEnergy : MonoBehaviour
{

    [SerializeField]
    Sprite[] sprites;

    [SerializeField]
    private OrbCounter counter;

    private SpriteRenderer spriteRenderer;

    private int prevSprite; 

    // Start is called before the first frame update

    [SerializeField]
    private float barFillSpeed;

    private float currTimeFilled;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevSprite = 0;      
    }


    void Awake()
    {
        counter.planetOrbMax = 10;
        counter.planetOrbsDeposited = 0;
    }

    // Update is called once per frame
    void Update()
    {

        int currSprite = (int) Mathf.Round((counter.planetOrbsDeposited * 1.0f / counter.planetOrbMax) * (sprites.Length - 1));
        
        if (prevSprite < currSprite)
        {
            currTimeFilled += Time.deltaTime;

            if (currTimeFilled > barFillSpeed)
            {
                currTimeFilled = 0;
                prevSprite++; 
            }
        }

        if (prevSprite > sprites.Length - 1)
            prevSprite = sprites.Length - 1;
        
        spriteRenderer.sprite = sprites[prevSprite];
    }
}
