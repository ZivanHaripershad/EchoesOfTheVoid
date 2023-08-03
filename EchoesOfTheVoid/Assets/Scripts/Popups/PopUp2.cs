using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TutorialData tutorialData;
    public MouseControl mouseControl;

    void Start()
    {
        mouseControl.EnableMouse();
    }
    
    private void OnMouseDown()
    {
        tutorialData.popUpIndex = 2;
    }
    
    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
    }
    
}
