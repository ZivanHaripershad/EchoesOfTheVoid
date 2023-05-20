using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletCount : ScriptableObject
{
    public int maxBullets = 10;
    public int currentBullets;
    public bool generateBullets;
}
