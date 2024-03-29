using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpeedControl : MonoBehaviour
{
    
    [SerializeField] private float pathFollowingEnemyNormalSpeed;
    [SerializeField] private float pathFollowingEnemyReducedSpeed;

    [SerializeField] private float shieldEnemyNormalSpeed;
    [SerializeField] private float shieldEnemyReducedSpeed;

    [SerializeField] private float motherShipNormalMoveSpeed;
    [SerializeField] private float motherShipReducedMoveSpeed;
    [SerializeField] private float motherShipNormalOrbitSpeed;
    [SerializeField] private float motherShipReducedOrbitSpeed;

    private float pathFollowingEnemyCurrentSpeed;
    private float shieldEnemyCurrentSpeed;
    private float motherShipMoveSpeed;
    private float motherShipOrbitSpeed;
    private float currentTimeScale;
    

    private void Start()
    {
        SpeedUp();
    }

    public void SpeedUp()
    {
        pathFollowingEnemyCurrentSpeed = pathFollowingEnemyNormalSpeed;
        shieldEnemyCurrentSpeed = shieldEnemyNormalSpeed;
        motherShipMoveSpeed = motherShipNormalMoveSpeed;
        motherShipOrbitSpeed = motherShipNormalOrbitSpeed;
        currentTimeScale = 1;
    }

    public void SlowDown()
    {
        pathFollowingEnemyCurrentSpeed = pathFollowingEnemyReducedSpeed;
        shieldEnemyCurrentSpeed = shieldEnemyReducedSpeed;
        motherShipMoveSpeed = motherShipReducedMoveSpeed;
        motherShipOrbitSpeed = motherShipReducedOrbitSpeed;
        currentTimeScale = 0.3f;
    }

    public float GetCurrentTimeScale()
    {
        return currentTimeScale;
    }

    public float GetPathFollowingEnemySpeed()
    {
        return pathFollowingEnemyCurrentSpeed;
    }

    public float GetShieldEnemySpeed()
    {
        return shieldEnemyCurrentSpeed;
    }
    public float GetMotherShipMoveSpeed()
    {
        return motherShipMoveSpeed;
    }
    public float GetMotherShipOrbitSpeed()
    {
        return motherShipOrbitSpeed;
    }
}
