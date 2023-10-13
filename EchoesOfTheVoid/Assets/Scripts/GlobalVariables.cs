using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public int prevEnemySpawned;
    public int prevPrevEnemySpawned;
    public bool mustPause;
    public int prevMissileEnemySpawned;
    public int prevPrevMissileEnemySpawned;

    public bool stopSpawningEnemies;
    
    // Start is called before the first frame update
    void Start()
    {
        prevEnemySpawned = -100;
        prevPrevEnemySpawned = -200;
        prevPrevMissileEnemySpawned = -100;
        prevPrevMissileEnemySpawned = -200;
        mustPause = false;
        stopSpawningEnemies = true;
    }
}
