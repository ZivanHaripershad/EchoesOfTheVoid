
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementBanner: MonoBehaviour
{
    private float achievementBannerWaitTime;
    private Text missionObjectiveText;
    [SerializeField] private GameObject achievementObjectiveCanvas;

    private void Start()
    {
        missionObjectiveText = achievementObjectiveCanvas.transform.Find("Notification").GetComponent<Text>();
        AchievementsManager.Instance.CheckIfCollectorCompleted();
        AchievementsManager.Instance.CheckIfDeflectorCompleted();
        AchievementsManager.Instance.CheckIfGodModeCompleted();
        AchievementsManager.Instance.CheckIfMillionaireCompleted();
        AchievementsManager.Instance.CheckIfProtectorCompleted();
        AchievementsManager.Instance.CheckIfRampageCompleted();
        AchievementsManager.Instance.CheckIfRiskTakerCompleted();
        AchievementsManager.Instance.CheckIfScholarCompleted();
        AchievementsManager.Instance.CheckIfSpeedRunnerCompleted();
    }

    private void Update()
    {
        Queue<string> achievementUpdates = AchievementsManager.Instance.GetAchievementUpdates();
        bool isBannerAvailable = AchievementsManager.Instance.GetIsBannerAvailable();

        if (achievementUpdates.Count > 0 && isBannerAvailable)
        {
            achievementBannerWaitTime = AchievementsManager.Instance.GetBannerWaitTime();
            AchievementsManager.Instance.SetIsBannerAvailable(false);
            achievementObjectiveCanvas.SetActive(true);
            var missionUpdate = achievementUpdates.Dequeue();
            AudioManager.Instance.PlaySFX("AchievementUnlocked");
            missionObjectiveText.text = missionUpdate;
        }
        
        if (achievementBannerWaitTime <= 0)
        {
            AchievementsManager.Instance.SetIsBannerAvailable(true);
            achievementObjectiveCanvas.SetActive(false);
            AchievementsManager.Instance.ResetBannerWaitTime();
        }
        
        achievementBannerWaitTime -= Time.deltaTime;
    }
}
