using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card3 : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardDesc = GameObject.Find("Card3Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new ShipHandlingUpgrade();
        currentSprite = normalSprite;
    }
}
