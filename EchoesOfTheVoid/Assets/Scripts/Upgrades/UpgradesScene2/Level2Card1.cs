using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Card1 : Level2Card
{
    // Start is called before the first frame update
    
    void Start()
    {
        cardDesc = GameObject.Find("Card1Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new CollectionRadiusUpgrade();
        currentSprite = normalSprite;
    }
}
