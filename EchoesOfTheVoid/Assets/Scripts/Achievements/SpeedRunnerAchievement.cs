
public class SpeedRunnerAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfSpeedRunnerCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .SpeedRunnerAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
