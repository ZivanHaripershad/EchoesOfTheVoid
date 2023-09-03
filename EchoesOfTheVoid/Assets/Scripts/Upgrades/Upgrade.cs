using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    private string name;
    private float value;

    protected void SetName(string name)
    {
        this.name = name;
    }
    
    public void SetValue(float value)
    {
        this.value = value;
    }

    public string GetName()
    {
        return name;
    }

    public float GetValue()
    {
        return value;
    }
}
