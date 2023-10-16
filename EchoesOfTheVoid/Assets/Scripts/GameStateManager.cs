using System;
using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{

    private static GameStateManager _instance;

    private int maxOrbCapacity;

    void Awake()
    {
        DontDestroyOnLoad(this);
        maxOrbCapacity = 4;
    }

    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // If the _instance is null, try to find an existing GameStateManager in the scene
                _instance = FindObjectOfType<GameStateManager>();

                // If no GameStateManager exists, create a new one
                if (_instance == null)
                {
                    GameObject newGameObject = new GameObject("GameStateManager");
                    _instance = newGameObject.AddComponent<GameStateManager>();
                }
            }
            return _instance;
        }
    }

    public int GetMaxOrbCapacity()
    {
        return maxOrbCapacity;
    }

    public void SetMaxOrbCapacity(int maxOrbCapacity)
    {
        this.maxOrbCapacity = maxOrbCapacity;
    }

}
