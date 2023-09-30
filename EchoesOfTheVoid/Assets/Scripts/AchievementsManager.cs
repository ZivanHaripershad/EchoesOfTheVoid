using System;
using UnityEngine;
using System.Collections.Generic;

public class AchievementsManager : MonoBehaviour
{

    private static AchievementsManager _instance;

    private Dictionary<Achievement, bool> achievements;

    private Dictionary<GameManagerData.Level, bool> levelsCompletedWithoutLosingHealth;
    
    private bool godModeCompleted;

    private bool godModeCheck;
    
    private int numOfShieldsUsed;

    private bool protectorCompleted;

    private bool collectorCompleted;

    private bool riskTakerCompleted;

    private int orbsCollected;

    private bool speedRunnerCompleted;

    private int numOfEnemiesKilled;
    
    
    void Awake()
    {
        DontDestroyOnLoad(this);

        achievements = new Dictionary<Achievement, bool>();

        achievements.Add(Achievement.ScholarAchievement, false);
        achievements.Add(Achievement.DeflectorAchievement, false);
        achievements.Add(Achievement.ProtectorAchievement, false);
        achievements.Add(Achievement.CollectorAchievement, false);
        achievements.Add(Achievement.RiskTakerAchievement, false);
        achievements.Add(Achievement.MillionaireAchievement, false);
        achievements.Add(Achievement.SpeedRunnerAchievement, false);
        achievements.Add(Achievement.RampageAchievement, false);
        achievements.Add(Achievement.GodModeAchievement, false);

        levelsCompletedWithoutLosingHealth = new Dictionary<GameManagerData.Level, bool>();

        godModeCompleted = false;
        godModeCheck = false;
        numOfShieldsUsed = 0;
        protectorCompleted = false;
        collectorCompleted = false;
        riskTakerCompleted = false;
        orbsCollected = 0;
        speedRunnerCompleted = false;
        numOfEnemiesKilled = 0;
    }

    public enum Achievement
    {
        ScholarAchievement,
        DeflectorAchievement,
        ProtectorAchievement,
        CollectorAchievement,
        RiskTakerAchievement,
        MillionaireAchievement,
        SpeedRunnerAchievement,
        RampageAchievement,
        GodModeAchievement
    }

    public static AchievementsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // If the _instance is null, try to find an existing AchievementsManager in the scene
                _instance = FindObjectOfType<AchievementsManager>();

                // If no AchievementsManager exists, create a new one
                if (_instance == null)
                {
                    GameObject newGameObject = new GameObject("AchievementsManager");
                    _instance = newGameObject.AddComponent<AchievementsManager>();
                }
            }
            return _instance;
        }
    }

    public bool HasAchievementBeenCompleted(Achievement achievement)
    {
        return achievements[achievement];
    }

    public void SetAchievementToComplete(Achievement achievement)
    {
        achievements[achievement] = true;
    }

    public void AddToLevelCompletedDictionary(GameManagerData.Level level, bool completedWithoutLosingHealth)
    {
        try
        {
            levelsCompletedWithoutLosingHealth.Add(level, completedWithoutLosingHealth);
        }
        catch (ArgumentException)
        {
            Debug.Log(level + "already completed and added to dictionary");
        }
    }

    private void Update()
    {
        if (levelsCompletedWithoutLosingHealth.Count == 3 && !godModeCompleted && !godModeCheck)
        {
            Debug.Log(levelsCompletedWithoutLosingHealth);
            bool hasGodModeBeenDone = true;
            
            foreach (var kvp in levelsCompletedWithoutLosingHealth)
            {
                if (!kvp.Value) //if not true, end loop because player lost health on this level
                {
                    hasGodModeBeenDone = false;
                    break;
                }
            }
            
            Debug.Log("Has God Mode Been Done: " + hasGodModeBeenDone);

            godModeCheck = true;
            godModeCompleted = hasGodModeBeenDone;
        }
    }

    public void CheckIfGodModeCompleted()
    {
        if (godModeCompleted)
        {
            SetAchievementToComplete(Achievement.GodModeAchievement);
        }
    }

    public void IncrementNumOfShieldsUsed()
    {
        numOfShieldsUsed++;
    }
    
    public void CheckIfDeflectorCompleted()
    {
        if (numOfShieldsUsed >= 10)
        {
            SetAchievementToComplete(Achievement.DeflectorAchievement);
        }
    }

    public void SetProtectorCompletionStatus(bool completed)
    {
        protectorCompleted = completed;
    }
    
    public bool GetProtectorCompletionStatus()
    {
        return protectorCompleted;
    }

    public void CheckIfProtectorCompleted()
    {
        if (protectorCompleted)
        {
            SetAchievementToComplete(Achievement.ProtectorAchievement);
        }
    }

    public void SetCollectorCompletionStatus(bool completed)
    {
        collectorCompleted = completed;
    }
    
    public bool GetCollectorCompletionStatus()
    {
        return collectorCompleted;
    }
    
    public void CheckIfCollectorCompleted()
    {
        if (collectorCompleted)
        {
            SetAchievementToComplete(Achievement.CollectorAchievement);
        }
    }
    
    public void SetRiskTakerCompletionStatus(bool completed)
    {
        riskTakerCompleted = completed;
    }

    public bool GetRiskTakerCompletionStatus()
    {
        return riskTakerCompleted;
    }
    
    public void CheckIfRiskTakerCompleted()
    {
        if (riskTakerCompleted)
        {
            SetAchievementToComplete(Achievement.RiskTakerAchievement);
        }
    }
    
    public void IncrementOrbsCollected()
    {
        orbsCollected++;
    }
    
    public void CheckIfMillionaireCompleted()
    {
        if (orbsCollected >= 100)
        {
            SetAchievementToComplete(Achievement.MillionaireAchievement);
        }
    }

    public void SetSpeedRunnerAchievementStatus(bool completed)
    {
        speedRunnerCompleted = completed;
    }
    
    public bool GetSpeedRunnerCompletionStatus()
    {
        return speedRunnerCompleted;
    }
    
    public void CheckIfSpeedRunnerCompleted()
    {
        if (speedRunnerCompleted)
        {
            SetAchievementToComplete(Achievement.SpeedRunnerAchievement);
        }
    }
    
    public void IncrementNumOfEnemiesKilled()
    {
        numOfEnemiesKilled++;
    }
    
    public void CheckIfRampageCompleted()
    {
        if (numOfEnemiesKilled >= 100)
        {
            SetAchievementToComplete(Achievement.RampageAchievement);
        }
    }
    
}
