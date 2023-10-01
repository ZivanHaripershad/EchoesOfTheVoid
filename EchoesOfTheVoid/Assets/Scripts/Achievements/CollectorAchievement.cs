
public class CollectorAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfCollectorCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .CollectorAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
