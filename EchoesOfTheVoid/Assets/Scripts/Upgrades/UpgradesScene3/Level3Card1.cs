using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Card1 : Level3Card
{
    // Start is called before the first frame update
    
    void Start()
    {
        cardDesc = GameObject.Find("Card1Desc").gameObject;
        cardDesc.SetActive(false);
        upgrade = new ReduceStunUpgrade();
        currentSprite = normalSprite;
    }
}
