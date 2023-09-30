
public class MillionaireAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfMillionaireCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .MillionaireAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
