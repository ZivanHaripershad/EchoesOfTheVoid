using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldCounter : ScriptableObject
{
    public int maxShieldAmount;
    public int currentShieldAmount;
    public bool isShieldActive;
}
