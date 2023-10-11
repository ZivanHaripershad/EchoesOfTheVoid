using System;
using UnityEngine;
using System.Collections.Generic;

public class AchievementsManager : MonoBehaviour
{

    private static AchievementsManager _instance;

    private Dictionary<Achievement, bool> achievements;

    private Dictionary<GameManagerData.Level, bool> levelsCompletedWithoutLosingHealth;
    
    private bool godModeCompleted;

    private int numOfShieldsUsed;

    private bool protectorCompleted;

    private bool collectorCompleted;

    private bool riskTakerCompleted;

    private int orbsCollected;

    private bool speedRunnerCompleted;

    private int numOfEnemiesKilled;
    
    private bool scholarCompleted;

    private Queue<string> achievementUpdates = new Queue<string>();
    
    [SerializeField] private float bannerWaitTime;
    
    private bool isBannerAvailable = true;


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
        
        levelsCompletedWithoutLosingHealth.Add(GameManagerData.Level.Level1, false);
        levelsCompletedWithoutLosingHealth.Add(GameManagerData.Level.Level2, false);
        levelsCompletedWithoutLosingHealth.Add(GameManagerData.Level.Level3, false);

        godModeCompleted = false;
        numOfShieldsUsed = 0;
        protectorCompleted = false;
        collectorCompleted = false;
        riskTakerCompleted = false;
        orbsCollected = 0;
        speedRunnerCompleted = false;
        scholarCompleted = false;
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

    public void UpdateLevelCompletedDictionary(GameManagerData.Level level, bool completedWithoutLosingHealth)
    {
        levelsCompletedWithoutLosingHealth[level] = completedWithoutLosingHealth;
    }

    private void CheckGodModeStatus()
    {
        if (!godModeCompleted)
        {
            bool hasGodModeBeenDone = true;
            
            foreach (var kvp in levelsCompletedWithoutLosingHealth)
            {
                if (!kvp.Value) //if not true, end loop because player lost health on this level
                {
                    hasGodModeBeenDone = false;
                    break;
                }
            }
            
            godModeCompleted = hasGodModeBeenDone;
        }
    }
    
    public bool CheckLevelGodModeCompleted(GameManagerData.Level level)
    {
        return levelsCompletedWithoutLosingHealth[level];
    }

    public void CheckIfGodModeCompleted()
    {
        CheckGodModeStatus();

        bool hasGodModeBeenAchieved = achievements[Achievement.GodModeAchievement];

        if (hasGodModeBeenAchieved)
        {
            return;
        }
        
        if (godModeCompleted)
        {
            AddAchievementUpdate("God Mode \n Achieved");
            SetAchievementToComplete(Achievement.GodModeAchievement);
        }
    }

    public void IncrementNumOfShieldsUsed()
    {
        numOfShieldsUsed++;
    }
    
    public void CheckIfDeflectorCompleted()
    {
        bool hasDeflectorBeenAchieved = achievements[Achievement.DeflectorAchievement];

        if (hasDeflectorBeenAchieved)
        {
            return;
        }
        
        if (numOfShieldsUsed >= 10)
        {
            AddAchievementUpdate("Deflector \n Achieved");
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
        bool hasProtectorBeenAchieved = achievements[Achievement.ProtectorAchievement];

        if (hasProtectorBeenAchieved)
        {
            return;
        }
        
        if (protectorCompleted)
        {
            AddAchievementUpdate("Protector \n Achieved");
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
        bool hasCollectorBeenAchieved = achievements[Achievement.CollectorAchievement];

        if (hasCollectorBeenAchieved)
        {
            return;
        }
        
        if (collectorCompleted)
        {
            AddAchievementUpdate("Collector \n Achieved");
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
        bool hasRiskTakerBeenAchieved = achievements[Achievement.RiskTakerAchievement];

        if (hasRiskTakerBeenAchieved)
        {
            return;
        }
        
        if (riskTakerCompleted)
        {
            AddAchievementUpdate("Risk Taker \n Achieved");
            SetAchievementToComplete(Achievement.RiskTakerAchievement);
        }
    }
    
    public void IncrementOrbsCollected()
    {
        orbsCollected++;
    }
    
    public void CheckIfMillionaireCompleted()
    {
        bool hasMillionaireBeenAchieved = achievements[Achievement.MillionaireAchievement];

        if (hasMillionaireBeenAchieved)
        {
            return;
        }
        
        if (orbsCollected >= 100)
        {
            AddAchievementUpdate("Millionaire \n Achieved");
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
        bool hasSpeedRunnerBeenAchieved = achievements[Achievement.SpeedRunnerAchievement];

        if (hasSpeedRunnerBeenAchieved)
        {
            return;
        }
        
        if (speedRunnerCompleted)
        {
            AddAchievementUpdate("Speed Runner \n Achieved");
            SetAchievementToComplete(Achievement.SpeedRunnerAchievement);
        }
    }
    
    public void IncrementNumOfEnemiesKilled()
    {
        numOfEnemiesKilled++;
    }
    
    public void CheckIfRampageCompleted()
    {
        bool hasRampageBeenAchieved = achievements[Achievement.RampageAchievement];

        if (hasRampageBeenAchieved)
        {
            return;
        }
        
        if (numOfEnemiesKilled >= 100)
        {
            AddAchievementUpdate("Rampage \n Achieved");
            SetAchievementToComplete(Achievement.RampageAchievement);
        }
    }
    
    public void SetScholarAchievementStatus(bool completed)
    {
        scholarCompleted = completed;
    }
    
    public bool GetScholarCompletionStatus()
    {
        return scholarCompleted;
    }
    
    public void CheckIfScholarCompleted()
    {
        bool hasScholarBeenAchieved = achievements[Achievement.ScholarAchievement];

        if (hasScholarBeenAchieved)
        {
            return;
        }
        
        if (scholarCompleted)
        {
            AddAchievementUpdate("Scholar \n Achieved");
            SetAchievementToComplete(Achievement.ScholarAchievement);
        }
    }
    
    public void ResetBannerWaitTime()
    {
        bannerWaitTime = 5;
    }
    
    public float GetBannerWaitTime()
    {
        return bannerWaitTime;
    }

    public void AddAchievementUpdate(string achievementUpdate)
    {
        if (achievementUpdates != null)
        {
            achievementUpdates.Enqueue(achievementUpdate);
        }
    }

    public bool GetIsBannerAvailable()
    {
        return isBannerAvailable;
    }

    public void SetIsBannerAvailable(bool isAvailable)
    {
        isBannerAvailable = isAvailable;
    }

    public Queue<string> GetAchievementUpdates()
    {
        return achievementUpdates;
    }
    
}
