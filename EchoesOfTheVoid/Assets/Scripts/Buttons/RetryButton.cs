using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite defaultExitGameText;
    public Sprite hoveredExitGameText;
    public AudioSource audioSource;
    private MouseControl mouseControl;
    public TutorialData tutorialData;
    private UIManager uiManager;
    public OrbCounter orbCounter;
    public HealthCount healthCount;
    public GameManagerData gameManagerData;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseControl = GameObject.Find("MouseControl").GetComponent<MouseControl>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        spriteRenderer.sprite = defaultExitGameText;
        mouseControl.EnableMouse();
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

    private void OnMouseUp()
    {
        Invoke("next", 0.3f);
    }

    private void OnMouseDown()
    {
        mouseControl.EnableMouse();
        audioSource.Play();
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