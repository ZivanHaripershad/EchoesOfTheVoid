using UnityEngine;

public abstract class Deposit : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite enabledFactorySprite;
    [SerializeField] protected Sprite disabledFactorySprite;
    [SerializeField] protected FactoryCosts factoryCosts;
    [SerializeField] protected GameManagerData gameManagerData;
    [SerializeField] protected TutorialData tutorialData;
    public OrbCounter orbCounter;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enabledFactorySprite;
        Debug.Log(gameManager);
    }

    void Enable()
    {
        spriteRenderer.sprite = enabledFactorySprite; 
    }

    void Disable()
    {
        spriteRenderer.sprite = disabledFactorySprite;
    }
    
    public abstract void RenderSprites();
}
