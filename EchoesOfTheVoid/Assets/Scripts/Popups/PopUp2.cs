using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp2 : MonoBehaviour
{
    // Start is called before the first frame update
    public TutorialData tutorialData;

    private void OnMouseDown()
    {
        tutorialData.popUpIndex = 2;
    }
}
