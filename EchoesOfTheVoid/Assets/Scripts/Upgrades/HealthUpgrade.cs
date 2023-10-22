using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : Upgrade
{
    public HealthUpgrade()
    {
        SetName("HealthUpgrade");
        SetDescription("Enemies have a 20% chance of dropping a health orb");
    }
}
