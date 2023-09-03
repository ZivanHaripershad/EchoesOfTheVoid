using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Level1Popup1 : Button
{
    // Start is called before the first frame updat
    public Level1Data level1Data;
    

    private void next()
    {
        level1Data.popUpIndex = 1;
    }

    override 
    public void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }

  
}
