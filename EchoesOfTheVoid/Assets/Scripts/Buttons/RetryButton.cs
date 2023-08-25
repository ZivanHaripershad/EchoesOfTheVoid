using UnityEngine;

public class RetryButton : Button
{
    public TutorialData tutorialData;
    private UIManager uiManager;
    public OrbCounter orbCounter;
    public HealthCount healthCount;
    public GameManagerData gameManagerData;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    
    private void next()
    {
        uiManager.SetLevelObjectsToActive();
        uiManager.SetAtmosphereObjectToActive();
        OrbCounterUI.GetInstance().SetOrbText(2);
        orbCounter.planetOrbsDeposited = 1;
        healthCount.currentHealth = healthCount.maxHealth;
        gameManagerData.numberOfOrbsCollected = 3;
        gameManagerData.numberOfEnemiesKilled = 3;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;
        tutorialData.popUpIndex = 6;
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}