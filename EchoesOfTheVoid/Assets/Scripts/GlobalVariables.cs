using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    private int prevEnemySpawned;
    private int prevPrevEnemySpawned;
    // Start is called before the first frame update
    void Start()
    {
        prevEnemySpawned = -100;
        prevPrevEnemySpawned = -200;
    }

    // Update is called once per frame
    public int getPrevEnemySpawned()
    {
        return prevEnemySpawned;
    }

    public int getPrevPrevEnemySpawned()
    {
        return prevPrevEnemySpawned;
    }

    public void setPrevEnemySpawned(int prevEnemySpawned)
    {
        this.prevEnemySpawned = prevEnemySpawned;
        
    }

    public void setPrevPrevEnemySpawned(int prevPrevEnemySpawned)
    {
        this.prevPrevEnemySpawned = prevPrevEnemySpawned;
    }
}
