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
        prevEnemySpawned = -1;
        prevPrevEnemySpawned = -2;
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
        this.prevPrevEnemySpawned = prevEnemySpawned;
    }

    public void setPrevPrevEnemySpawned(int prevPrevEnemySpawned)
    {
        this.prevPrevEnemySpawned = prevPrevEnemySpawned;
    }
}
