
public class RiskTakerAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfRiskTakerCompleted();
        
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .RiskTakerAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
