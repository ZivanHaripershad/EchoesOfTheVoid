using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Card3 : Level2Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardDesc = GameObject.Find("Card3Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new OrbCapacityUpgrade();
        currentSprite = normalSprite;
    }
}
