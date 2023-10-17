using System;
using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
    
    private static GameStateManager _instance;

    private int maxOrbCapacity;
    private bool isLevel1Completed;
    private bool isLevel2Completed;
    private bool isLevel3Completed;

    void Awake()
    {
        DontDestroyOnLoad(this);
        maxOrbCapacity = 4;
        isLevel1Completed = false;
        isLevel2Completed = false;
        isLevel3Completed = false;
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
    
    public bool IsLevel1Completed
    {
        get => isLevel1Completed;
        set => isLevel1Completed = value;
    }

    public bool IsLevel2Completed
    {
        get => isLevel2Completed;
        set => isLevel2Completed = value;
    }

    public bool IsLevel3Completed
    {
        get => isLevel3Completed;
        set => isLevel3Completed = value;
    }

}
