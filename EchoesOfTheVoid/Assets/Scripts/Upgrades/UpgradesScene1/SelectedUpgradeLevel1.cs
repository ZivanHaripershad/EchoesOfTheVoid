using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUpgradeLevel1 : MonoBehaviour
{
    private static SelectedUpgradeLevel1 instance;

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
    
    public static SelectedUpgradeLevel1 Instance
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
