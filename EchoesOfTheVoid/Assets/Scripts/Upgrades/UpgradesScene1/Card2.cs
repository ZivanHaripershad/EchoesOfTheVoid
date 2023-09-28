using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card2 : Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardDesc = GameObject.Find("Card2Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new BulletFireRateUpgrade();
        upgrade.SetValue(0.15f);
        currentSprite = normalSprite;
    }
}
