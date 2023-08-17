using UnityEngine;

public abstract class Deposit : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite enabledFactorySprite;
    [SerializeField] protected Sprite disabledFactorySprite;
    [SerializeField] protected FactoryCosts factoryCosts;
    public OrbCounter orbCounter;
    
    void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = enabledFactorySprite;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
