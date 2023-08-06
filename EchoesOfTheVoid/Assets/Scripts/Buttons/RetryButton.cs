using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultExitGameText;
    public Sprite hoveredExitGameText;
    public AudioSource audioSource;
    public MouseControl mouseControl;
    public TutorialData tutorialData;
    public UIManager uiManager;
    public OrbCounter orbCounter;
    public HealthCount healthCount;
    public GameManagerData gameManagerData;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = defaultExitGameText;
        mouseControl.EnableMouse();
    }

    private void OnMouseDown()
    {
        Debug.Log("retrying level");
        mouseControl.EnableMouse();
        audioSource.Play();
        uiManager.SetLevelObjectsToActive();
        uiManager.SetAtmosphereObjectToActive();
        OrbCounterUI.instance.SetOrbText(2);
        orbCounter.planetOrbsDeposited = 1;
        healthCount.currentHealth = healthCount.maxHealth;
        gameManagerData.numberOfOrbsCollected = 3;
        gameManagerData.numberOfEnemiesKilled = 3;
        gameManagerData.tutorialWaitTime = 10f;
        gameManagerData.hasResetAmmo = true;
        tutorialData.popUpIndex = 6;
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoveredExitGameText;
    }

    private void OnMouseExit()
    {
        spriteRenderer.sprite = defaultExitGameText;
    }
}