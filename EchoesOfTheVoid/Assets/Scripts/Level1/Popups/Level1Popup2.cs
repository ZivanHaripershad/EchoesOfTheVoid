using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Level1Popup2 : Button
{
    // Start is called before the first frame update
    public Level1Data level1Data;

    private void Next()
    {
        level1Data.popUpIndex = 2;
    }
    
    public override void OnMouseUp()
    {
        Invoke("Next", 0.3f);
    }

}
