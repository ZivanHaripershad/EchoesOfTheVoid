using System;
using UnityEngine;

public class Level2Popup2 : Button
{
    // Start is called before the first frame update
    public Level2Data level2Data;
    
    private void Next()
    {
        level2Data.popUpIndex = 1;
    }
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Invoke("Next", 0.3f);
    }

}
