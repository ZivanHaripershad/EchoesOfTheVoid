using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2 : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public Sprite defaultNewGameText;
    public Sprite hoveredNewGameText;
    public TutorialData tutorialData;
    public MouseControl mouseControl;
    public AudioSource audioSource;

    void Start()
    {
        spriteRenderer.sprite = defaultNewGameText;
        mouseControl.EnableMouse();
    }
    
    private void OnMouseDown()
    {
        audioSource.Play();
        tutorialData.popUpIndex = 2;
    }
    
    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredNewGameText;
    }
    
    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultNewGameText;
    }
    
}
