using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUpgradeLevel2 : MonoBehaviour
{
    private static SelectedUpgradeLevel2 instance;

    private Upgrade upgrade;
    
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }

        DontDestroyOnLoad(this);
    }
    
    public static SelectedUpgradeLevel2 Instance
    {
        get { return instance; }
    }

    public Upgrade GetUpgrade()
    {
        return upgrade;
    }

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
    }
}
