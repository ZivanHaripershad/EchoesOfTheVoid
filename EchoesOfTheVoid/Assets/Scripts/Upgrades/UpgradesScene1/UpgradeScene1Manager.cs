using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScene1Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private Upgrade selectedUpgrade; 

    // Start is called before the first frame update
    void Start()
    {
        selectedUpgrade = null;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        selectedUpgrade = upgrade;
    }

    public Upgrade GetUpgrade()
    {
        return selectedUpgrade;
    }

}
