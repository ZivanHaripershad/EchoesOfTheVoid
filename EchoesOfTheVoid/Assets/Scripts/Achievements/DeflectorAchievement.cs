
public class DeflectorAchievement : Achievement
{
    void Awake()
    {
        AchievementsManager.Instance.CheckIfDeflectorCompleted();
        
        if (AchievementsManager.Instance.HasAchievementBeenCompleted(AchievementsManager.Achievement
                .DeflectorAchievement))
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledSprite;
        }
    }
}
