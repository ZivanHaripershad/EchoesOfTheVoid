
public class ProtectorAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfProtectorCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .ProtectorAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
