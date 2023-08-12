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
    private MouseControl mouseControl;
    private AudioSource audioSource;

    void Start()
    {
        spriteRenderer.sprite = defaultNewGameText;
        audioSource = GetComponent<AudioSource>();
        mouseControl = GameObject.Find("MouseControl").GetComponent<MouseControl>();
        mouseControl.EnableMouse();
    }

    private void next()
    {
        tutorialData.popUpIndex = 1;
    }
    
    private void OnMouseDown()
    {
        audioSource.Play();
    }

    private void OnMouseUp()
    {
        Invoke("next", 0.3f);
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
