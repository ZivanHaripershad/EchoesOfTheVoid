using UnityEngine;

[CreateAssetMenu]
public class GameManagerData : ScriptableObject
{
    public int numberOfEnemiesKilled;
    public int numberOfOrbsCollected;
    public bool expireOrbs;
    public bool tutorialActive;
    public float tutorialWaitTime;
    public bool hasResetAmmo;
    public int numberOfEnemiesToKill;
    public Level level;
    public bool isShieldUp;
    public float spawnInterval;
    public float timeTillNextWave;
    public float spawnTimerVariation;
    
    public enum Level
    {
        Tutorial, 
        Level1, 
        Level2,
        Level3
    }
}
