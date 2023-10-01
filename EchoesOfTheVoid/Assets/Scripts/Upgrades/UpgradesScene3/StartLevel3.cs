using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevel3 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite enabledButton;
    public Sprite disabledButton;
    public Sprite onHoverButton;
    public UpgradeScene3Manager upgradeScene3Manager;
    public MouseControl mouseControl;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.enabled = true;
        mouseControl.EnableMouse();
    }

    private void Update()
    {
        CheckIfUpgradeIsSet(enabledButton);
    }

    private void CheckIfUpgradeIsSet(Sprite enabledSprite)
    {
        if (upgradeScene3Manager.GetUpgrade() != null)
        {
            spriteRenderer.sprite = enabledSprite;
        }
        else
        {
            spriteRenderer.sprite = disabledButton;
        }
    }

    private void OnMouseDown()
    {
        if (upgradeScene3Manager.GetUpgrade() != null)
        {
            Debug.Log("Level 3 Upgrade: " + upgradeScene3Manager.GetUpgrade().GetName());
            SelectedUpgradeLevel3.Instance.SetUpgrade(upgradeScene3Manager.GetUpgrade());
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene("Level3");
        }
        else
        {
            AudioManager.Instance.PlaySFX("CannotDeposit");
        }
        
    }

    private void OnMouseEnter()
    {
        mouseControl.EnableMouse();
        CheckIfUpgradeIsSet(onHoverButton);
    }

    private void OnMouseExit()
    {
        CheckIfUpgradeIsSet(enabledButton);
    }
}
