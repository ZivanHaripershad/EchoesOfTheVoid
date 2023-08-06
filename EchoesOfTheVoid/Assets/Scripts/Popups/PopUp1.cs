using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp1 : MonoBehaviour
{
    // Start is called before the first frame updat
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
        tutorialData.popUpIndex = 1;
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
