﻿using UnityEngine;

public abstract class Button : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite defaultText;
    [SerializeField] protected Sprite hoveredText;
    [SerializeField] protected MouseControl mouseControl;

    private void Start()
    {
        mouseControl = GameObject.Find("MouseControl").GetComponent<MouseControl>();
        mouseControl.EnableMouse();
        spriteRenderer.sprite = defaultText;
    }

    public abstract void OnMouseDown();

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultText;
    }

    public abstract void OnMouseUp();
}
