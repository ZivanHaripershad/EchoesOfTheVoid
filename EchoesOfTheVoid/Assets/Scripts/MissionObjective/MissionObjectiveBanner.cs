using System.Collections.Generic;
using UnityEngine;


public class MissionObjectiveBanner : MonoBehaviour
{
    [SerializeField] private float bannerWaitTime;

    private Queue<string> missionUpdates = new Queue<string>();

    private bool isBannerAvailable = true;

    public void ResetBannerWaitTime()
    {
        bannerWaitTime = 5;
    }
    
    public float GetBannerWaitTime()
    {
        return bannerWaitTime;
    }

    public void AddMissionUpdate(string missionUpdate)
    {
        if (missionUpdates != null)
        {
            Debug.Log("Adding Mission:" + missionUpdate);
            missionUpdates.Enqueue(missionUpdate);
        }
    }

    public bool GetIsBannerAvailable()
    {
        return isBannerAvailable;
    }

    public void SetIsBannerAvailable(bool isAvailable)
    {
        isBannerAvailable = isAvailable;
    }

    public Queue<string> GetMissionUpdates()
    {
        return missionUpdates;
    }
    
}
