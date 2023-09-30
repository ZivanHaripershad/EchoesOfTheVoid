
public class RampageAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfRampageCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .RampageAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
