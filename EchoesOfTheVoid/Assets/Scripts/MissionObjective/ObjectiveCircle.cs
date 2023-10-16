using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectiveCircle : MonoBehaviour
{
    [SerializeField] protected Sprite[] sprites;
    [SerializeField] protected GameManagerData gameManagerData;
    
    [SerializeField] protected SpriteRenderer spriteRenderer; 
    protected int prevSprite;


    private void Start()
    {
        prevSprite = 0;
        spriteRenderer.sprite = sprites[0];
    }
}
