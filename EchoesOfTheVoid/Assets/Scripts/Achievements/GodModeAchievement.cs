
public class GodModeAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfGodModeCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .GodModeAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
