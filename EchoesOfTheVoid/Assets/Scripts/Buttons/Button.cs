using System;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite defaultText;
    [SerializeField] protected Sprite hoveredText;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] private MouseControl mouseControl;

    private void Start()
    {
        mouseControl = GameObject.Find("MouseControl").GetComponent<MouseControl>();
        mouseControl.EnableMouse();
        spriteRenderer.sprite = defaultText;
    }
    
    private void OnMouseDown()
    {
        mouseControl.EnableMouse();
        audioSource.Play();
    }

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
