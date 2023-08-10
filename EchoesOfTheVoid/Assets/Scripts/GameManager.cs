using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isShieldEnabled;

    private void Start()
    {
        isShieldEnabled = false;
    }

    public void DisableShield()
    {
        isShieldEnabled = false;
    }
    
    public void EnableShield()
    {
        isShieldEnabled = true;
    }

    public bool IsShieldEnabled()
    {
        return isShieldEnabled;
    }
}
