using System;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public GameManagerData gameManagerData;
    public OrbCounter orbCounter;


    [SerializeField] private MissionObjectiveBanner missionObjectiveBanner;

    public void UpdateEnemiesDestroyedBanner()
    {
        if (gameManagerData.numberOfEnemiesKilled % 5 == 0)
        {
            var enemiesKilledUpdate = gameManagerData.numberOfEnemiesKilled + "/" + gameManagerData.numberOfEnemiesToKill + " enemies destroyed";
            missionObjectiveBanner.AddMissionUpdate(enemiesKilledUpdate);
        }
    }
    
    public void UpdatePlanetEnergyBanner()
    {
        var energyPercentage = Math.Round((decimal) orbCounter.planetOrbsDeposited / orbCounter.planetOrbMax * 100);
        if (energyPercentage % 20 == 0)
        {
            var energyPercentageUpdate = energyPercentage + "% Network Completed";
            missionObjectiveBanner.AddMissionUpdate(energyPercentageUpdate);
        }
    }
    
    public void UpdateMothershipDestroyedBanner()
    {
        var mothershipDestroyedUpdate = "1/1 Mothership Destroyed";
        missionObjectiveBanner.AddMissionUpdate(mothershipDestroyedUpdate);
    }

    public void UpdatePrimaryTargetHealthBanner(float healthPercentage)
    {
        var enemiesKilledUpdate = "Primary target health: " + (100 - healthPercentage) + "%";
        missionObjectiveBanner.AddMissionUpdate(enemiesKilledUpdate);
    }
    
}
