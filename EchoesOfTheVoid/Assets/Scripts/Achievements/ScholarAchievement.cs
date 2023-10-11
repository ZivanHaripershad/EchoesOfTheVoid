
public class ScholarAchievement : Achievement
{
    void Awake()
    {
        // spriteRenderer.enabled = true;

        AchievementsManager.Instance.CheckIfScholarCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .ScholarAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
