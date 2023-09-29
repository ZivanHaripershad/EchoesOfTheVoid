using UnityEngine;

public class RetryButton : Button
{
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
        OrbCounterUI.GetInstance().SetOrbText(0);
        orbCounter.planetOrbsDeposited = 0;
        healthCount.currentHealth = healthCount.maxHealth;
        gameManagerData.numberOfOrbsCollected = 0;
        gameManagerData.numberOfEnemiesKilled = 0;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;
    }
    
    public override void OnMouseDown()
    {
        mouseControl.EnableMouse();
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    
    public override void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }
}