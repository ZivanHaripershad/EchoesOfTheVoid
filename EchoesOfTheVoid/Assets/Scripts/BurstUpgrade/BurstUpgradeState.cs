using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BurstUpgradeState : ScriptableObject
{
    public bool isBurstUpgradeReplenishing;
    public bool isBurstUpgradeCoolingDown;
    public bool isBurstUpgradeReady;
    public bool isBurstUpgradeBeingUsed;
}
