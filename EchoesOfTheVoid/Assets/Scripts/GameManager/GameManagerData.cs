using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameManagerData : ScriptableObject
{
    public int numberOfEnemiesKilled;
    public int numberOfOrbsCollected;
    public bool expireOrbs;
    public bool tutorialActive;
    public float tutorialWaitTime;
    public bool hasResetAmmo;
    public int numberOfEnemiesToKill;
}
