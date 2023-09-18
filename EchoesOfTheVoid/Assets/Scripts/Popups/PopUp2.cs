using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2 : Button
{
    // Start is called before the first frame update
    public TutorialData tutorialData;
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }

    private void Next()
    {
        tutorialData.popUpIndex = 1;
    }
    
    public override void OnMouseUp()
    {
        Invoke("Next", 0.3f);
    }

}
