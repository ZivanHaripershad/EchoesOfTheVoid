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
    private GameManagerData.Level currentLevel;

    [SerializeField] private float maxDepositCoolDown;

    private bool isCooledDown;
    private float coolDownTime;
    private bool isMarkingModeOn;
    private int level1NumberOfEnemiesToKill;

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
    
    private void Update()
    {
        if (_instance && _instance.CoolDownTime >= _instance.MaxDepositCoolDown 
            && !_instance.IsCooledDown)
        {
            _instance.CoolDownTime = 0f;
            _instance.IsCooledDown = true;
        }
        
        if (_instance && !_instance.isCooledDown)
        {
            _instance.coolDownTime += Time.deltaTime;
        }
    }

    public int GetMaxOrbCapacity()
    {
        return _instance.maxOrbCapacity;
    }

    public void SetMaxOrbCapacity(int maxOrbCapacity)
    {
        _instance.maxOrbCapacity = maxOrbCapacity;
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

    public GameManagerData.Level CurrentLevel
    {
        get => _instance.currentLevel;
        set => _instance.currentLevel = value;
    }
    
    public bool IsCooledDown
    {
        get => _instance.isCooledDown;
        set => _instance.isCooledDown = value;
    }

    public float CoolDownTime
    {
        get => _instance.coolDownTime;
        set => _instance.coolDownTime = value;
    }
    
    public float MaxDepositCoolDown
    {
        get => _instance.maxDepositCoolDown;
        set => _instance.maxDepositCoolDown = value;
    }
    
    public bool IsMarkingModeOn
    {
        get => _instance.isMarkingModeOn;
        set => _instance.isMarkingModeOn = value;
    }
    
    public int Level1NumberOfEnemiesToKill
    {
        get => _instance.level1NumberOfEnemiesToKill;
        set => _instance.level1NumberOfEnemiesToKill = value;
    }
    
}
