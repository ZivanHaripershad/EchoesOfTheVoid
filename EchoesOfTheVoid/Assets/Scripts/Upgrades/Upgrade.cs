using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    private string name;
    private string value;

    protected void SetName(string name)
    {
        this.name = name;
    }
    
    protected void SetValue(string value)
    {
        this.value = value;
    }

    public string GetName()
    {
        return name;
    }

    public string GetValue()
    {
        return value;
    }
}
