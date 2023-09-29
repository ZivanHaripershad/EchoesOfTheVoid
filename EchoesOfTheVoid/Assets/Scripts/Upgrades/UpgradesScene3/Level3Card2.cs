using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Card2 : Level3Card
{
    // Start is called before the first frame update
    void Start()
    {
        cardDesc = GameObject.Find("Card2Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new DoubleDamageUpgrade();
        currentSprite = normalSprite;
    }
}
