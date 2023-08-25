using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpaceshipMode : ScriptableObject
{
    public bool collectionMode;
    public Vector3 currentPosition;
    public bool returningToPlanet;
    public bool isOnCenterObjectsRadius;
    public bool canRotateAroundPlanet;
}
