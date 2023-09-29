using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Level3Card : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    public GameObject cardDesc;
    public Sprite hoverSprite;
    public Sprite selectedSprite;
    public Sprite normalSprite;
    public UpgradeScene3Manager upgradeScene3Manager;
    public Text upgradesSelectedText;
    public MouseControl mouseControl;
    protected Sprite currentSprite;
    protected Upgrade upgrade;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = true;
        cardDesc.SetActive(false);
        mouseControl.EnableMouse();
    }

    private void OnMouseDown()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        
        upgradeScene3Manager.SetUpgrade(upgrade);

        UpdateCardSprites();

        if (upgradeScene3Manager.GetUpgrade() != null)
        {
            upgradesSelectedText.text = "Upgrades Selected \n 1/1";
        }
        else
        {
            upgradesSelectedText.text = "Upgrades Selected \n 0/1";
        }
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        spriteRenderer.sprite = hoverSprite;
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
        cardDesc.SetActive(true);
    }

    private void OnMouseExit()
    {
        cardDesc.SetActive(false);
        transform.localScale = new Vector3(1f, 1f, 1f);
        spriteRenderer.sprite = currentSprite;
    }

    private void UpdateCardSprites()
    {
        var card1 = FindObjectsOfType<Level3Card>().ToList().Find( x=>x.name == "Card1");
        var card2 = FindObjectsOfType<Level3Card>().ToList().Find( x=>x.name == "Card2");
        var card3 = FindObjectsOfType<Level3Card>().ToList().Find( x=>x.name == "Card3");
        
        var card1SpriteRenderer = card1.spriteRenderer;
        var card2SpriteRenderer = card2.spriteRenderer;
        var card3SpriteRenderer = card3.spriteRenderer;
        
        if (upgradeScene3Manager.GetUpgrade() != null && upgradeScene3Manager.GetUpgrade().GetName() == "ShipHandlingUpgrade")
        {
            card1SpriteRenderer.sprite = card1.normalSprite;
            card1.currentSprite = card1.normalSprite;
            card2SpriteRenderer.sprite = card2.normalSprite;
            card2.currentSprite = card2.normalSprite;
            card3SpriteRenderer.sprite = card3.selectedSprite;
            card3.currentSprite = card3.selectedSprite;
        }
        else if (upgradeScene3Manager.GetUpgrade() != null &&
                 upgradeScene3Manager.GetUpgrade().GetName() == "DoubleDamageUpgrade")
        {
            card1SpriteRenderer.sprite = card1.normalSprite;
            card1.currentSprite = card1.normalSprite;
            card2SpriteRenderer.sprite = card2.selectedSprite;
            card2.currentSprite = card2.selectedSprite;
            card3SpriteRenderer.sprite = card3.normalSprite;
            card3.currentSprite = card3.normalSprite;
        }
        else if (upgradeScene3Manager.GetUpgrade() != null && upgradeScene3Manager.GetUpgrade().GetName() == "CollectionRadiusUpgrade")
        {
            card1SpriteRenderer.sprite = card1.selectedSprite;
            card1.currentSprite = card1.selectedSprite;
            card2SpriteRenderer.sprite = card2.normalSprite;
            card2.currentSprite = card2.normalSprite;
            card3SpriteRenderer.sprite = card3.normalSprite;
            card3.currentSprite = card3.normalSprite;
        }
    }
}
